using Festivalitis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class Band: IDataErrorInfo
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

        private String _Name;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten ")]
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
        private byte[] _Picture;
        public byte[] Picture
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
        [Url(ErrorMessage = "Dit moet een geldige URL zijn")]
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
        [Url(ErrorMessage = "Dit moet een geldige URL zijn")]
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

        #region DataValidatie

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }


        public string Error
        {
            get { return "Model not valid"; }
        }



        //string IDataErrorInfo.Error
        //{
        //    get { return "Model not valid"; }
        //}

        //string IDataErrorInfo.this[string columnName]
        //{
        //    get
        //    {
        //        try
        //        {
        //            object value = this.GetType().GetProperty(columnName).GetValue(this);
        //            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        //            {
        //                MemberName = columnName
        //            });
        //        }
        //        catch (ValidationException ex)
        //        {
        //            return ex.Message;
        //        }
        //        return String.Empty;
        //    }
        //}

        #endregion 


        public static ObservableCollection<Band> getAll()
        {
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();

            String sSQL = "SELECT * FROM Bands";
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
                    aNew.Picture = (byte[])reader["Picture"];
                }
                else
                {
                    aNew.Picture = new byte[0];
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

                aNew.Genres = Genre.getByBand(aNew.ID);

                lijst.Add(aNew);
            }


            return lijst;
        }

        public static Band getBand(String id)
        {
            Band band = new Band();

            String sSQL = "SELECT * FROM Bands WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", id);
            if (par1.Value == null) par1.Value = DBNull.Value;
            DbDataReader reader = Database.GetData(sSQL, par1);
            while (reader.Read())
            {
                band.Name = reader["Name"].ToString();
                band.ID = id;
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
                    band.Picture = (byte[])reader["Picture"];
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
                band.Genres = Genre.getByBand(id);
            }


            return band;
        }

       
        public static void NewBand(Band band)
        {
            String sql = "INSERT INTO Bands (Name, Picture, Description, Twitter, Facebook) VALUES(@Name, @Picture, @Description, @Twitter, @Facebook)";
            DbParameter par1 = Database.AddParameter("@Name", band._Name);
            DbParameter par2 = Database.AddParameter("@Picture", band.Picture);
            DbParameter par3 = Database.AddParameter("@Description", band.Description);
            DbParameter par4 = Database.AddParameter("@Twitter", band.Twitter);
            DbParameter par5 = Database.AddParameter("@Facebook", band.Facebook);
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteBand(Band band)
        {
            String sql = "DELETE FROM Bands WHERE ID = @Band";
            DbParameter par1 = Database.AddParameter("@Band", band._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditBand(Band band)
        {
            String sql = "UPDATE Bands Set Name=@Name, Description=@Description, Twitter=@Twitter, Facebook=@Facebook WHERE ID=@Band";
            DbParameter par1 = Database.AddParameter("@Band", band._ID);
            DbParameter par2 = Database.AddParameter("@Name", band._Name);
            DbParameter par4 = Database.AddParameter("@Description", band._Description);
            DbParameter par5 = Database.AddParameter("@Twitter", band._Twitter);
            DbParameter par6 = Database.AddParameter("@Facebook", band._Facebook);
            if (par4.Value == null) par4.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par4, par5, par6);
        }

        public static void EditPicture(Band band, Byte[] picture)
        {
            String sql = "UPDATE Bands Set Picture=@Picture WHERE ID=@Band";
            DbParameter par1 = Database.AddParameter("@Band", band._ID);
            DbParameter par3 = Database.AddParameter("@Picture", picture);
            if (par3.Value == null) par3.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par3);
        }

        public static void NewBandGenre(Band band, Genre genre)
        {
            String sql = "INSERT INTO BandGenre (BandId, GenreId) VALUES(@Band, @Genre)";
            DbParameter par1 = Database.AddParameter("@Band", band.ID);
            DbParameter par2 = Database.AddParameter("@Genre", genre.ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2);
        }

        public static void DeleteBandGenre(Band band, Genre genre)
        {
            String sql = "DELETE FROM BandGenre WHERE BandId = @Band AND GenreId = @Genre";
            DbParameter par1 = Database.AddParameter("@Band", band.ID);
            DbParameter par2 = Database.AddParameter("@Genre", genre.ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2);
        }

        public override string ToString()
        {
            return this.Name;
        }


    }
}
