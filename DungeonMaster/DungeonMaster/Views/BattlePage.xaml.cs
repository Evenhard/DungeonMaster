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
	public partial class BattlePage : ContentPage
	{
        BattleViewModel viewModel;

        public BattlePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BattleViewModel();
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var item = args.SelectedItem as Fighter;
            //if (item == null)
            //    return;

            //var mob = await App.Database.GetMonster(item.id);
            //await Navigation.PushAsync(new InfoMonsterPage(new InfoMonsterViewModel(mob)));

            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadListCommand.Execute(null);
        }
    }
}