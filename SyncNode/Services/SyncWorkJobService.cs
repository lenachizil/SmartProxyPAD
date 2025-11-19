using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Common.Models;
using Common.Utilities;
using SyncNode.Settings;

namespace SyncNode.Services
{
    public class SyncWorkJobService : IHostedService
    {
        private readonly ConcurrentDictionary<Guid, SyncEntity> documents =
            new ConcurrentDictionary<Guid, SyncEntity>();
        private readonly IMovieAPISettings _settings;

        private Timer _timer;

        public SyncWorkJobService(IMovieAPISettings settings)
        {
            _settings = settings;
        }

        public void AddItem(SyncEntity entity)
        {
            SyncEntity document = null;
            bool isPresent = documents.TryGetValue(entity.Id, out document);

            if (!isPresent || (isPresent && entity.LastChangedAt > document.LastChangedAt))
            {
                documents[entity.Id] = entity;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_timer = new Timer(DoSendWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            _timer = new Timer(async _ => await DoSendWorkAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        //private void DoSendWork(object state)
        //{
        //    foreach (var document in documents)
        //    {
        //        SyncEntity entity = null;
        //        var isPresent = documents.TryRemove(document.Key, out entity);

        //        if (isPresent)
        //        {
        //            var recievers = _settings.Hosts.Where(x => !x.Contains(entity.Origin));

        //            foreach (var reciever in recievers)
        //            { 
        //                var url = $"{reciever}/sync/{entity.ObjectType}";

        //                try
        //                {
        //                    var result = HttpClientUtility.SendJsonAsync(entity.JsonData, url, entity.SyncType);

        //                    if (!result.IsSuccessStatusCode)
        //                    {
        //                        //log error
        //                    }

        //                }
        //                catch (Exception e)
        //                { 
        //                //log
        //                }


        //            }

        //        }
        //    }
        //}
        private async Task DoSendWorkAsync()
        {
            foreach (var document in documents)
            {
                SyncEntity entity = null;
                var isPresent = documents.TryRemove(document.Key, out entity);

                if (isPresent)
                {
                    var recievers = _settings.Hosts.Where(x => !x.Contains(entity.Origin));

                    foreach (var reciever in recievers)
                    {
                        // var url = $"{reciever}/sync/{entity.ObjectType}";
                        var url = $"{reciever}/api/{entity.ObjectType.ToLower()}/sync";


                        try
                        {
                            var result = await HttpClientUtility.SendJsonAsync(entity.JsonData, url, entity.SyncType);

                            if (!result.IsSuccessStatusCode)
                            {
                                // log error
                            }
                        }
                        catch (Exception e)
                        {
                            // log
                        }
                    }
                }
            }
        }

    }
}