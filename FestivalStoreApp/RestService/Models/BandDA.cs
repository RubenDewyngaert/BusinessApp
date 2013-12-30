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
            List<Band> lijst = new List<Band>(); string sSQL = "SELECT * FROM [Bands]";

            DbDataReader reader = Database.GetData(sSQL, null);

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    Band aNew = new Band();

                    aNew.ID = reader["ID"].ToString();
                    aNew.Name = reader["Name"].ToString();
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

                    lijst.Add(aNew);
                }
            } return lijst;
        }

        public static Band GetBand(int index) {
            return GetBands()[index];
        }
    }
}