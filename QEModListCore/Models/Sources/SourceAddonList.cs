using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Models.Sources
{
    public class SourceAddonList : Source
    {
        private const string ContentJsonFile = "content.json";

        public Uri BaseUrl { get; set; }

  

        public override async Task<List<Addon>> GetAddonsAsync(CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var uri = new Uri(BaseUrl, ContentJsonFile);
            using var response = await client.GetAsync(uri,cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Unsuccessful status code from '{uri}': {response.StatusCode}");

            
            var stream = await response.Content.ReadAsStreamAsync();
            var list = await JsonSerializer.DeserializeAsync<AddonList>(stream, QEModListServer.JsonSerializerOptions);
            return list.Addons;
        }

        public override async Task<Stream> GetFileStreamAsync(string path,CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var uri = new Uri(BaseUrl, path);
            return await client.GetStreamAsync(uri,cancellationToken);
        }
    }
}
