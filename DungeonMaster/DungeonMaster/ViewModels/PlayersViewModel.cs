using DungeonMaster.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DungeonMaster.ViewModels
{
    public class PlayersViewModel : BaseViewModel
    {
        public ObservableCollection<Player> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PlayersViewModel()
        {
            Title = "Игроки";
            Items = new ObservableCollection<Player>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.Database.GetHeroes();
                items = items.OrderBy(item => item.Name).ToList();

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
