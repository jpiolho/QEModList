using QEModList.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Models.Sources
{
    public class SourceGithub : Source
    {

        public Uri Url { get; set; }


        private Manifest _manifest;
        private string _versionTag;



        public override async Task<List<Addon>> GetAddonsAsync(CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            // Fetch latest release
            var response = await client.GetAsync(new Uri(Url, "releases/latest"), cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new SourceException($"Failed to fetch latest release. Status code: {response.StatusCode}");

            var versionTag = _versionTag = response.RequestMessage.RequestUri.Segments[^1];


            // Fetch the manifest
            response = await client.GetAsync(new Uri(Url, $"releases/download/{versionTag}/manifest.json"), cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new SourceException($"Failed to fetch manifest. Status code: {response.StatusCode}");

            var manifest = _manifest = await JsonSerializer.DeserializeAsync<Manifest>(await response.Content.ReadAsStreamAsync(cancellationToken), QEModListServer.JsonSerializerOptions, cancellationToken);


            var addon = new Addon()
            {
                Name = manifest.Name,
                Author = manifest.Author,
                Date = manifest.Date,
                Description = new() { { "en", manifest.Description } },
                Download = manifest.Pak,
                Gamedir = manifest.Gamedir,
                Id = manifest.Gamedir,
                Size = manifest.Size
            };

            // Convert screenshots

            return new List<Addon>(new Addon[] { addon }); ;
        }

        public override async Task<Stream> GetFileStreamAsync(string path, CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            var extension = Path.GetExtension(_manifest.Download).ToUpperInvariant();
            switch (extension)
            {
                default:
                    throw new SourceException($"Unsupported file extension: {extension}");
                case ".ZIP":
                    {
                        // Download zip
                        var tempFile = Path.GetTempFileName();
                        try
                        {
                            using (var fs = new FileStream(tempFile, FileMode.Open, FileAccess.ReadWrite))
                            using (var stream = await client.GetStreamAsync(new Uri(Url, $"releases/download/{_versionTag}/{_manifest.Download}"), cancellationToken))
                            {
                                await stream.CopyToAsync(fs, cancellationToken);
                            }

                            using (var zip = ZipFile.OpenRead(tempFile))
                            {
                                var entry = zip.Entries.FirstOrDefault(entry => Path.GetFileName(entry.FullName) == _manifest.Pak && entry.Length == _manifest.Size);

                                if (entry == null)
                                    throw new SourceException($"Could not find '{_manifest.Pak}' with size {_manifest.Size} inside zip");

                                using (var pakStream = entry.Open())
                                {
                                    // TODO: Improve this so it streams the file instead of having it in memory                        
                                    var ms = new MemoryStream();
                                    await pakStream.CopyToAsync(ms, cancellationToken);
                                    ms.Position = 0;
                                    return ms;
                                }
                            }
                        }
                        finally
                        {
                            File.Delete(tempFile);
                        }
                        break;
                    }
                case ".PAK":
                        return await client.GetStreamAsync(new Uri(Url, $"releases/download/{_versionTag}/{_manifest.Download}"), cancellationToken);
            }


        }

    }

}
