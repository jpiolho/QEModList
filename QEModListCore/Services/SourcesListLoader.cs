using QEModList.Core.Exceptions;
using QEModList.Core.Models.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Services
{
    public class SourcesListLoader
    {
        public class SourcesListHeader
        {
            public int Version { get; set; }
        }

        public class SourcesList
        {
            public List<string[]> Sources { get; set; }
        }


        private static readonly List<Source> DefaultSources = new List<Source>(new SourceAddonList[]
        {
            new SourceAddonList() { SourceValue = "https://mods.silver.idtech.services/" }
        });

        private const string Filename = "sources.json";

        private string GetSourcesPath()
        {
            return Path.Combine(QEModListPaths.GetProgramAppDataFolder(), Filename);
        }
        
        public async Task<List<Source>> LoadAsync(CancellationToken cancellationToken)
        {
            var path = GetSourcesPath();
            if (!File.Exists(path))
                return DefaultSources;

            // Read file
            var json = await File.ReadAllTextAsync(path, cancellationToken);

            // Parse header & compare version
            var header = JsonSerializer.Deserialize<SourcesListHeader>(json, QEModListServer.JsonSerializerOptions);
            if (header.Version != 1)
                throw new InvalidDataException($"Unsupported sources list version: {header.Version}");

            // Parse whole sources
            var sourcesList = JsonSerializer.Deserialize<SourcesList>(json, QEModListServer.JsonSerializerOptions);

            // Create the sources object
            var sources = new List<Source>();
            foreach (var source in sourcesList.Sources)
            {
                if (source.Length != 2)
                    throw new SourceException("Invalid source entry");

                var sourceType = source[0].Trim();
                var sourcePath = source[1].Trim();

                switch (sourceType.ToUpperInvariant())
                {
                    default: throw new InvalidDataException($"Unknown source type: {sourceType}");
                    case SourceAddonList.Type: sources.Add(new SourceAddonList() { BaseUrl = new Uri(sourcePath) }); break;
                    case SourceGithub.Type: sources.Add(new SourceGithub() { Url = new Uri(sourcePath) }); break;
                    case SourceFolder.Type: sources.Add(new SourceFolder() { Path = sourcePath }); break;
                }
            }


            return sources;
        }

        public async Task SaveAsync(IEnumerable<Source> sources, CancellationToken cancellationToken)
        {
            var path = GetSourcesPath();
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(new
            {
                Version = 1,
                Sources = sources.Select(s => new string[] { s.TypeName, s.SourceValue })
            }, QEModListServer.JsonSerializerOptions));
        }
    }
}
