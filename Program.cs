using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBuisnessLayer;
namespace ContactsPresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Find Contact Method
            clsContact Contact = clsContactServices.FindContact(1); 

            if(Contact != null)
            {
                Console.WriteLine("Contact was found !");
            }

            else
            { Console.WriteLine("Contact was not found!"); }


            // Add new Contact Method
            clsContactServices.AddNewContact();

            //Update Contact Method

            clsContactServices.UpdateContact(4);

            // Delete Contact Method

            clsContactServices.DeleteContact(15);

            //List all Contacts Method

            clsContactServices.ListAllContacts(); 

            // isExist Contact Method

            if (clsContactServices.isContactExists(4))
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }
    }
}
