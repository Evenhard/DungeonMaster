using DungeonMaster.Models;
using DungeonMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DungeonMaster.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayersPage : ContentPage
	{
        PlayersViewModel viewModel;

        public PlayersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new PlayersViewModel();
        }

        public async void ToolbarClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewPlayerPage()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var hero = args.SelectedItem as Player;
            if (hero == null)
                return;

            ItemsListView.SelectedItem = null;

            var result = await App.Current.MainPage.DisplayAlert(
                    "Внимание",
                    "Удалить игрока из списка?",
                    "Да", "Нет");

            if (result)
            {
                await App.Database.DeletePlayer(hero);
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}