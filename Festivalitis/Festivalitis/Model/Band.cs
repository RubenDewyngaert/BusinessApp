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
    class Band
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
        private String _Picture;
        public String Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                _Picture = value;
            }
        }

        private String _Description;
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        private String _Twitter;
        public String Twitter
        {
            get
            {
                return _Twitter;
            }
            set
            {
                _Twitter = value;
            }
        }

        private String _Facebook;
        public String Facebook
        {
            get
            {
                return _Facebook;
            }
            set
            {
                _Facebook = value;
            }
        }
        private ObservableCollection<Genre> _Genres;
        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _Genres;
            }
            set
            {
                _Genres = value;
            }
        }

        public static ObservableCollection<Band> getAll()
        {
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();

            String sSQL = "SELECT * FROM Band";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Band aNew = new Band();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                //De reader crasht als hij iets leegs inleest, daarom vangen wij dit op met de if's
                if (!DBNull.Value.Equals(reader["Description"]))
                {
                    aNew.Description = reader["Description"].ToString();
                }
                else
                {
                    aNew.Description = null;
                }
                if (!DBNull.Value.Equals(reader["Picture"]))
                {
                    aNew.Picture = "Images/" + reader["Picture"].ToString() + ".jpg";
                }
                else
                {
                    aNew.Picture = null;
                }
                if (!DBNull.Value.Equals(reader["Twitter"]))
                {
                    aNew.Twitter = reader["Twitter"].ToString();
                }
                else
                {
                    aNew.Twitter = null;
                }
                if (!DBNull.Value.Equals(reader["Facebook"]))
                {
                    aNew.Facebook = reader["Facebook"].ToString();
                }
                else
                {
                    aNew.Facebook = null;
                }
                lijst.Add(aNew);
            }


            return lijst;
        }

        /*public static Band getBand(String bandId){
            Band band = new Band();
            String sSQL = "SELECT * FROM Band WHERE id=@filter";
            DbParameter filter = Database.AddParameter("@filter", bandId);
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
                band.ID = reader["ID"].ToString();
            band.Name = reader["Name"].ToString();
                if (!DBNull.Value.Equals(reader["Description"]))
                {
                    band.Description = reader["Description"].ToString();
                }
                else
                {
                    band.Description = null;
                }
            if (!DBNull.Value.Equals(reader["Picture"]))
            {
                band.Picture = "Images/" + reader["Picture"].ToString() + ".jpg";
            }
            else
            {
                band.Picture = null;
            }
            if (!DBNull.Value.Equals(reader["Twitter"]))
            {
                band.Twitter = reader["Twitter"].ToString();
            }
            else
            {
                band.Twitter = null;
            }
            if (!DBNull.Value.Equals(reader["Facebook"]))
            {
                band.Facebook = reader["Facebook"].ToString();
            }
            else
            {
                band.Facebook = null;
            }

            return band;
        }*/

    }
}
