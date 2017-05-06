using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalContacts
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K GroupHeaderName { get; private set; }

        public Grouping(K headerName, IEnumerable<T> items)
        {
            GroupHeaderName = headerName;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
