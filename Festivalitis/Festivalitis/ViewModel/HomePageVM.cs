using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalitis.ViewModel
{
    //dit is de viewmodel klasse dat hoort bij de view 'homepage'
    class HomePageVM : ObservableObject, IPage //eerst klasse dan pas interface
    {


        public string Name
        {
            get { return "Algemeen"; } //unieke naam!
        }
    }
}
