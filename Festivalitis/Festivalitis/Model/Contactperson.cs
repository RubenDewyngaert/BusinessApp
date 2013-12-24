using Festivalitis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
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

        public static ObservableCollection<Contactperson> getAll()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();

            String sSQL = "SELECT * FROM ContactPerson";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Contactperson aNew = new Contactperson();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                //De reader crasht als hij iets leegs inleest, daarom vangen wij dit op met de if's
                if (!DBNull.Value.Equals(reader["Company"]))
                {
                    aNew._Company = reader["Company"].ToString();
                }
                else
                {
                    aNew._Company = null;
                }
                if (!DBNull.Value.Equals(reader["City"]))
                {
                    aNew._City = reader["City"].ToString();
                }
                else
                {
                    aNew._City = null;
                }
                if (!DBNull.Value.Equals(reader["Email"]))
                {
                    aNew._Email = reader["Email"].ToString();
                }
                else
                {
                    aNew._Email = null;
                }
                if (!DBNull.Value.Equals(reader["Phone"]))
                {
                    aNew._Phone = reader["Phone"].ToString();
                }
                else
                {
                    aNew._Phone = null;
                }
                if (!DBNull.Value.Equals(reader["Cellphone"]))
                {
                    aNew._Cellphone = reader["Cellphone"].ToString();
                }
                else
                {
                    aNew._Cellphone = null;
                }
                string typeID = reader["JobRole"].ToString();

                aNew._JobRole = ContactpersonType.getType(typeID);

                
                lijst.Add(aNew);
            }


            return lijst;
        }
    }
}
