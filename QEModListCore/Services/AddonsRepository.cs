using Microsoft.Extensions.Hosting;
using Nito.AsyncEx;
using QEModList.Core.Models;
using QEModList.Core.Models.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Services
{
    public class AddonsRepository : IHostedService
    {
        private SourcesListLoader _sourcesListLoader;
        private AddonList _list;
        private List<Source> _sources;

        public AddonList List => _list;
        public List<Source> Sources => _sources;

        public AddonsRepository(SourcesListLoader sourcesListLoader)
        {
            _sourcesListLoader = sourcesListLoader;
            _list = new AddonList();
        }


        public async Task RefreshAsync(Action<int,int> progressCallback, CancellationToken cancellationToken)
        {
            var sources = _sources;

            var list = new AddonList();

            list.Addons.Add(new Addon() {
                Name = "=[Mod Changer]=",
                Author = "",
                Date = "",
                Description = new() { { "en", "This is the mod changer. Use it whenever you want to activate a multiplayer mod: 1. Activate this mod   2. Select the mod you want to activate   3. 'Delete' it   4. Re-download it   5. Activate it   6. Done!" } },
                Download = "empty.pak",
                Gamedir = "__modchanger",
                Id = "__modchanger",
                Size = 12
            });

            var listLock = new AsyncLock();

            int totalSources = sources.Count;
            int totalSourcesDone = 0;

            async Task AddSourceToList(int sourceId)
            {
                try
                {
                    var source = sources[sourceId];
                    var addons = await source.GetAddonsAsync(cancellationToken);

                    for (int aid = 0; aid < addons.Count; aid++)
                    {
                        Addon addon = addons[aid];

                        for (var i = 0; i < addon.Screenshots.Count; i++)
                            addon.Screenshots[i] = $"{sourceId}__{addon.Screenshots[i]}";

                        addon.Download = $"{sourceId}__{addon.Download.Replace("/",":").Replace("\\",":")}";
                        addon.Id = $"{addon.Id}";
                    }

                    using (await listLock.LockAsync(cancellationToken))
                    {
                        /*
                        list.Addons.Add(new Addon()
                        {
                            Name = $"---------",
                            Author = "",
                            Date = "",
                            Description = new() { { "en", "This is just a separator. Ignore" } },
                            Download = "empty.pak",
                            Gamedir = "_separator",
                            Id = "__modchanger",
                            Size = 12
                        });
                        */
                        list.Addons.AddRange(addons);
                    }

                }
                catch(Exception ex)
                {
                    ;
                }
                
                progressCallback?.Invoke(Interlocked.Increment(ref totalSourcesDone),totalSources);
            }

            var tasks = new List<Task>(sources.Count);
            for(var i=0;i<sources.Count;i++)
                tasks.Add(AddSourceToList(i));

            await Task.WhenAll(tasks);

            Interlocked.Exchange(ref _list, list);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _sources = await _sourcesListLoader.LoadAsync(cancellationToken);

            await RefreshAsync(null,cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
