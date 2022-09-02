using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QEModList.Core.Services;
using System;
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
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        private WebApplication? _app;

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

            await _app.RunAsync("http://127.0.0.1:80");
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_app is not null)
                await _app.StopAsync(cancellationToken);
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

            return Task.FromResult(Results.Ok(repo.List));
        }

        private Task<IResult> GetEmptyPakAsync()
        {
            return Task.FromResult(Results.File(Convert.FromBase64String("UEFDSwwAAAAAAAAA"), "application/octet-stream"));
        }
    }
}
