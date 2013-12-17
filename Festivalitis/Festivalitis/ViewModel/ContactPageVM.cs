using FestivalApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalitis.ViewModel
{
    class ContactPageVM : ObservableObject, IPage
    {
        //_contactpersonen = ContactPerson.getAll();

        public string Name
        {
            get { return "Contactpersonen"; }
        }

        private ObservableCollection<Contactperson> _contactpersonen;

        public ObservableCollection<Contactperson> ContactPersonen
        {
            get
            {
                return _contactpersonen;
            }
        }

        private Contactperson _geselecteerdePersoon;
        public Contactperson GeselecteerdPersoon
        {
            get
            {
                return _geselecteerdePersoon;
            }

            set
            {
                _geselecteerdePersoon = value;
                Console.WriteLine("Select: " + _geselecteerdePersoon.Name);

                OnPropertyChanged("GeslecteerdPersoon");
            }
        }
    }
}
