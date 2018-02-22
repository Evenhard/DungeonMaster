using DungeonMaster.Services;
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
	public partial class MainPage : TabbedPage
    {
		public MainPage ()
		{
			InitializeComponent ();
            Task.Run(async () => await AddTestFighters.JustDoIt());
        }
    }
}