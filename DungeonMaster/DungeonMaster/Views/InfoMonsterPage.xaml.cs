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
	public partial class InfoMonsterPage : ContentPage
	{
        InfoMonsterViewModel viewModel;

        public InfoMonsterPage(InfoMonsterViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnClick(object sender, EventArgs e)
        {
            DeleteButton.IsEnabled = false;
            var mob = viewModel.Mob;

            MessagingCenter.Send(this, "DeleteMob", mob);
            await Navigation.PopAsync();
        }
    }
}