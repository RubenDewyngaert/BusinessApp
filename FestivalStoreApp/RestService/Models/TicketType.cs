
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

        
        public override string ToString()
        {
            return this.Name;
        }
    }

}
