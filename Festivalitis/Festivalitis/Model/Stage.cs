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

        /*public static Stage getStage(String sID)
        {
            Stage stage = new Stage();
            String sSQL = "SELECT * FROM Stage WHERE id=@filter";
            DbParameter filter = Database.AddParameter("@filter", sID);
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
                stage.ID = reader["ID"].ToString();
            stage.Name = reader["Name"].ToString();
            return stage;
        }*/
    }
}
