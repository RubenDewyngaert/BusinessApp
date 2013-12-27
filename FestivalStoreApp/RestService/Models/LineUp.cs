
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FestivalApp.model
{
    class LineUp
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

        

        public override string ToString() 
        {
            return this.Band.Name + " speelt op " + this.Stage.Name;
        }
    }
}
