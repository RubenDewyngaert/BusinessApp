using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FestivalApp.model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festivalitis.ViewModel
{
    class TicketPageVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Ticketing"; }
        }

        public TicketPageVM() {
            _types = TicketType.getAll();
            _tickets = Ticket.getAll();
            foreach (TicketType tt in _types){
                _totaalAantal = _totaalAantal + tt.AvailableTickets;
            }
            int TotAantal = _totaalAantal;
            foreach (Ticket t in _tickets){
                TotAantal = TotAantal - t.Amount;
                _aantal = TotAantal;
            }
            _ticketNumber = 0;
            _soortAantal = 0;
            _newTicket = new Ticket();

        }

        private ObservableCollection<TicketType> _types;
        public ObservableCollection<TicketType> Types {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                OnPropertyChanged("Types");
            }
        }

        private TicketType _geselecteerdType;
        public TicketType GeselecteerdType
        {
            get
            {
                return _geselecteerdType;
            }
            set
            {
                _geselecteerdType = value;
                if (_geselecteerdType != null) {
                    _newType = _geselecteerdType;
                    _soortAantal = _geselecteerdType.AvailableTickets; ;
                    int SooAantal = _geselecteerdType.AvailableTickets;
                    foreach (Ticket t in _tickets)
                    {
                        if (t.TicketType.ID == _geselecteerdType.ID)
                        {
                            SooAantal = SooAantal - t.Amount;
                            _soortAantal = SooAantal;
                        }
                    }
                }
                OnPropertyChanged("GeselecteerdType");
                OnPropertyChanged("NewType");
                OnPropertyChanged("SoortAantal");
            }
        }

        private TicketType _newType;
        public TicketType NewType
        {
            get
            {
                return _newType;
            }
            set
            {
                _newType = value;
                OnPropertyChanged("NewType");
            }
        }

        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
            set
            {
                _tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        private Ticket _geselecteerdTicket;
        public Ticket GeselecteerdTicket
        {
            get
            {
                return _geselecteerdTicket;
            }
            set
            {
                _geselecteerdTicket = value;
                if (_geselecteerdTicket != null)
                {
                    _newTicket = _geselecteerdTicket;
                    int i = 0;
                    foreach (TicketType tt in _types)
                    {

                        if (tt.ID == _geselecteerdTicket.TicketType.ID)
                        {
                            _ticketNumber = i;
                            break;
                        }
                        i++;

                    }
                    

                }
                OnPropertyChanged("GeselecteerdTicket");
                OnPropertyChanged("NewTicket");
                OnPropertyChanged("TicketNumber");  
            }
        }

        private Ticket _newTicket;
        public Ticket NewTicket
        {
            get
            {
                return _newTicket;
            }
            set
            {
                _newTicket = value;
                OnPropertyChanged("NewTicket");
            }
        }

        private int _ticketNumber;

        public int TicketNumber
        {
            get
            {
                return _ticketNumber;
            }

            set
            {
                _ticketNumber = value;
                OnPropertyChanged("TicketNumber");
            }
        }

        private int _soortAantal;

        public int SoortAantal
        {
            get
            {
                return _soortAantal;
            }

            set
            {
                _soortAantal = value;
                OnPropertyChanged("SoortAantal");
            }
        }

        private int _totaalAantal;

        public int TotaalAantal
        {
            get
            {
                return _totaalAantal;
            }

            set
            {
                _totaalAantal = value;
                OnPropertyChanged("TotaalAantal");
            }
        }

        private int _aantal;

        public int Aantal
        {
            get
            {
                return _aantal;
            }

            set
            {
                _aantal = value;
                OnPropertyChanged("Aantal");
            }
        }

        #region TicketCommands

        public ICommand DeleteTicketCommand
        {
            get
            {
                return new RelayCommand(DeleteTicket);
            }
        }

        public void DeleteTicket()
        {
            Ticket.DeleteTicket(GeselecteerdTicket);
            Tickets.Remove(GeselecteerdTicket);
            _tickets = Ticket.getAll();

            int TotAantal = _totaalAantal;
            foreach (Ticket t in _tickets)
            {
                TotAantal = TotAantal - t.Amount;
                _aantal = TotAantal;
            }

            OnPropertyChanged("Tickets");
            OnPropertyChanged("Aantal");
        }

        public ICommand AddTicketCommand
        {
            get
            {
                return new RelayCommand(AddTicket);
            }
        }

        public void AddTicket()
        {
            NewTicket.TicketType = _types[_ticketNumber];
            Ticket.NewTicket(NewTicket, NewTicket.TicketType.ID);
            _tickets = Ticket.getAll();

            int TotAantal = _totaalAantal;
            foreach (Ticket t in _tickets)
            {
                TotAantal = TotAantal - t.Amount;
                _aantal = TotAantal;
            }
            OnPropertyChanged("Tickets");
            OnPropertyChanged("Aantal");

        }

        public ICommand EditTicketCommand
        {
            get
            {
                return new RelayCommand(EditTicket);
            }
        }

        public void EditTicket()
        {
            NewTicket.TicketType = _types[_ticketNumber];
            Ticket.EditTicket(NewTicket, NewTicket.TicketType.ID);
            _tickets = Ticket.getAll();

            int TotAantal = _totaalAantal;
            foreach (Ticket t in _tickets)
            {
                TotAantal = TotAantal - t.Amount;
                _aantal = TotAantal;
            }
            OnPropertyChanged("Tickets");
            OnPropertyChanged("Aantal");

        }



        public ICommand PrintTicketCommand
        {
            get
            {
                return new RelayCommand(PrintTicket);
            }
        }

        public void PrintTicket()
        {
            Ticket t = _geselecteerdTicket; 
            for (int i = 0; i < t.Amount; i++) 
            { 
                string filename = i + "_" + t.Ticketholder + ".docx"; 
                File.Copy("Template.docx", filename, true); 
                WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true); 
                IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>(); 
                foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>()) 
                {
                    bookmarks[bms.Name] = bms; 
                } 
                bookmarks["Naam"].Parent.InsertAfter<Run>(new Run(new Text(t.Ticketholder.Replace(" ", string.Empty))), bookmarks["Naam"]); 
                bookmarks["Email"].Parent.InsertAfter<Run>(new Run(new Text(t.TicketholderEmail)), bookmarks["Email"]); 
                bookmarks["Aantal"].Parent.InsertAfter<Run>(new Run(new Text(t.Amount.ToString())), bookmarks["Aantal"]); 
                Run run = new Run(new Text(i + t.Ticketholder)); 
                RunProperties prop = new RunProperties(); 
                RunFonts font = new RunFonts() 
                { 
                    Ascii = "Free 3 of 9 Extended", HighAnsi = "Free 3 of 9 Extended" 
                }; 
                FontSize size = new FontSize() { Val = "50" }; 
                prop.Append(font); prop.Append(size); 
                run.PrependChild<RunProperties>(prop); 
                bookmarks["Barcode"].Parent.InsertAfter<Run>(run, bookmarks["Barcode"]); 
                newdoc.Close(); 
            } 
            
            MessageBox.Show("De tickets van "+_geselecteerdTicket.Ticketholder+" werden afgedrukt.");
        }   



        #endregion 

        #region Type commands

        public ICommand DeleteTypeCommand
        {
            get
            {
                return new RelayCommand(DeleteType);
            }
        }

        public void DeleteType()
        {
            Boolean verwijder = true;

            foreach (Ticket t in _tickets)
            {
                Console.WriteLine(t.TicketType.Name + t.TicketType.ID + "1  2" + GeselecteerdType.ID);
                if (t.TicketType.ID == GeselecteerdType.ID)
                {
                    verwijder = false;
                }
            }

            if (verwijder == true)
            {
                TicketType.DeleteType(GeselecteerdType);
                Types.Remove(GeselecteerdType);
                OnPropertyChanged("TicketNumber");
                GeselecteerdTicket = _tickets[0];
                OnPropertyChanged("GeselecteerdTicket");
            }
            else
            {
                MessageBox.Show("Deze type kan niet verwijderd worden omdat deze nog gebruikt wordt.");
            }


        }

        public ICommand AddTypeCommand
        {
            get
            {
                return new RelayCommand(AddType);
            }
        }

        public void AddType()
        {
            TicketType.NewType(NewType);
            _types = TicketType.getAll();
            OnPropertyChanged("Types");
            GeselecteerdTicket = _tickets[0];
            OnPropertyChanged("GeselecteerdTicket");
            
        }

        public ICommand EditTypeCommand
        {
            get
            {
                return new RelayCommand(EditType);
            }
        }

        public void EditType()
        {
            TicketType.EditType(NewType);
            _types = TicketType.getAll();
            OnPropertyChanged("Types");
            GeselecteerdTicket = _tickets[0];
            OnPropertyChanged("GeselecteerdTicket");
        }

        #endregion
    }
}
