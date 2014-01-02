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
    //dit is de viewmodel klasse dat hoort bij de view 'homepage'
    class HomePageVM : ObservableObject, IPage //eerst klasse dan pas interface
    {
        public string Name
        {
            get { return "Algemeen"; } //unieke naam!
        }

        public HomePageVM() {
            _festival = Festival.GetData();
            _lineups = LineUp.getAll();
        }

        #region Festival Fields

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

        private ObservableCollection<LineUp> _lineups;
        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                return _lineups;
            }
            set
            {
                _lineups = value;
                OnPropertyChanged("LineUps");
            }

        }

        #endregion

        #region Commands

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


            Boolean TeVroeg = false;
            Boolean TeLaat = false;

            foreach (LineUp lu in _lineups) {
                if (lu.Date < editFestival.StartDate) { 
                    TeVroeg = true;
                }
                if (lu.Date > editFestival.EndDate)
                {
                    TeLaat = true;
                }
            }
            if (TeVroeg == true)
            {
                MessageBox.Show("De wijzigingen werden niet toegepast. Bepaalde Optredens vallen voor de startdatum.");
            }
            else {
                if (TeLaat == true)
                {
                    MessageBox.Show("De wijzigingen werden niet toegepast. Bepaalde Optredens vallen na de einddatum.");
                }
                else {
                    Festival.EditData(editFestival);
                }
            }
            LineUpPageVM.UpdateAll();

        }
    }

        #endregion
}
