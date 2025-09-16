using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ContactsDataLayer
{
    public class clsContactsData
    {
        static public bool CheckIfTheContactInTheDB(int ContactID, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref Nullable<DateTime> DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = "select * from Contacts where ContactID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@ID", ContactID);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        FirstName = reader["FirstName"].ToString();
                        LastName = reader["LastName"].ToString();
                        Email = reader["Email"].ToString();
                        Phone = reader["Phone"].ToString();
                        Address = reader["Address"].ToString();
                        DateOfBirth = (DateTime)reader["DateOfBirth"];
                        CountryID = Convert.ToInt32(reader["CountryID"]);
                        ImagePath = reader["ImagePath"].ToString();


                        reader.Close();
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                catch (Exception Error)
                {
                    Console.WriteLine(Error.ToString());
                    return false;
                }
            }


            return false;
        }

        static public int AddNewContactToTheDB(string FirstName, string LastName, string Email, string Phone, string Address, Nullable<DateTime> DateOfBirth, int CountryID, string ImagePath)
        {
            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = "insert into Contacts(FirstName , LastName , Email , Phone , Address , DateOfBirth , CountryID , ImagePath) values(@FirstName , @LastName , @Email , @Phone , @Address , @DateOfBirth , @CountryID , @ImagePath ) select SCOPE_IDENTITY();";

            int ContactID = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);

                if (ImagePath == "")
                    cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ImagePath", ImagePath);


                try
                {
                    conn.Open();

                    object obj = cmd.ExecuteScalar();

                    if (obj != null && int.TryParse(obj.ToString(), out int LastID))
                    {
                        ContactID = LastID;
                        Console.WriteLine("New contact was added succefly"); ;
                    }

                    else
                        Console.WriteLine("Failed to add new contact");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return ContactID;
            }

        }

        static public bool UpdateContactInTheDB(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address, Nullable<DateTime> DateOfBirth, int CountryID, string ImagePath)
        {
            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = @"UPDATE Contacts
                                   SET FirstName = @FirstName
                                      ,LastName = @LastName
                                      ,Email = @Email
                                      ,Phone = @Phone
                                      ,Address = @Address
                                      ,DateOfBirth = @DateOfBirth
                                      ,CountryID = @CountryID
                                      ,ImagePath = @ImagePath
                                 WHERE ContactID = @ContactID
                    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int temp = 0;
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("@CountryID", CountryID);

                if (ImagePath == "")
                    cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@ImagePath", ImagePath);


                try
                {
                    conn.Open();

                    temp = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return temp > 0;
            }

        }

        static public bool DeleteContactIDFromTheDB(int ContactID)
        {
            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = "Delete from Contacts where ContactID = @ContactID";


            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int temp = 0;
                cmd.Parameters.AddWithValue("@ContactID", ContactID);


                try
                {
                    conn.Open();

                    temp = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return temp > 0;
            }
        }

        static public DataTable GetAllContactsFromDB()
        {

            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = "select * from Contacts";


            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                        dt.Load(reader);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return dt;
            }
        }

        static public bool isContactExistsInDB(int ContactID)
        {
            string connectionString = "Server=. ; Database = ContactsDB ; User = ## ; Password = ######";
            string query = @"select case when exists 
                            (
                            select 0 from Contacts where ContactID = @ContactID
                            ) then 1 else 0 end as isExists;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object temp = null;
                cmd.Parameters.AddWithValue("@ContactID", ContactID);
                try
                {
                    conn.Open();

                     temp = cmd.ExecuteScalar();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return  Convert.ToBoolean(Convert.ToInt32(temp.ToString()));
            }
        }
    }
}
