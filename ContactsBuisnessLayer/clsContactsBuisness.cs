using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsDataLayer;
namespace ContactsBuisnessLayer
{
    public class clsContact
    {
        public int ContactID { get; internal set ; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public Nullable<DateTime> DateOfBirth { set; get; }
        public int CountryID { set; get; }
        public string ImagePath { set; get; }



        internal clsContact(int ContactID = 0, string FirstName = "", string LastName = "", string Email = "", string Phone = "", string Address = "", Nullable<DateTime> DateOfBirth = null, int CountryID = 0, string ImagePath = "")
        {
            this.ContactID = ContactID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
        }

        public clsContact()
        {
            this.ContactID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = null;
            this.CountryID = -1;
            this.ImagePath = "";
        }

    }

    public class clsContactServices
    {

        static private clsContact ReadTheNewAddedContactInfo()
        {
            clsContact Contact = new clsContact();

            Contact.FirstName = "Thaer";
            Contact.LastName = "Hussein";
            Contact.Email = "thaer@gmail.com";
            Contact.Phone = "07803242434";
            Contact.ImagePath = "Thaer";
            Contact.Address = "None";
            Contact.DateOfBirth = DateTime.Now;
            Contact.CountryID = 3;

            return Contact;

        }
        static private clsContact ReadTheNewUpdatedContactInfo(int ContactID)
        {
            clsContact Contact = new clsContact(ContactID);

            Contact.FirstName = "Ali";
            Contact.LastName = "Muhareb";
            Contact.Email = "Gaber";
            Contact.Phone = "0780504356";
            Contact.ImagePath = "Thaer";
            Contact.Address = "None";
            Contact.DateOfBirth = DateTime.Now;
            Contact.CountryID = 3;

            return Contact;

        }
        static public clsContact FindContact(int ContactID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            Nullable<DateTime> DateOfBirth = null;
            int CountryID = 0;



            if (clsContactsData.CheckIfTheContactInTheDB(ContactID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
            {
                clsContact Contact = new clsContact(ContactID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);

                return Contact;
            }

            else
                return null;
        }
        static public int AddNewContact()
        {
            clsContact Contact = ReadTheNewAddedContactInfo();

            Contact.ContactID = clsContactsData.AddNewContactToTheDB(Contact.FirstName, Contact.LastName, Contact.Email, Contact.Phone, Contact.Address, Contact.DateOfBirth, Contact.CountryID, Contact.ImagePath);

            return Contact.ContactID;
        }

        static public bool UpdateContact(int ContactID)
        {
            clsContact Contact = FindContact(ContactID);

            if (Contact == null)
            {
                Console.WriteLine("Invalid Contact ID");
                return false;
            }

            else
            {
                Contact = ReadTheNewUpdatedContactInfo(ContactID);

                return clsContactsData.UpdateContactInTheDB(ContactID, Contact.FirstName, Contact.LastName, Contact.Email, Contact.Phone, Contact.Address, Contact.DateOfBirth, Contact.CountryID, Contact.ImagePath);
            }

        }

        static public bool DeleteContact(int ContactID)
        {
            
             return clsContactsData.DeleteContactIDFromTheDB(ContactID);

        }

        static public void ListAllContacts()
        {
            DataTable dt = clsContactsData.GetAllContactsFromDB();

            foreach (DataRow row in dt.Rows)
            {
                string contactID = row["ContactID"].ToString().PadRight(5);
                string firstName = row["FirstName"].ToString().PadRight(15);
                string lastName = row["LastName"].ToString().PadRight(15);
                string email = row["Email"].ToString().PadRight(25);

                Console.WriteLine($"{contactID}{firstName}{lastName}{email}");
            }
        }

        static public bool isContactExists(int ContactID)
        {
            return clsContactsData.isContactExistsInDB(ContactID);
        }
        static public void PrintInfo(clsContact Contact)
        {
            Console.WriteLine("ContactID : " + Contact.ContactID);
            Console.WriteLine("FirstName : " + Contact.FirstName);
            Console.WriteLine("LastName : " + Contact.LastName);
            Console.WriteLine("Email : " + Contact.Email);
            Console.WriteLine("Phone : " + Contact.Phone);
            Console.WriteLine("Address : " + Contact.Address);
            Console.WriteLine("DateOfBirth : " + Contact.DateOfBirth.ToString());
            Console.WriteLine("CountryID : " + Contact.CountryID.ToString());
            Console.WriteLine("ImagePath : " + Contact.ImagePath);
        }
    }
}
