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
                aNew.AvailableTickets = Int32.Parse(reader["AvailableTickets"].ToString());

                lijst.Add(aNew);
            }


            return lijst;
        }

        public static void NewType(TicketType type)
        {
            String sql = "INSERT INTO TicketType (Name, Price, AvailableTickets) VALUES(@Name, @Price, @AvailableTickets)";
            DbParameter par1 = Database.AddParameter("@Name", type.Name);
            DbParameter par2 = Database.AddParameter("@Price", type.Price);
            DbParameter par3 = Database.AddParameter("@AvailableTickets", type.AvailableTickets);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3);
        }

        public static void DeleteType(TicketType type)
        {
            String sql = "DELETE FROM TicketType WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", type._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditType(TicketType type)
        {
            String sql = "UPDATE TicketType Set Name=@Name, Price=@Price, AvailableTickets=@AvailableTickets WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("@ID", type.ID);
            DbParameter par2 = Database.AddParameter("@Name", type.Name);
            DbParameter par3 = Database.AddParameter("@Price", type.Price);
            DbParameter par4 = Database.AddParameter("@AvailableTickets", type.AvailableTickets);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

}
