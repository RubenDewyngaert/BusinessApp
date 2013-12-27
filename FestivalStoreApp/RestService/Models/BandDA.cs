using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FestivalApp.model;
using DBHelper;
using System.Data.Common;

namespace RestService.Models
{
    public class BandDA
    {
        public static List<Band> GetBands() {
            List<Band> lijst = new List<Band>(); string sSQL = "SELECT * FROM [Band]";

            DbDataReader reader = Database.GetData(sSQL, null);

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    Band aNieuw = new Band();

                    aNieuw.ID = reader["ID"].ToString(); 
                    aNieuw.Name = reader["Name"].ToString(); 
                    aNieuw.Picture = reader["Picture"].ToString(); 
                    aNieuw.Description = reader["Description"].ToString(); 
                    aNieuw.Twitter = reader["Twitter"].ToString(); 
                    aNieuw.Facebook = reader["Facebook"].ToString();

                    lijst.Add(aNieuw);
                }
            } return lijst;
        }

        public static Band GetBand(int index) {
            return GetBands()[index];
        }
    }
}