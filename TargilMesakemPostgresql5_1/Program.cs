using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemPostgresql5_1
{
    class Program
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static bool TestDbConnection(string conn)
        {
            try
            {
                using (var my_conn = new NpgsqlConnection(conn))
                {
                    my_conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The program failed to create connection with db {ex}");
                return false;
            }
        }
        private static void PrintAllWorkersAndTheirRoles(string conn_string)
        {
            using (var conn = new NpgsqlConnection(conn_string))
            {
                conn.Open();
                string sp_name = "sp_get_all_workers_and_their_roles";

                NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure; 

                    
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    long id = (long)reader["id"];
                    string name = (string)reader["name"];
                    string phone = (string)reader["phone"];
                    int salary = (int)reader["salary"];
                    string role = (string)reader["name_role"];
                    Console.WriteLine($"{id} {name},{phone},{salary},{role}");
                }
            }
        }
        private static void Get_Max_Workers_In_Same_Site(string conn_string)
        {
            try
            {
                using (var conn = new NpgsqlConnection(conn_string))
                {
                    conn.Open();
                    string sp_name = "sp_get_max_workers_in_same_site";

                    NpgsqlCommand command = new NpgsqlCommand(sp_name, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        long number_of_workers = (long)reader["max_workers_in_site"];
                        string name_of_site = (string)reader["name_of_max_workers_in_site"];
                        Console.WriteLine($"number_of_workers:{number_of_workers} ,name_of_site:{name_of_site};");
                    }
                }
            }
            catch (Exception ex)
            {
                my_logger.Error("Failed Function Get_Max_Workers_In_Same_Site by sp (sp_get_max_workers_in_same_site)");
                my_logger.Error($"Failed to read from data base. Error : {ex}");
            }
        }

        static void Main(string[] args)
        {
            my_logger.Info("...System coming up...");
            string conn_string = "Host=localhost;Username=postgres;Password=admin;Database=TargilMesakemPostgre";
            if (TestDbConnection(conn_string))
                {
                    PrintAllWorkersAndTheirRoles(conn_string);
                    Console.WriteLine();
                    Get_Max_Workers_In_Same_Site(conn_string);
                }
            my_logger.Info("...System shutting down...");
        }
    }
}
