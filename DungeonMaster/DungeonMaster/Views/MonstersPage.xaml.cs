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
	public partial class MonstersPage : ContentPage
	{
        MonstersViewModel viewModel;

        public MonstersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MonstersViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var mob = args.SelectedItem as Monster;
            if (mob == null)
                return;

            await Navigation.PushAsync(new InfoMonsterPage(new InfoMonsterViewModel(mob)));

            ItemsListView.SelectedItem = null;
        }

        public async void ToolbarClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewMonsterPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}