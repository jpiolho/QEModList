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
        public const string Type = "ADDONLIST";
        private const string ContentJsonFile = "content.json";

        public Uri BaseUrl { get; set; }
        public override string SourceValue { get => BaseUrl.ToString(); set => BaseUrl = new Uri(value); }
        public override string TypeName => Type;

        public override object Clone()
        {
            return new SourceAddonList()
            {
                BaseUrl = BaseUrl
            };
        }

        public override async Task<List<Addon>> GetAddonsAsync(CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var uri = new Uri(BaseUrl, ContentJsonFile);
            using var response = await client.GetAsync(uri,cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Unsuccessful status code from '{uri}': {response.StatusCode}");

            
            var rawJson = await response.Content.ReadAsStringAsync();

            var list = JsonSerializer.Deserialize<AddonList>(SanitizeJson(rawJson), QEModListServer.JsonSerializerOptions);
            return list.Addons;
        }

        public override async Task<Stream> GetFileStreamAsync(string path,CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var uri = new Uri(BaseUrl, path.Replace(":","/"));
            return await client.GetStreamAsync(uri,cancellationToken);
        }


        private static string SanitizeJson(string json)
        {
            var sb = new StringBuilder();

            bool inQuote = false;
            bool escape = false;
            bool replaced;
            for (var i = 0; i < json.Length; i++)
            {
                var c = json[i];

                replaced = false;

                if (!escape)
                {
                    if (c == '\\')
                    {
                        escape = true;
                    }
                    else if (c == '"')
                    {
                        inQuote = !inQuote;
                    }
                    else if (inQuote)
                    {
                        if (c == '\r')
                        {
                            sb.Append("\\r");
                            replaced = true;
                        }
                        else if (c == '\n')
                        {
                            sb.Append("\\n");
                            replaced = true;
                        }
                    }
                }
                else
                {
                    escape = false;
                }

                if (!replaced)
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
