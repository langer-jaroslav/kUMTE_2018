using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using kUMTE_2018.Models;
using SQLite;
using Todoist.Net;
using Todoist.Net.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kUMTE_2018
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskBrowsePage : ContentPage
    {
        public ObservableCollection<Item> Items { get; set; }
        private readonly Project _project;
        public TaskBrowsePage()
        {
            InitializeComponent();
        }

        public TaskBrowsePage(Project item)
        {
            InitializeComponent();
            _project = item;
            Title = item.Name;
        }

        protected override void OnAppearing()
        {
            LoadTasks();
        }

        private async Task LoadTasks()
        {
            Items = new ObservableCollection<Item>();
            using (var client = new TodoistClient(ProjectBrowsePage.AuthKey))
            {
                var tasks = await client.Items.GetAsync();
                foreach (var item in tasks.Where(x=>x.ProjectId == _project.Id))
                {
                    Items.Add(item);
                }
            }
            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var action = await DisplayActionSheet("Action", "Cancel", null, new[] {"Done", "Edit"});
            
            ((ListView)sender).SelectedItem = null;

            if (action == "Done")
            {
                using (var client = new TodoistClient(ProjectBrowsePage.AuthKey))
                {
                    await client.Items.CompleteAsync(true, ((Item) e.Item).Id);
                    Items.Remove((Item) e.Item);
                }
            }
            else if (action == "Edit")
            {
                await DisplayAlert(":(", "Not implemented yet :(", "Ok");
            }
        }

        private void AddButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTaskPage(_project));
        }
    }
}
