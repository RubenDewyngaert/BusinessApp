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
    class Genre
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
