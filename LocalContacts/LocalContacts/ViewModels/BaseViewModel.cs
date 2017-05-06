using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LocalContacts.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Constructor

        public BaseViewModel()
        {
            //xDoerDetail = new XDoer();
            //XDoerList = new List<XDoer>();
        }

        #endregion

        #region Internal Properties

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }


        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        #endregion

        #region INotifyPropertiesChanged Implementation 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Object.Equals(property, value))
            {
                property = value;
                this.OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
