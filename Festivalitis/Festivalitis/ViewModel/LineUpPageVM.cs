using FestivalApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalitis.ViewModel
{
    class LineUpPageVM : ObservableObject, IPage
    {
        public LineUpPageVM(){
        _lineups = LineUp.getAll();
        //_podia = Stage.getAll();
        }

        public string Name
        {
            get { return "LineUp"; }
        }

        private ObservableCollection<LineUp> _lineups;

        public ObservableCollection<LineUp> Lineups
        {
            get
            {
                return _lineups;
            }
        }

        private LineUp _geselecteerdeLineup;
        public LineUp GeselecteerdLineup
        {
            get
            {
                return _geselecteerdeLineup;
            }

            set
            {
                _geselecteerdeLineup = value;

                OnPropertyChanged("GeslecteerdLineup");
            }
        }

        private ObservableCollection<LineUp> _podia;

        public ObservableCollection<LineUp> Podia
        {
            get
            {
                return _podia;
            }
        }

        private LineUp _geselecteerdePodium;
        public LineUp GeselecteerdPodium
        {
            get
            {
                return _geselecteerdePodium;
            }

            set
            {
                _geselecteerdePodium = value;

                OnPropertyChanged("GeslecteerdPodium");
            }
        }
    }
}
