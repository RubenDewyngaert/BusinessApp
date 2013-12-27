using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festivalitis.ViewModel
{
    class BandGenrePageVM : ObservableObject, IPage
    {
        public BandGenrePageVM(){
            //_bands = Band.getAll();
            //_genres = Genre.getAll();

        }

        public string Name
        {
            get { return "Genres/Band"; }
        }

    }
}
