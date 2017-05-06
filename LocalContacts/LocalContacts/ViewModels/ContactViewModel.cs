using Plugin.Contacts;
using Plugin.Contacts.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalContacts.ViewModels
{
	public class ContactViewModel : BaseViewModel
	{
		#region Constructor

		public ContactViewModel()
		{

		}

        #endregion

        #region Properties

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                SetProperty(ref _searchQuery, value);
                FilterContacts();
            }
        }

        private List<Contact> _contactItems;
		public List<Contact> ContactItems
		{
			get { return _contactItems; }
			set { SetProperty(ref _contactItems, value); }
		}

		ObservableCollection<Grouping<string, Contact>> _contactsHeader;
		public ObservableCollection<Grouping<string, Contact>> ContactsHeaders
		{
			get
			{
				return _contactsHeader;
			}
			set
			{
				SetProperty(ref _contactsHeader, value);
			}
		}

        #endregion

        #region Commands

        public ICommand LoadContactsCommand { get { return new Command(OnLoadContacts); } }

		#endregion

		#region Support Methods

		async void OnLoadContacts(object obj)
		{
			try
			{
				IsBusy = true;
				//Requesting / Checking Permission
				if (await CrossContacts.Current.RequestPermission())
				{
					CrossContacts.Current.PreferContactAggregation = false;

					await Task.Run(() =>
					{
						if (CrossContacts.Current.Contacts == null)
							return;

						ContactItems = CrossContacts.Current.Contacts.ToList()
						  .Where(c => !string.IsNullOrWhiteSpace(c.LastName) && c.Phones.Count > 0) //It has known bug that's why making call this way
						  .ToList();
                        //Sorted / Filter
                        FilterContacts();
						IsBusy = false;
					});
				}
			}
			catch (Exception ex)
			{
				//Log Exception
			}
			finally
			{
				IsBusy = false;
			}
		}

        void FilterContacts()
        {
            List<Contact> _items;
            if(!string.IsNullOrEmpty(SearchQuery))
            {
                _items = ContactItems.Where(x => x.FirstName.ToLower().Contains(SearchQuery) || x.LastName.ToLower().Contains(SearchQuery)).ToList();
            }
            else
            {
                _items = ContactItems.ToList();
            }
            var sorted = from menu in _items
                         orderby menu.FirstName ascending
                         group menu by menu.FirstName into menuGroup //If we we need to group by some other property then we can replace FirstName with that Property
                         select new Grouping<string, Contact>(menuGroup.Key, menuGroup);
            ContactsHeaders = new ObservableCollection<Grouping<string, Contact>>(sorted);
        }

		#endregion
	}
}
