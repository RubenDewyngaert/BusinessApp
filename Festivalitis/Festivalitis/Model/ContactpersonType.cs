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
    class ContactpersonType: IDataErrorInfo
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


        public static ContactpersonType getType(String id)
        {
            ContactpersonType type = new ContactpersonType();

            String sSQL = "SELECT * FROM ContactpersonType WHERE ID = @ID";
            DbParameter par1 = Database.AddParameter("@ID", id);
            if (par1.Value == null) par1.Value = DBNull.Value;
            DbDataReader reader = Database.GetData(sSQL, par1);
            while (reader.Read())
            {
                type.Name = reader["Name"].ToString();
                type.ID = id;
            }


            return type;
        }

        public static ObservableCollection<ContactpersonType> getAll()
        {

            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();

            String sSQL = "SELECT * FROM ContactpersonType";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                ContactpersonType aNew = new ContactpersonType();
                aNew.ID = reader["ID"].ToString();
                aNew.Name = reader["Name"].ToString();
                lijst.Add(aNew);
            }


            return lijst;
        }

        public static void NewRole(String role)
        {
            String sql = "INSERT INTO ContactpersonType VALUES(@Name)";
            DbParameter par1 = Database.AddParameter("@Name", role);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void DeleteRole(ContactpersonType role)
        {
            String sql = "DELETE FROM ContactpersonType WHERE ID = @Role";
            DbParameter par1 = Database.AddParameter("@Role", role._ID);
            if (par1.Value == null) par1.Value = DBNull.Value;
            Database.ModifyData(sql, par1);
        }

        public static void EditRole(ContactpersonType role, String naam)
        {
            String sql = "UPDATE ContactpersonType Set Name=@naam WHERE ID=@Role";
            DbParameter par1 = Database.AddParameter("@Role", role._ID);
            DbParameter par2 = Database.AddParameter("@naam", naam);
            if (par2.Value == null) par2.Value = DBNull.Value;
            Database.ModifyData(sql, par1, par2);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
