using FestivalApp.model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festivalitis.ViewModel
{
    class TicketPageVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Ticketing"; }
        }

        public TicketPageVM() {
            _types = TicketType.getAll();
        }

        private ObservableCollection<TicketType> _types;
        public ObservableCollection<TicketType> Types {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                OnPropertyChanged("Types");
            }
        }

        private TicketType _geselecteerdType;
        public TicketType GeselecteerdType
        {
            get
            {
                return _geselecteerdType;
            }
            set
            {
                _geselecteerdType = value;
                if (_geselecteerdType != null) {
                    _newType = _geselecteerdType;
                }
                OnPropertyChanged("GeselecteerdType");
                OnPropertyChanged("NewType");
            }
        }

        private TicketType _newType;
        public TicketType NewType
        {
            get
            {
                return _newType;
            }
            set
            {
                _newType = value;
                OnPropertyChanged("NewType");
            }
        }

        #region Type commands

        public ICommand DeleteTypeCommand
        {
            get
            {
                return new RelayCommand(DeleteType);
            }
        }

        public void DeleteType()
        {
            TicketType.DeleteType(GeselecteerdType);
            Types.Remove(GeselecteerdType);
        }

        public ICommand AddTypeCommand
        {
            get
            {
                return new RelayCommand(AddType);
            }
        }

        public void AddType()
        {
            TicketType.NewType(NewType);
            _types = TicketType.getAll();
            OnPropertyChanged("Types");
        }

        public ICommand EditTypeCommand
        {
            get
            {
                return new RelayCommand(EditType);
            }
        }

        public void EditType()
        {
            TicketType.EditType(NewType);
            _types = TicketType.getAll();
            OnPropertyChanged("Types");
        }

        #endregion
    }
}
