using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupRolodex
{
    class Program
    {
        static SqlConnection connection; 

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What would you like to do: \n \t 1) Enter contact info. \n \t 2) Delete contact info. \n \t 3) Quit. \n \t 4) Search for an entry.");
                string user_input = Console.ReadLine();

                connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Rachel\source\repos\GroupRolodex\GroupRolodex\GroupRolodex.mdf;Integrated Security=True");
                connection.Open();

                if (user_input == "1")
                {
                    //Enter contact function goes here
                    Console.WriteLine("What name do you want to add?");
                    string user_name = Console.ReadLine();
                    Console.WriteLine("What phone number would you like to add?");
                    string user_phone = Console.ReadLine();

                    SqlCommand addContact = new SqlCommand($@"INSERT INTO GroupRolodex (Name, PhoneNumber) VALUES('{user_name}', '{user_phone}')", connection);
                    addContact.ExecuteNonQuery();
                    Console.WriteLine("We have added your contact.");
                }

                else if (user_input == "2")
                {
                    //Delete function goes here 
                    Console.WriteLine("What is the Id # you would like to delete?");
                    int id_number = Convert.ToInt32(Console.ReadLine());
                    SqlCommand delete_contact = new SqlCommand($@"DELETE FROM GroupRolodex WHERE ID = ('{id_number}')", connection);
                    delete_contact.ExecuteNonQuery();
                    Console.WriteLine("Your contact has been deleted.");
                }

                else if (user_input == "3")
                {
                    break; 
                }

                else if (user_input == "4")
                {
                    Console.WriteLine("Search:");
                    string search_name = Console.ReadLine();
                    SqlCommand search = new SqlCommand($"SELECT * FROM GroupRolodex WHERE Name LIKE '{search_name}%'",connection);
                    //WHERE ID = ('{id_number}')", connection)
                    SqlDataReader reader = search.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Name"].ToString() + " - " + reader["PhoneNumber"].ToString()); 
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Dude. What the heck?");
                }
                Console.ReadLine();
            }
            connection.Close();
        } 
    }
}