using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class ContactpersonType
    {
        private String _ID;
        public String ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten ")]
        private String _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
    }
}
