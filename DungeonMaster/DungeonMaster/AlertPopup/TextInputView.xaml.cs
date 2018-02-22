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
	public partial class TextInputView : ContentView
	{
        public EventHandler CloseButtonEventHandler { get; set; }
        public int TextInputResult { get; set; }

        public static readonly BindableProperty IsValidationLabelVisibleProperty =
            BindableProperty.Create(nameof(IsValidationLabelVisible), typeof(bool), typeof(TextInputView), false, BindingMode.OneWay, null,
            (bindable, value, newValue) =>
            {
                if ((bool)newValue)
                {
                    ((TextInputView)bindable).ValidationLabel.IsVisible = true;
                }
                else
                {
                    ((TextInputView)bindable).ValidationLabel.IsVisible = false;
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

        public TextInputView(string titleText)
        {
            InitializeComponent();

            TitleLabel.Text = titleText;
            InputEntry.Placeholder = "0";
            CloseButton.Text = "Ок";
            ValidationLabel.Text = "Поле ввода не должно быть пустым!";

            CloseButton.Clicked += CloseButton_Clicked;
            InputEntry.TextChanged += InputEntry_TextChanged;
        }

        public void RemoveTap(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(InputEntry.Text))
                    InputEntry.Text = "0";

                var number = Int32.Parse(InputEntry.Text);
                number = number > 0 ? --number : number;
                InputEntry.Text = number.ToString();
            }
            catch { }
        }

        public void AddTap(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(InputEntry.Text))
                    InputEntry.Text = "0";

                var number = Int32.Parse(InputEntry.Text);
                ++number;
                InputEntry.Text = number.ToString();
            }
            catch { }
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            CloseButtonEventHandler?.Invoke(this, e);
        }

        private void InputEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var resultSuccess = Int32.TryParse(InputEntry.Text, out var result);
            if (resultSuccess)
                TextInputResult = result;
            else
                TextInputResult = 0;
        }
    }
}