using DungeonMaster.Models;
using DungeonMaster.Views;
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
    public class MonstersViewModel : BaseViewModel
    {
        public ObservableCollection<Monster> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public MonstersViewModel()
        {
            Title = "Монстры";
            Items = new ObservableCollection<Monster>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<InfoMonsterPage, Monster>(this, "DeleteMob", async (obj, _mob) =>
            {
                var mob = _mob as Monster;

                await App.Database.DeleteMonster(mob);
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.Database.GetMonsters();
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
