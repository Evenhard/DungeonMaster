using DungeonMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DungeonMaster.AlertPopup
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFighterView : ContentView
	{
        public EventHandler CloseButtonEventHandler { get; set; }
        public Fighter FighterResult { get; set; }

        public static readonly BindableProperty IsValidationLabelVisibleProperty =
            BindableProperty.Create(nameof(IsValidationLabelVisible), typeof(bool), typeof(AddFighterView), false, BindingMode.OneWay, null,
            (bindable, value, newValue) =>
            {
                if ((bool)newValue)
                {
                    ((AddFighterView)bindable).ValidationLabel.IsVisible = true;
                }
                else
                {
                    ((AddFighterView)bindable).ValidationLabel.IsVisible = false;
                }
            });

        public bool IsValidationLabelVisible
        {
            get
            {
                return (bool)GetValue(IsValidationLabelVisibleProperty);
            }
            set
            {
                SetValue(IsValidationLabelVisibleProperty, value);
            }
        }

        private bool isMonster;

        public AddFighterView(string titleText, List<Player> heroes)
        {
            InitializeComponent();

            FighterResult = new Fighter();
            isMonster = false;
            FighterPicker.ItemsSource = heroes;
            FighterPicker.ItemDisplayBinding = new Binding("Name");

            TitleLabel.Text = titleText;
            InputEntry.Placeholder = "0";
            CloseButton.Text = "Ок";
            ValidationLabel.Text = "Поля ввода не должны быть пустыми или иметь отрицательное значение!";

            CloseButton.Clicked += CloseButton_Clicked;
            InputEntry.TextChanged += InputEntry_TextChanged;
            FighterPicker.SelectedIndexChanged += picker_SelectedIndexChanged;
        }

        public AddFighterView(string titleText, List<Monster> mobs)
        {
            InitializeComponent();

            FighterResult = new Fighter();
            isMonster = true;
            FighterPicker.ItemsSource = mobs;
            FighterPicker.ItemDisplayBinding = new Binding("Name");

            TitleLabel.Text = titleText;
            InputEntry.Placeholder = "0";
            CloseButton.Text = "Ок";
            ValidationLabel.Text = "Поля ввода не должны быть пустыми или иметь отрицательное значение!";

            CloseButton.Clicked += CloseButton_Clicked;
            InputEntry.TextChanged += InputEntry_TextChanged;
            FighterPicker.SelectedIndexChanged += picker_SelectedIndexChanged;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            CloseButtonEventHandler?.Invoke(this, e);
        }

        private void InputEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputEntry.Text))
                InputEntry.Text = "0";
            Int32.TryParse(InputEntry.Text, out var initiative);
            FighterResult.Initiative = initiative;
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isMonster)
            {
                var mob = FighterPicker.SelectedItem as Monster;

                FighterResult.id = mob.Id;
                FighterResult.Name = mob.Name;
                FighterResult.Health = mob.Health;
                FighterResult.isMonster = true;
                FighterResult.XP = mob.XP;
                FighterResult.Description = mob.Description;
            }
            else
            {
                var hero = FighterPicker.SelectedItem as Player;

                FighterResult.Name = hero.Name;
                FighterResult.isMonster = false;
                FighterResult.Class = hero.Class;
            }
        }
    }
}