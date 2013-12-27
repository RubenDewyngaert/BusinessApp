using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FestivalApp.model
{
    class Ticket
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
    }
}
