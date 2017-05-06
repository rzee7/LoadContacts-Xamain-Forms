using LocalContacts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalContacts
{
    public partial class MainPage : ContentPage
    {
        public ContactViewModel ViewModel { get { return BindingContext as ContactViewModel; } }
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadContactsCommand.Execute(null);
        }
    }
}
