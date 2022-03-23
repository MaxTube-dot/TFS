using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.Metadata;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace Levin.TfsUpdater.Core.TFS
{
    public class TfsService:ITfsSevice
    {
        private string _token;

        private Uri _uri;

        VssConnection connection;

        WorkItemTrackingHttpClient witClient;


        public TfsService(string uri, string token)
        {
            _token = token;
            _uri = new Uri(uri);

            connection = new VssConnection(_uri, new VssBasicCredential(string.Empty, _token));

            witClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }

        public WorkTask[] GetItems(string wiql)
        {
            try
            {
                

                Wiql query = new Wiql() { Query = wiql };

                var ids = witClient.QueryByWiqlAsync(query).Result.WorkItems.Select(x=>x.Id);

                var workItems = witClient.GetWorkItemsAsync(ids).Result.ToArray();

                return WorkTask.ToWorkTasks(witClient, workItems);
            }
            catch
            {
                Loger.AddMessage(string.Format($"Ошибка получения задач из TFS token={0} uri={1} wiql={3}!",_token,_uri,wiql));
            }

            return null;
        }

    }
}
