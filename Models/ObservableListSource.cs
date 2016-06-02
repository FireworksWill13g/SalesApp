using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SalesApp.Models
{
    class ObservableListSource<T> : ObservableCollection<T>, IListSource where T : BaseModel
    {
        public IBindingList _bindingList;

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList()
        {
            return _bindingList ?? (_bindingList = this.ToBindingList());
        }

        //converts the collection of objects into a binding list (an IBinding List) using the ToBindingList Method
        //Allows us to do data binding on windows forms. 

    }
}
