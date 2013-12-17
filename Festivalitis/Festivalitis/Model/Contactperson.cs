using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class Contactperson
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
        private String _Company;
        public String Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

        private ContactpersonType _JobRole;
        public ContactpersonType JobRole
        {
            get
            {
                return _JobRole;
            }
            set
            {
                _JobRole = value;
            }
        }
        private String _City;
        public String City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }

        private String _Email;
        public String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        private String _Phone;
        public String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }

        private String _Cellphone;
        public String Cellphone
        {
            get
            {
                return _Cellphone;
            }
            set
            {
                _Cellphone = value;
            }
        }
    }
}
