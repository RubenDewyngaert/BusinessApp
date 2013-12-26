using FestivalApp.model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festivalitis.ViewModel
{
    //dit is de viewmodel klasse dat hoort bij de view 'homepage'
    class HomePageVM : ObservableObject, IPage //eerst klasse dan pas interface
    {


        public string Name
        {
            get { return "Algemeen"; } //unieke naam!
        }

        public HomePageVM() {
            _festival = Festival.GetData();
        }

        private Festival _festival;
        public Festival Festival {
            get {
                return _festival;
            }
            set {
                _festival = value;
                OnPropertyChanged("Festival");
            }
        
        }

        public ICommand EditDataCommand
        {
            get
            {
                return new RelayCommand(EditData);
            }
        }

        private void EditData()
        {
            Festival editFestival = new Festival();
            editFestival.Name = Festival.Name;
            editFestival.StartDate = Festival.StartDate;
            editFestival.EndDate = Festival.EndDate;

            Festival.EditData(editFestival);
        }
    }
}
