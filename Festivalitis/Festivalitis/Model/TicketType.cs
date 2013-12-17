using Festivalitis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.model
{
    class TicketType
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
        private double _Price;
        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }

        private int __AvailableTickets;
        public int AvailableTickets
        {
            get
            {
                return __AvailableTickets;
            }
            set
            {
                __AvailableTickets = value;
            }
        }

        public static ObservableCollection<TicketType> getAll()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();

            String sSQL = "SELECT * FROM TicketType";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                TicketType aNew = new TicketType();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                aNew.Price = Convert.ToDouble(reader["Price"].ToString());
                aNew.AvailableTickets = Int32.Parse(reader["AvailebleTickets"].ToString());

                lijst.Add(aNew);
            }


            return lijst;
        }
    }
}
