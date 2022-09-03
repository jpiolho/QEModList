using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QEModList.Core.Services;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core
{


    public class QEModListServer
    {
        internal static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        private WebApplication? _app;
        public AddonsRepository? AddonsRepository => _app?.Services.GetRequiredService<AddonsRepository>();
        public SourcesListLoader? SourcesListLoader => _app?.Services.GetRequiredService<SourcesListLoader>();

        public bool IsRunning { get; private set; }


        public async Task RunAsync(CancellationToken cancellationToken = default)
        {

            var builder = WebApplication.CreateBuilder();
            builder.Host.UseConsoleLifetime();

            builder.Services.AddSingleton<SourcesListLoader>();
            builder.Services.AddSingleton<AddonsRepository>();
            builder.Services.AddHostedService(provider => provider.GetRequiredService<AddonsRepository>());

            _app = builder.Build();

            _app.MapGet("/", GetContentJsonAsync);
            _app.MapGet("/content.json", GetContentJsonAsync);
            _app.MapGet("/empty.pak", GetEmptyPakAsync);
            _app.MapGet("/{file}", GetFileAsync);

            IsRunning = true;
            await _app.RunAsync("http://127.0.0.1:80");
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_app is not null)
                await _app.StopAsync(cancellationToken);

            IsRunning = false;
        }


        private async Task<IResult> GetFileAsync(string file, CancellationToken cancellationToken)
        {
            var repository = _app.Services.GetRequiredService<AddonsRepository>();

            // Parse the source id
            const string Separator = "__";
            var idx = file.IndexOf(Separator);

            if (idx == -1)
                return Results.BadRequest("Missing source id from filename");

            if (!int.TryParse(file.Substring(0, idx), out var sourceId))
                return Results.BadRequest("Invalid source id");

            var filename = file.Substring(idx + Separator.Length);


            // Get mime type
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file, out var contentType))
                contentType = "application/octet-stream";

            // Return file
            return Results.Stream(await repository.Sources[sourceId].GetFileStreamAsync(filename, cancellationToken), contentType, filename);
        }

        private Task<IResult> GetContentJsonAsync()
        {
            var repo = _app.Services.GetRequiredService<AddonsRepository>();

            var json = SerializeAddonListToQEJson(repo.List);

            return Task.FromResult(Results.Text(json, "application/json"));
        }

        private Task<IResult> GetEmptyPakAsync()
        {
            return Task.FromResult(Results.File(Convert.FromBase64String("UEFDSwwAAAAAAAAA"), "application/octet-stream"));
        }




        private static JsonSerializerOptions listSerializationOptions = new JsonSerializerOptions()
        {
            WriteIndented = false,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private static string SerializeAddonListToQEJson(Models.AddonList list)
        {
            var json = JsonSerializer.Serialize(list, listSerializationOptions);

            // Support new lines
            var sb = new StringBuilder(json);
            sb.Replace("\\r", "\r");
            sb.Replace("\\n", "\n");

            return sb.ToString();
        }
    }
}
