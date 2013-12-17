using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalitis.ViewModel
{
    class ObservableObject : INotifyPropertyChanged
    {
        // Deze methode (gebasseerd op curcus) roepen we aan van zodra eenproperty wijzigt
        protected void OnPropertyChanged(string propertyName)
        {
            //controle of event (vuurpijl) beschikbaar is
            if (PropertyChanged != null)
            {
                //vuurpijl afschieten en de naam doorgeven
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
