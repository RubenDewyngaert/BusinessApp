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
        public static void NewBand(Band band)
        {
            String sql = "INSERT INTO Band (Name, Picture, Description, Twitter, Facebook) VALUES(@Name, @Picture, @Description, @Twitter, @Facebook)";
            DbParameter par1 = Database.AddParameter("@Name", band._Name);
            DbParameter par2 = Database.AddParameter("@Picture", band._Name);
            DbParameter par3 = Database.AddParameter("@Description", band._Name);
            DbParameter par4 = Database.AddParameter("@Twitter", band._Name);
            DbParameter par5 = Database.AddParameter("@Facebook", band._Name);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteBand(Band band)
        {
            String sql = "DELETE FROM Band WHERE ID = @Band";
            DbParameter par1 = Database.AddParameter("@Band", band._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditBand(Band band)
        {
            String sql = "UPDATE Stage Set Name=@naam Stage=@Stage Picture=@Picture Description=@Description Twitter=@Twitter Facebook=@Facebook WHERE ID=@Band";
            DbParameter par1 = Database.AddParameter("@Band", band._ID);
            DbParameter par2 = Database.AddParameter("@Name", band._Name);
            DbParameter par3 = Database.AddParameter("@Picture", band._Picture);
            DbParameter par4 = Database.AddParameter("@Description", band._Description);
            DbParameter par5 = Database.AddParameter("@Twitter", band._Twitter);
            DbParameter par6 = Database.AddParameter("@Facebook", band._Facebook);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5, par6);
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
