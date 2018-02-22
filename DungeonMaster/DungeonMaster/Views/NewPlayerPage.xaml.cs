using DungeonMaster.Models;
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
	public partial class NewPlayerPage : ContentPage
	{
        public NewPlayerPage()
        {
            InitializeComponent();
        }

        async void OnClick(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;

            var hero = new Player
            {
                Name = NameEntry.Text,
                PlayerName = PlayerEntry.Text,
                Class = ClassEntry.Text
            };

            //MessagingCenter.Send(this, "AddItem", Item);
            await App.Database.AddHero(hero);
            await Navigation.PopModalAsync();
        }
    }
}