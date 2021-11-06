using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Models.Sources
{
    public class SourceFolder : Source
    {
        public string Path { get; set; }

        public override async Task<List<Addon>> GetAddonsAsync(CancellationToken cancellationToken)
        {


            var json = await File.ReadAllTextAsync(System.IO.Path.Combine(Path, "manifest.json"));
            var manifest = JsonSerializer.Deserialize<Manifest>(json, QEModListServer.JsonSerializerOptions);

            var addon = new Addon()
            {
                Name = manifest.Name,
                Author = manifest.Author,
                Date = manifest.Date,
                Description = new Dictionary<string, string>()
                {
                    { "en", manifest.Description }
                },
                Gamedir = !string.IsNullOrEmpty(manifest.Gamedir) ? manifest.Gamedir : System.IO.Path.GetFileName(Path),
                Id = System.IO.Path.GetFileName(Path),
                Download = manifest.Download,
                Size = manifest.Size,
                Screenshots = manifest.Screenshots
            };

            var list = new List<Addon>();
            list.Add(addon);
            return list;
        }

        public override Task<Stream> GetFileStreamAsync(string path, CancellationToken cancellationToken)
        {
            return Task.FromResult<Stream>(new FileStream(System.IO.Path.Combine(Path, path), FileMode.Open));
        }
    }
}
