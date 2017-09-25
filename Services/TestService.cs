using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TestingList.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isConnected = IsServerConnected("Server=DESKTOP-7KRBO2R\\SQLEXPRESS;Database=ToDoListDb;Trusted_Connection=True;");
            Console.WriteLine("DB isConnected = {0}", isConnected);

            if (isConnected)
            {
                ExecuteInsert();

                ExecuteUpdate();

                ExecuteDelete();

                ExecuteSelect();

                ExecuteSelectById();
            }

            Console.ReadLine();
        }

        private static void ExecuteSelectById()
        {
            throw new NotImplementedException();
        }

        private static void ExecuteSelect()
        {
            throw new NotImplementedException();
        }

        private static void ExecuteDelete()
        {
            throw new NotImplementedException();
        }

        private static void ExecuteUpdate()
        {
            throw new NotImplementedException();
        }

        private static void ExecuteInsert()
        {
            throw new NotImplementedException();
        }

        private static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}