using Festivalitis.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class Festival: IDataErrorInfo
    {
        private DateTime _StartDate;
        [Required(ErrorMessage = "De startdatum is verplicht")]
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        private DateTime _EndDate;
        [Required(ErrorMessage = "De eindatum is verplicht")]
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
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


        public static Festival GetData() {
            Festival festival = new Festival();

            String sSQL = "SELECT * FROM Festival";

            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read()) {

                festival._Name = reader["Name"].ToString();
                festival._StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                festival._EndDate = Convert.ToDateTime(reader["EndDate"].ToString());

            }

            return festival;
        }

        public static void EditData(Festival festival) {
            String sSQL = "UPDATE Festival SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate";

            DbParameter par1 = Database.AddParameter("@Name", festival._Name);
            DbParameter par2 = Database.AddParameter("@StartDate", festival._StartDate);
            DbParameter par3 = Database.AddParameter("@EndDate", festival._EndDate);

            Database.ModifyData(sSQL, par1, par2, par3);
        }
    }
}
