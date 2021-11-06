using Microsoft.AspNetCore.Hosting;
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

        private IWebHost _webhost;
        private CancellationTokenSource _cts;


        public QEModListServer()
        {
            _webhost = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://127.0.0.1:80")
                .UseStartup<Startup>()
                .Build();
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            _cts = new CancellationTokenSource();

            cancellationToken.Register(() => _cts.Cancel());
            await _webhost.RunAsync(_cts.Token);
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            await _webhost.StopAsync(cancellationToken);
        }
    }
}
