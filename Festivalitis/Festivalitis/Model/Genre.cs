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
    class Genre: IDataErrorInfo
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


        public static ObservableCollection<Genre> getAll()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();

            String sSQL = "SELECT * FROM Genre";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Genre aNew = new Genre();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                
                lijst.Add(aNew);
            }


            return lijst;
        }

        public static ObservableCollection<Genre> getByBand(string BandID)
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();

            String sSQL = "SELECT * FROM BandGenre WHERE BandId = @BID";
            DbParameter par1 = Database.AddParameter("@BID", BandID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            DbDataReader reader = Database.GetData(sSQL, par1);
            while (reader.Read())
            {

                lijst.Add(getById(reader["GenreID"].ToString()));

            }

            return lijst;
        }

        public static Genre getById(string GenreID)
        {
            Genre genre = new Genre();

            String sSQL = "SELECT * FROM Genre WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", GenreID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            DbDataReader reader = Database.GetData(sSQL, par1);
            while (reader.Read())
            {
                genre.ID = reader["ID"].ToString();
                genre.Name = reader["Name"].ToString();
            }

            return genre;
        }

        public static void NewGenre(String genre){
            String sql =  "INSERT INTO Genre VALUES(@Name)";
            DbParameter par1 = Database.AddParameter("@Name", genre);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1); 
        }

        public static void DeleteGenre(Genre genre) {
            String sql = "DELETE FROM Genre WHERE ID = @Genre";
            DbParameter par1 = Database.AddParameter("@Genre", genre._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditGenre(Genre genre, String naam)
        {
            String sql = "UPDATE Genre Set Name=@naam WHERE ID=@Genre";
            DbParameter par1 = Database.AddParameter("@Genre", genre._ID);
            DbParameter par2 = Database.AddParameter("@naam", naam);
            if (par2.Value == null) par2.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2);
        }

        public override string ToString(){
            return this.Name;
        }
    }
}
