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
    class BandGenrePageVM : ObservableObject, IPage
    {
        public BandGenrePageVM(){
            _bands = Band.getAll();
            _genres = Genre.getAll();
            _bandGenres = Genre.getAll();
            _toevoegen = false;

        }

        public string Name
        {
            get { return "Genres/Band"; }
        }

        private ObservableCollection<Band> _bands;
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
                _bandGenres = GeselecteerdBand.Genres;
                OnPropertyChanged("GeselecteerdBand");
                OnPropertyChanged("BandGenres");
            }
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

        private ObservableCollection<Genre> _bandGenres;
        public ObservableCollection<Genre> BandGenres
        {
            get
            {
                return _bandGenres;
            }
            set
            {
                _bandGenres = value;
                OnPropertyChanged("BandGenres");
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
                Toevoegen = true;
                foreach (Genre g in BandGenres) {
                    if (_geselecteerdeGenre.ID == g.ID)
                    {
                        Toevoegen = false;
                    }
                }
                OnPropertyChanged("Toevoegen");
                OnPropertyChanged("GeselecteerdGenre");
                Console.WriteLine(GeselecteerdGenre);
                Console.WriteLine(Toevoegen);
            }
        }

        private Genre _geselecteerdeBandGenre;
        public Genre GeselecteerdBandGenre
        {
            get
            {
                return _geselecteerdeBandGenre;
            }

            set
            {
                _geselecteerdeBandGenre = value;
                OnPropertyChanged("GeselecteerdBandGenre");
                Console.WriteLine(GeselecteerdBandGenre);
            }
        }

        private Boolean _toevoegen;
        public Boolean Toevoegen
        {
            get
            {
                return _toevoegen;
            }

            set
            {
                _toevoegen = value;
                OnPropertyChanged("Toevoegen");
            }
        }

        public ICommand DeleteGenreCommand
        {
            get
            {
                return new RelayCommand(DeleteGenre);
            }
        }

        public void DeleteGenre()
        {
            Band.DeleteBandGenre(GeselecteerdBand ,GeselecteerdBandGenre);
            BandGenres.Remove(GeselecteerdBandGenre);
        }

        public ICommand AddGenreCommand
        {
            get
            {
                return new RelayCommand(AddGenre);
            }
        }

        public void AddGenre()
        {
            Band.NewBandGenre(GeselecteerdBand, GeselecteerdGenre);
            _bandGenres.Add(GeselecteerdGenre);
            OnPropertyChanged("BandGenres");
        }
    }
}
