using Festivalitis.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class Festival
    {
        private DateTime _StartDate;
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
