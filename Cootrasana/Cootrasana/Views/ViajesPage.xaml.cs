﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cootrasana.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViajesPage : ContentPage
	{
		public ViajesPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

	}
}