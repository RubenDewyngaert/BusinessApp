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
    class LineUpPageVM : ObservableObject, IPage
    {
        public LineUpPageVM(){
        _lineups = LineUp.getAll();
        _podia = Stage.getAll();
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
                if(_geselecteerdeLineup != null){
                    _newLineUp = _geselecteerdeLineup;

                }

                OnPropertyChanged("GeslecteerdLineup");
                OnPropertyChanged("NewLineUp");
            }
        }

        private LineUp _newLineUp;

        public LineUp newLineUp
        {
            get
            {
                return _newLineUp;
            }

            set
            {
                _newLineUp = value;
                OnPropertyChanged("NewLineUp");
            }
        }

        private ObservableCollection<Stage> _podia;

        public ObservableCollection<Stage> Podia
        {
            get
            {
                return _podia;
            }
        }

        private Stage _geselecteerdePodium;
        public Stage GeselecteerdPodium
        {
            get
            {
                return _geselecteerdePodium;
            }

            set
            {
                _geselecteerdePodium = value;
                if (_geselecteerdePodium != null) {
                    _newStage = _geselecteerdePodium.Name;
                    Console.WriteLine("Select: " + _geselecteerdePodium.Name);
                    Console.WriteLine(_newStage);
                }
                OnPropertyChanged("GeslecteerdPodium");
                OnPropertyChanged("NewStage");
            }
        }

        private String _newStage;
        public String NewStage
        {
            get {
                return _newStage;
            }

            set {
                _newStage = value;
                OnPropertyChanged("NewStage");
            }
        }

        public ICommand DeleteStageCommand
        {
            get
            {
                return new RelayCommand(DeleteStage);
            }
        }

        public void DeleteStage()
        {
            NewStage = "";
            Stage.DeleteStage(GeselecteerdPodium);
            Podia.Remove(GeselecteerdPodium);


        }

        public ICommand AddStageCommand
        {
            get
            {
                return new RelayCommand(AddStage);
            }
        }

        public void AddStage()
        {
            Stage.NewStage(NewStage);
            _podia = Stage.getAll();
            OnPropertyChanged("Podia");
        }

        public ICommand EditStageCommand
        {
            get
            {
                return new RelayCommand(EditStage);
            }
        }

        public void EditStage()
        {
            Stage.EditStage(GeselecteerdPodium, NewStage);
            _podia = Stage.getAll();
            OnPropertyChanged("Podia");
        }
        

    }
}
