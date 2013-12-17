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

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
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
                _newGenre = _geselecteerdeGenre.Name;
                Console.WriteLine("Select: " + _geselecteerdeGenre.Name);
                Console.WriteLine(_newGenre);

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

        

        public string Name
        {
            get { return "Bands"; }
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
                Console.WriteLine("Select: " + _geselecteerdeBand.Name);

                OnPropertyChanged("GeselecteerdBand");
            }
        }

        public ICommand DeleteBandCommand
        {
            get { return new RelayCommand(DeleteBand); }
        }

        public ICommand SaveBandCommand
        {
            get { return new RelayCommand(SaveBand); }
        }


        public void DeleteBand()
        {
            //Band.Remove(SelectedBand);
            //Bands.Remove(SelectedBand);
        }

        public void SaveBand()
        {
            //Band.EditBand(SelectedBand);
        }

        public void DeleteGenre()
        {
            //Genre.Remove(SelectedGenre);
            //Genres.Remove(SelectedGenre);
        }

        public ICommand NewBandCommand {
            get {
                return new RelayCommand(AddBand);
            }
        }

        public void AddBand() {
            //Band.NewBand(_newBand);
        }

        public ICommand ClearBandCommand {
            get
            {
                return new RelayCommand(ClearBand);
            }
        }

        public void ClearBand() {
            
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
            //Genre.EditGenre(GeselecteerdGenre.ID, NewGenre);
            _genres = Genre.getAll();
        }
    }
}
