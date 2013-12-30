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
    class Stage: IDataErrorInfo
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

        public static Stage getStage(String id)
        {
            Stage stage = new Stage();

            String sSQL = "SELECT * FROM Stage WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", id);
            if (par1.Value == null) par1.Value = DBNull.Value;
            DbDataReader reader = Database.GetData(sSQL, par1);
            while (reader.Read())
            {
                stage.Name = reader["Name"].ToString();
                stage.ID = id;
            }


            return stage;
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
