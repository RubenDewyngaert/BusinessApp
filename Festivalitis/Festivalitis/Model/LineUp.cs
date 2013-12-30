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
    class LineUp: IDataErrorInfo
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

        private DateTime _Date;
        [Required(ErrorMessage = "De datum is verplicht")]
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }
        private String _From;
        [Required(ErrorMessage = "Het starttijd is verplicht")]
        public String From
        {
            get
            {
                return _From;
            }
            set
            {
                _From = value;
            }
        }

        private String _Until;
        [Required(ErrorMessage = "De eindtijd is verplicht")]
        public String Until
        {
            get
            {
                return _Until;
            }
            set
            {
                _Until = value;
            }
        }
        private Stage _Stage;
        public Stage Stage
        {
            get
            {
                return _Stage;
            }
            set
            {
                _Stage = value;
            }
        }

        private Band _Band;
        public Band Band
        {
            get
            {
                return _Band;
            }
            set
            {
                _Band = value;
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


        public static ObservableCollection<LineUp> getAll()
        {

            ObservableCollection<LineUp> lijst = new ObservableCollection<LineUp>();

            String sSQL = "SELECT * FROM LineUp ORDER BY Date, Vanaf";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                LineUp aNew = new LineUp();
                aNew.ID = reader["ID"].ToString();
                aNew.Date = Convert.ToDateTime(reader["Date"].ToString());
                aNew.From = reader["Vanaf"].ToString();
                aNew.Until = reader["Until"].ToString();
                string BandID = reader["Band"].ToString();
                string StageID = reader["Stage"].ToString();
                aNew.Band = Band.getBand(BandID);
                aNew.Stage = Stage.getStage(StageID);

                lijst.Add(aNew);
            }


            return lijst;
        }


        public static void NewLineUp(LineUp lineup, string SID, string BID)
        {
            String sql = "INSERT INTO LineUp VALUES(@Date, @Vanaf, @Until, @Stage, @Band)";
            DbParameter par1 = Database.AddParameter("@Date", lineup.Date);
            DbParameter par2 = Database.AddParameter("@Vanaf", lineup.From);
            DbParameter par3 = Database.AddParameter("@Until", lineup.Until);
            DbParameter par4 = Database.AddParameter("@Stage", SID);
            DbParameter par5 = Database.AddParameter("@Band", BID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            if (par5.Value == null) par5.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5);
        }

        public static void DeleteLineUp(LineUp lineup)
        {
            String sql = "DELETE FROM LineUp WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", lineup._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditLineUp(LineUp lineup, string SID, string BID)
        {
            String sql = "UPDATE LineUp Set Date=@Date, Vanaf=@Vanaf, Until=@Until, Stage=@Stage, Band=@Band WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("@ID", lineup.ID);
            DbParameter par2 = Database.AddParameter("@Date", lineup.Date);
            DbParameter par3 = Database.AddParameter("@Vanaf", lineup.From);
            DbParameter par4 = Database.AddParameter("@Until", lineup.Until);
            DbParameter par5 = Database.AddParameter("@Stage", SID);
            DbParameter par6 = Database.AddParameter("@Band", BID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            if (par5.Value == null) par5.Value = DBNull.Value;
            if (par6.Value == null) par6.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5, par6);
        }

        public override string ToString() 
        {
            return this.Band.Name + " speelt op " + this.Stage.Name;
        }
    }
}
