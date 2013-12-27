using FestivalApp.model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festivalitis.ViewModel
{
    class ContactPageVM : ObservableObject, IPage
    {

        public ContactPageVM(){
            _contactpersonen = Contactperson.getAll();
            _contactpersonTypes = ContactpersonType.getAll();
            _roles = ContactpersonType.getAll();
            _newPerson = new Contactperson();
            _newRole = "";

        }

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

        private ObservableCollection<ContactpersonType> _contactpersonTypes;

        public ObservableCollection<ContactpersonType> ContactPersonTypes
        {
            get
            {
                return _contactpersonTypes;
            }
        }

        private int _typeNumber;

        public int TypeNumber {
            get
            {
                return _typeNumber;
            }

            set
            {
                _typeNumber = value;
                OnPropertyChanged("TypeNumber");
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
                if (_geselecteerdePersoon != null)
                {
                    _newPerson = _geselecteerdePersoon;
                    Console.WriteLine("Select: " + _geselecteerdePersoon.Name);
                    Console.WriteLine("Select: " + _newPerson.Name);

                    int i = 0;

                    foreach (ContactpersonType ct in _contactpersonTypes) {

                        if (ct.Name == _geselecteerdePersoon.JobRole.Name)
                        {
                            _typeNumber = i;
                            break;
                        }
                        i ++;

                    }
                    
                }

                OnPropertyChanged("GeselecteerdPersoon");
                OnPropertyChanged("NewPerson");
                OnPropertyChanged("TypeNumber");
            }
        }

        private Contactperson _newPerson;

        public Contactperson NewPerson
        {
            get
            {
                return _newPerson;
            }

            set
            {
                _newPerson = value;
                OnPropertyChanged("NewPerson");
            }
        }


        private ObservableCollection<ContactpersonType> _roles;

        public ObservableCollection<ContactpersonType> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }

        private ContactpersonType _geselecteerdeRole;
        public ContactpersonType GeselecteerdRole
        {
            get
            {
                return _geselecteerdeRole;
            }

            set
            {
                _geselecteerdeRole = value;
                if (_geselecteerdeRole != null)
                {
                    _newRole = _geselecteerdeRole.Name;
                    Console.WriteLine("Select: " + _geselecteerdeRole.Name);
                    Console.WriteLine(_newRole);
                }

                OnPropertyChanged("GeslecteerdRole");
                OnPropertyChanged("NewRole");
            }
        }

        private String _newRole;

        public String NewRole
        {
            get
            {
                return _newRole;
            }

            set
            {
                _newRole = value;
                OnPropertyChanged("NewRole");
            }
        }

        #region PersonCommands

        public ICommand AddPersonCommand
        {
            get
            {
                return new RelayCommand(AddPerson);
            }
        }

        public void AddPerson()
        {
            NewPerson.JobRole = _contactpersonTypes[_typeNumber];
            Contactperson.NewPerson(NewPerson, NewPerson.JobRole.ID);
            _contactpersonen = Contactperson.getAll();
            OnPropertyChanged("ContactPersonen");
        }

        public ICommand EditPersonCommand
        {
            get
            {
                return new RelayCommand(EditPerson);
            }
        }

        public void EditPerson()
        {
            NewPerson.JobRole = _contactpersonTypes[_typeNumber];
            Contactperson.EditPerson(NewPerson, NewPerson.JobRole.ID);
            _contactpersonen = Contactperson.getAll();
            OnPropertyChanged("ContactPersonen");
        }

        public ICommand DeletePersonCommand
        {
            get
            {
                return new RelayCommand(DeletePerson);
            }
        }

        public void DeletePerson()
        {
            Contactperson.DeletePerson(GeselecteerdPersoon);
            ContactPersonen.Remove(GeselecteerdPersoon);
        }


        public ICommand DeleteRoleCommand
        {
            get
            {
                return new RelayCommand(DeleteRole);
            }
        }

        #endregion

        public void DeleteRole()
        {
            Boolean verwijder = true;

            foreach (Contactperson cp in _contactpersonen) {
                Console.WriteLine(cp.Name + cp.JobRole.ID + "1  2" +  GeselecteerdRole.ID);
                if (cp.JobRole.ID == GeselecteerdRole.ID)
                {
                    verwijder = false;
                }
            }

            if (verwijder == true)
            {
                NewRole = "";
                ContactpersonType.DeleteRole(GeselecteerdRole);
                Roles.Remove(GeselecteerdRole);
            }
            else
            {
                MessageBox.Show("Deze functie kan niet verwijderd worden omdat deze nog gebruikt wordt.");
            }

        }

        public ICommand AddRoleCommand
        {
            get
            {
                return new RelayCommand(AddRole);
            }
        }

        public void AddRole()
        {
            ContactpersonType.NewRole(NewRole);
            _roles = ContactpersonType.getAll();
            OnPropertyChanged("Roles");

            if (_geselecteerdePersoon != null)
            {
                int i = 0;

                foreach (ContactpersonType ct in _contactpersonTypes)
                {

                    if (ct.Name == _geselecteerdePersoon.JobRole.Name)
                    {
                        _typeNumber = i;
                        break;
                    }
                    i++;

                }

            }

            OnPropertyChanged("TypeNumber");
        }

        public ICommand EditRoleCommand
        {
            get
            {
                return new RelayCommand(EditRole);
            }
        }

        public void EditRole()
        {
            ContactpersonType.EditRole(GeselecteerdRole, NewRole);
            _roles = ContactpersonType.getAll();
            OnPropertyChanged("Roles");
        }

        private String _searchTerm;

        public String SearchTerm
        {
            get
            {
                return _searchTerm;
            }

            set
            {
                _searchTerm = value;
                OnPropertyChanged("SearchTerm");
            }
        }

        public ICommand SearchContactCommand {
            get {
                return new RelayCommand(SearchContact);
            }
        }


        public void SearchContact() {
            ObservableCollection<Contactperson> cpTotal = Contactperson.getAll();
            ObservableCollection<Contactperson> cplijst = new ObservableCollection<Contactperson>();

            foreach (Contactperson cp in cpTotal) {
                if (cp.Name.ToLower().Contains(_searchTerm.ToLower())) {

                    cplijst.Add(cp);
                }
            }     
            _contactpersonen = cplijst;
            OnPropertyChanged("ContactPersonen");
        }

        public ICommand SearchContactClear {
            get
            {
                return new RelayCommand(SearchClear);
            }
        }

        public void SearchClear()
        {
            _contactpersonen = Contactperson.getAll();
            _searchTerm = "";
            OnPropertyChanged("ContactPersonen");
            OnPropertyChanged("SearchTerm");
        }
        
    }
}
