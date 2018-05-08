using System.Collections.Generic;
using System.Threading.Tasks;
using Todoist.Net;
using Todoist.Net.Models;

namespace kUMTE_2018.Models
{
    public class TodoIstApiHelper
    {
        private readonly ITodoistClient _client;
        public TodoIstApiHelper(string key)
        {
            _client = new TodoistClient(key);
        }

        public IEnumerable<Project> GetProjects()
        {
            IEnumerable<Project> projects = null;
            Task.Run(async () =>
            {
                projects = await _client.Projects.GetAsync();

            }).Wait();
            return projects;
        }

        public void AddItem(string text)
        {
            Task.Run(async () =>
            {
                var quickAddItem = new QuickAddItem(text);
                var task = await _client.Items.QuickAddAsync(quickAddItem);

            }).Wait();
        }

        public Resources GetResources()
        {
            Resources res = null;
            Task.Run(async () =>
            {
                res = await _client.GetResourcesAsync();

            }).Wait();
            return res;
        }
    }
}
