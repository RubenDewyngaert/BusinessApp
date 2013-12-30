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
            _bands = Band.getAll();
            _stages = Stage.getAll();
            _newLineUp = new LineUp();
            _newStage = "";
            _festival = Festival.GetData();
            _start = _festival.StartDate;
            _end = _festival.EndDate;
            _newLineUp.Date = _festival.StartDate;
            _data = Start;
        }

        public static void UpdateAll()
        {
            _bands = Band.getAll();
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

        private static ObservableCollection<Band> _bands;
        public ObservableCollection<Band> Bands
        {
            get
            {
                return _bands;
            }
            set
            {
                _bands = value;
                OnPropertyChanged("Bands");
            }
        }

        private DateTime _data;

        public DateTime Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private Festival _festival;

        public Festival Festival
        {
            get
            {
                return _festival;
            }
            set
            {
                _festival = value;
                OnPropertyChanged("Festival");
            }
        }

        private DateTime _start;

        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                OnPropertyChanged("Start");
            }
        }

        private DateTime _end;

        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
                OnPropertyChanged("End");
            }
        }



        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get
            {
                return _stages;
            }

            set
            {
                _stages = value;
                OnPropertyChanged("Stages");
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

                    int i = 0;
                    int j = 0;

                    foreach (Band b in _bands) {
                        if (b.ID == _geselecteerdeLineup.Band.ID)
                        {
                            _bandNumber = i;
                            break;
                        }
                        i++;
                    }

                    foreach (Stage s in _stages) {
                        if (s.ID == _geselecteerdeLineup.Stage.Name)
                        {
                            _stageNumber = j;
                            break;
                        }
                        j++;
                    }



                }

                OnPropertyChanged("GeslecteerdLineup");
                OnPropertyChanged("NewLineUp");
                OnPropertyChanged("BandNumber");
                OnPropertyChanged("StageNumber");
            }
        }
        private int _bandNumber;

        public int BandNumber
        {
            get
            {
                return _bandNumber;
            }

            set
            {
                _bandNumber = value;
                OnPropertyChanged("BandNumber");
            }
        }

        private int _stageNumber;

        public int StageNumber
        {
            get
            {
                return _stageNumber;
            }

            set
            {
                _stageNumber = value;
                OnPropertyChanged("StageNumber");
            }
        }

        private int _dayNumber;

        public int DayNumber
        {
            get
            {
                return _dayNumber;
            }

            set
            {
                _dayNumber = value;
                OnPropertyChanged("DayNumber");
            }
        }

        private LineUp _newLineUp;

        public LineUp NewLineUp
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


        //Bewerken van de Lineup ADD, EDIT & DELETE
        public ICommand AddLineUpCommand
        {
            get
            {
                return new RelayCommand(AddLineUp);
            }
        }

        public void AddLineUp()
        {
            NewLineUp.Band = _bands[_bandNumber];
            NewLineUp.Stage = _stages[_stageNumber];
            LineUp.NewLineUp(NewLineUp, NewLineUp.Stage.ID, NewLineUp.Band.ID);
            _lineups = LineUp.getAll();
            OnPropertyChanged("LineUps");
        }

        public ICommand EditLineUpCommand
        {
            get
            {
                return new RelayCommand(EditLineUp);
            }
        }

        public void EditLineUp()
        {
            NewLineUp.Band = _bands[_bandNumber];
            NewLineUp.Stage = _stages[_stageNumber];
            LineUp.EditLineUp(NewLineUp, NewLineUp.Stage.ID, NewLineUp.Band.ID);
            _lineups = LineUp.getAll();
            OnPropertyChanged("LineUps");
        }

        public ICommand DeleteLineUpCommand
        {
            get
            {
                return new RelayCommand(DeleteLineUp);
            }
        }

        public void DeleteLineUp()
        {
            LineUp.DeleteLineUp(GeselecteerdLineup);
            Lineups.Remove(GeselecteerdLineup);
        }

        #region search

        public ICommand SearchLineUpCommand
        {
            get
            {
                return new RelayCommand(SearchLineUp);
            }
        }


        public ICommand ClearSearchCommand
        {
            get
            {
                return new RelayCommand(ClearSearch);
            }
        }

        public void SearchLineUp()
        {
            ObservableCollection<LineUp> Searchups = LineUp.getAll();
            ObservableCollection<LineUp> Resultups = new ObservableCollection<LineUp>();
            foreach (LineUp lu in Searchups) {
                if (lu.Date == Data)
                {
                    Resultups.Add(lu);
                }
            }

            _lineups = Resultups;
            OnPropertyChanged("LineUps");
        }

        public void ClearSearch() {
            _lineups = LineUp.getAll();
            OnPropertyChanged("LineUps");
        }


        #endregion 

    }
}
