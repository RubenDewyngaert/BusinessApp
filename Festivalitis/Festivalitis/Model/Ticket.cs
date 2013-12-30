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
    class Ticket: IDataErrorInfo
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

        private String _Ticketholder;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten.")]
        public String Ticketholder
        {
            get
            {
                return _Ticketholder;
            }
            set
            {
                _Ticketholder = value;
            }
        }
        private String _TicketholderEmail;
        [Required(ErrorMessage = "Het EmailAdress is verplicht")]
        [EmailAddress(ErrorMessage = "Dit moet een geldig emailadres zijn.")]
        public String TicketholderEmail
        {
            get
            {
                return _TicketholderEmail;
            }
            set
            {
                _TicketholderEmail = value;
            }
        }

        private TicketType _TicketType;
        public TicketType TicketType
        {
            get
            {
                return _TicketType;
            }
            set
            {
                _TicketType = value;
            }
        }
        private int __Amount;
        [Required(ErrorMessage = "Het aantal is verplicht")]
        public int Amount
        {
            get
            {
                return __Amount;
            }
            set
            {
                __Amount = value;
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


        public static ObservableCollection<Ticket> getAll()
        {
            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();

            String sSQL = "SELECT * FROM Ticket";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Ticket aNew = new Ticket();
                aNew.ID = reader["ID"].ToString();
                aNew.Ticketholder = reader["Ticketholder"].ToString();
                aNew.TicketholderEmail = reader["TicketholderEmail"].ToString();
                aNew.Amount = Int32.Parse(reader["Amount"].ToString());
                string ttID = reader["TicketType"].ToString();
                aNew.TicketType = TicketType.getByID(ttID);
                lijst.Add(aNew);
            }


            return lijst;
        }

        public static void NewTicket(Ticket ticket, string TTID)
        {
            String sql = "INSERT INTO Ticket VALUES(@Ticketholder, @TicketholderEmail, @TicketType, @Amount)";
            DbParameter par1 = Database.AddParameter("@Ticketholder", ticket.Ticketholder);
            DbParameter par2 = Database.AddParameter("@TicketholderEmail", ticket.TicketholderEmail);
            DbParameter par3 = Database.AddParameter("@TicketType", TTID);
            DbParameter par4 = Database.AddParameter("@Amount", ticket.Amount);
            if (par1.Value == null) par1.Value = DBNull.Value;
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4);
        }

        public static void DeleteTicket(Ticket ticket)
        {
            String sql = "DELETE FROM Ticket WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", ticket._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditTicket(Ticket ticket, string TTID)
        {
            String sql = "UPDATE Ticket Set Ticketholder=@Ticketholder, TicketholderEmail=@TicketholderEmail, TicketType=@TicketType, Amount=@Amount WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("@ID", ticket.ID);
            DbParameter par2 = Database.AddParameter("@Ticketholder", ticket.Ticketholder);
            DbParameter par3 = Database.AddParameter("@TicketholderEmail", ticket.TicketholderEmail);
            DbParameter par4 = Database.AddParameter("@TicketType", TTID);
            DbParameter par5 = Database.AddParameter("@Amount", ticket.Amount);
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            if (par5.Value == null) par5.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5);
        }

        public override string ToString()
        {
            return this.Ticketholder;
        }
    }
}
