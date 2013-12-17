using Festivalitis.View;
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
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();

            //hieronder voegen we al een eerst IPage-object toe
            //bij nieuwe pages moet deze lisjt aangevuld worden met telkens de bijhoorden viewmodelklasse
            _pages.Add(new HomePageVM());
            _pages.Add(new BandPageVM());
            _pages.Add(new ContactPageVM());
            _pages.Add(new LineUpPageVM());
            _pages.Add(new TicketPageVM());


            //default zetten we de CurrentPage in op de eerste IPage
            _currentPage = Pages[0];


        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }

        }

        private ObservableCollection<IPage> _pages;
        public ObservableCollection<IPage> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                OnPropertyChanged("Pages");
            }
        }

        //volgende twee commands > curcus > ze zorgen voor de buttons op mainwindow.xaml en kan de juiste commando activeren, hier is dat wisselen van pagina

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }



    }
}
