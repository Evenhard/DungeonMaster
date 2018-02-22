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
	public partial class NewMonsterPage : ContentPage
	{
        public NewMonsterPage()
        {
            InitializeComponent();
        }

        async void OnClick(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;

            Int32.TryParse(HealthEntry.Text, out var health);
            Int32.TryParse(XPEntry.Text, out var xp);

            var mob = new Monster
            {
                Name = NameEntry.Text,
                Health = health,
                XP = xp,
                Description = DescriptionEditor.Text
            };

            //MessagingCenter.Send(this, "AddItem", Item);
            await App.Database.AddMonster(mob);
            await Navigation.PopModalAsync();
        }
    }
}