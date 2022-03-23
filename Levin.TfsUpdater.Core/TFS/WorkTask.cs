using System;
using System.Collections.Generic;
using System.Linq;


using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace Levin.TfsUpdater.Core.TFS
{
    public class WorkTask : IWorkTask
    {
        public string RegexString { get; set; }

        private WorkItem _workItem;

        WorkItemTrackingHttpClient _witClient;

        public WorkTask(WorkItemTrackingHttpClient witClient,WorkItem  workItem)
        {
            if (workItem == null)
                throw new NullReferenceException();

            _workItem = workItem;
            _witClient = witClient;

        }

        public string GetDescription()
        {
            string description =(string)_workItem.Fields.Where(x=>x.Key=="Description").FirstOrDefault().Value;

            return description;

        }

        public void ReplaceDescriprion(string newDescription)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();

            patchDocument.Add(
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.Description",
                    Value = newDescription
                }
            );


            _witClient.UpdateWorkItemAsync(patchDocument,(int)_workItem.Id);

        }

        public static WorkTask[] ToWorkTasks(WorkItemTrackingHttpClient witClient,WorkItem[] workItems)
        {
            List<WorkTask> tasks = new List<WorkTask>();


            foreach (var item in workItems)
            {
                tasks.Add(new WorkTask(witClient, item));

            }

            return tasks.ToArray();

        }
    }
}
