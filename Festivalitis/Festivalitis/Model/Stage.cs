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
    class Stage
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

        public static ObservableCollection<Stage> getAll()
        {
            ObservableCollection<Stage> lijst = new ObservableCollection<Stage>();

            String sSQL = "SELECT * FROM Stage";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Stage aNew = new Stage();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();

                lijst.Add(aNew);
            }


            return lijst;
        }

        public static void NewStage(String stage)
        {
            String sql = "INSERT INTO Stage VALUES(@Name)";
            DbParameter par1 = Database.AddParameter("@Name", stage);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void DeleteStage(Stage stage)
        {
            String sql = "DELETE FROM Stage WHERE ID = @Stage";
            DbParameter par1 = Database.AddParameter("@Stage", stage._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditStage(Stage stage, String naam)
        {
            String sql = "UPDATE Stage Set Name=@naam WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("@ID", stage.ID);
            DbParameter par2 = Database.AddParameter("@naam", naam);
            if (par2.Value == null) par2.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
