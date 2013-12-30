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
    class Contactperson : IDataErrorInfo
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
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten ")]
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
        private String _Company;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten ")]
        public String Company
        {
            get
            {
                return _Company;
            }
            set
            {
                _Company = value;
            }
        }

        private ContactpersonType _JobRole;
        public ContactpersonType JobRole
        {
            get
            {
                return _JobRole;
            }
            set
            {
                _JobRole = value;
            }
        }
        private String _City;
        [Required(ErrorMessage = "De naam is verplicht")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 karakters bevatten ")]
        public String City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }

        private String _Email;
        [Required(ErrorMessage = "De naam is verplicht")]
        [EmailAddress(ErrorMessage = "Dit is geen geldig emailadres")]
        public String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        private String _Phone;
        [Required(ErrorMessage = "De naam is verplicht")]
        [Phone(ErrorMessage = "Dit is geen geldig telefoonnummer")]
        public String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }

        private String _Cellphone;
        [Required(ErrorMessage = "De naam is verplicht")]
        [Phone(ErrorMessage = "Dit is geen geldig telefoonnummer")]
        public String Cellphone
        {
            get
            {
                return _Cellphone;
            }
            set
            {
                _Cellphone = value;
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

        public static ObservableCollection<Contactperson> getAll()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();

            String sSQL = "SELECT * FROM ContactPerson";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Contactperson aNew = new Contactperson();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                //De reader crasht als hij iets leegs inleest, daarom vangen wij dit op met de if's
                if (!DBNull.Value.Equals(reader["Company"]))
                {
                    aNew._Company = reader["Company"].ToString();
                }
                else
                {
                    aNew._Company = null;
                }
                if (!DBNull.Value.Equals(reader["City"]))
                {
                    aNew._City = reader["City"].ToString();
                }
                else
                {
                    aNew._City = null;
                }
                if (!DBNull.Value.Equals(reader["Email"]))
                {
                    aNew._Email = reader["Email"].ToString();
                }
                else
                {
                    aNew._Email = null;
                }
                if (!DBNull.Value.Equals(reader["Phone"]))
                {
                    aNew._Phone = reader["Phone"].ToString();
                }
                else
                {
                    aNew._Phone = null;
                }
                if (!DBNull.Value.Equals(reader["Cellphone"]))
                {
                    aNew._Cellphone = reader["Cellphone"].ToString();
                }
                else
                {
                    aNew._Cellphone = null;
                }
                string typeID = reader["JobRole"].ToString();

                aNew._JobRole = ContactpersonType.getType(typeID);

                lijst.Add(aNew);
            }


            return lijst;
        }

        public static void NewPerson(Contactperson person, string JRID) {
            String sql = "INSERT INTO ContactPerson VALUES(@Name, @Company, @JobRole, @City, @Email, @Phone, @Cellphone)";
            DbParameter par1 = Database.AddParameter("@Name", person.Name);
            DbParameter par2 = Database.AddParameter("@Company", person.Company);
            DbParameter par3 = Database.AddParameter("@JobRole", JRID);
            DbParameter par4 = Database.AddParameter("@City", person.City);
            DbParameter par5 = Database.AddParameter("@Email", person.Email);
            DbParameter par6 = Database.AddParameter("@Phone", person.Phone);
            DbParameter par7 = Database.AddParameter("@Cellphone", person.Cellphone);
            if (par1.Value == null) par1.Value = DBNull.Value;
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            if (par5.Value == null) par5.Value = DBNull.Value;
            if (par6.Value == null) par6.Value = DBNull.Value;
            if (par7.Value == null) par7.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5, par6, par7); 
        }

        public static void DeletePerson(Contactperson person) {
            String sql = "DELETE FROM ContactPerson WHERE ID = @Person";
            DbParameter par1 = Database.AddParameter("@Person", person._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditPerson(Contactperson person, string JRID) {
            String sql = "UPDATE ContactPerson Set Name=@Name, Company=@Company, JobRole=@JobRole, City=@City, Email=@Email, Phone=@Phone, Cellphone=@Cellphone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("@ID", person.ID);
            DbParameter par2 = Database.AddParameter("@Name", person.Name);
            DbParameter par3 = Database.AddParameter("@Company", person.Company);
            DbParameter par4 = Database.AddParameter("@JobRole", JRID);
            DbParameter par5 = Database.AddParameter("@City", person.City);
            DbParameter par6 = Database.AddParameter("@Email", person.Email);
            DbParameter par7 = Database.AddParameter("@Phone", person.Phone);
            DbParameter par8 = Database.AddParameter("@Cellphone", person.Cellphone);
            if (par2.Value == null) par2.Value = DBNull.Value;
            if (par3.Value == null) par3.Value = DBNull.Value;
            if (par4.Value == null) par4.Value = DBNull.Value;
            if (par5.Value == null) par5.Value = DBNull.Value;
            if (par6.Value == null) par6.Value = DBNull.Value;
            if (par7.Value == null) par7.Value = DBNull.Value;
            if (par8.Value == null) par8.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2, par3, par4, par5, par6, par7, par8); 
        }


        public override string ToString()
        {
            return this.Name;
        }

    }

    

    
}
