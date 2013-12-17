using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivalitis.ViewModel
{

    //elke viewModel-klasse zal deze interface MOETEN implementeren
    //zo kan ik later een lijst van opbjecten van klassen gaan bijhouden waarvan
    // de klasse dze interface implementeert. Deze lijst zit in de klasse ApplicationVM
    interface IPage
    {
        //één property Name. Elke klasse moet deze prop uitwerken.
        string Name { get; }

    }
}
