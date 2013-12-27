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

    class BandPageVM : ObservableObject, IPage
    {
        public BandPageVM(){
            _bands = Band.getAll();
            _genres = Genre.getAll();

        }

        public string Name
        {
            get { return "Bands"; }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                OnPropertyChanged("Genres");
            }
        }

        private Genre _geselecteerdeGenre;
        public Genre GeselecteerdGenre
        {
            get
            {
                return _geselecteerdeGenre;
            }

            set
            {
                _geselecteerdeGenre = value;
                if (_geselecteerdeGenre != null) {
                    _newGenre = _geselecteerdeGenre.Name;
                    Console.WriteLine("Select: " + _geselecteerdeGenre.Name);
                    Console.WriteLine(_newGenre);
                }


                //deze is nodig zodat dit kan geupdate worden, de controls worden zo geupdate -->>> NOODZAKELIJK
                OnPropertyChanged("GeslecteerdGenre");
                OnPropertyChanged("NewGenre");
            }
        }

        private String _newGenre;

        public String NewGenre
        {
            get
            {
                return _newGenre;
            }

            set
            {
                _newGenre = value;
                OnPropertyChanged("NewGenre");
            }
        }

        private Band _newBand;

        public Band NewBand{
            get
            {
                return _newBand;
            }

            set
            {
                _newBand = value;
                OnPropertyChanged("NewBand");
            }
        }

        private ObservableCollection<int> _genreNumbers;

        public ObservableCollection<int> GenreNumbers {
            get
            {
                return _genreNumbers;
            }

            set
            {
                _genreNumbers = value;
                OnPropertyChanged("GenreNumber");
            }
        }
        

        

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get
            {
                return _bands;
            }
        }

        private Band _geselecteerdeBand;
        public Band GeselecteerdBand
        {
            get
            {
                return _geselecteerdeBand;
            }

            set
            {

                _geselecteerdeBand = value;
                if (_geselecteerdeBand != null)
                {
                    _newBand = _geselecteerdeBand;
                    Console.WriteLine("Select: " + _geselecteerdeBand.Name);
                    Console.WriteLine(_newBand.Name);

                    foreach (Genre g in _geselecteerdeBand.Genres)
                    {
                        int i = 0;
                        foreach (Genre gbg in _genres) {
                            if (gbg == g)
                            {
                                _genreNumbers.Add(i);
                                break;
                            }
                            i++;
                        }

                    }

                }

                OnPropertyChanged("GeselecteerdBand");
                OnPropertyChanged("NewBand");
                OnPropertyChanged("GenreNumbers");

            }
        }

        #region BandCommands

        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand(DeleteBand); }
        }

        public ICommand EditBandCommand
        {
            get { return new RelayCommand(EditBand); }
        }

        public ICommand NewBandCommand
        {
            get
            {
                return new RelayCommand(AddBand);
            }
        }


        public void DeleteBand()
        {
            Band.DeleteBand(GeselecteerdBand);
            _bands.Remove(GeselecteerdBand);
        }

        public void EditBand()
        {
            Band.EditBand(NewBand);
            _bands = Band.getAll();
            OnPropertyChanged("Bands");
        }

        public void AddBand() 
        {
            Band.NewBand(NewBand);
            _bands = Band.getAll();
            OnPropertyChanged("Bands");
        }

        #endregion

        #region Genre commands

        public ICommand DeleteGenreCommand {
            get
            {
                return new RelayCommand(DeleteGenre);
            }
        }

        public void DeleteGenre()
        {
            NewGenre = "";
            Genre.DeleteGenre(GeselecteerdGenre);
            Genres.Remove(GeselecteerdGenre);


        }

        public ICommand AddGenreCommand {
            get
            {
                return new RelayCommand(AddGenre);
            }
        }

        public void AddGenre() {
            Genre.NewGenre(NewGenre);
            _genres = Genre.getAll();
            OnPropertyChanged("Genres");
        }

        public ICommand EditGenreCommand
        {
            get
            {
                return new RelayCommand(EditGenre);
            }
        }

        public void EditGenre()
        {
            Genre.EditGenre(GeselecteerdGenre, NewGenre);
            _genres = Genre.getAll();
            OnPropertyChanged("Genres");
        }

#endregion
    }
}
