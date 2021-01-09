using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSQLITE
{
    class Cars_DAO
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_conn_string;
        public Cars_DAO(string conn_string)
        {
            m_conn_string = conn_string;
        }

        private int ExecuteNonQuery(string query)
        {
            int result = 0;

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                using (cmd.Connection = new SQLiteConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        public void AddCars(Cars c)
        {
            ExecuteNonQuery("INSERT INTO Cars(Manufacturer,Model,Year)" +
           $"VALUES('{c.Manufacturer}', '{c.Model}', {c.Year});");

        }

        public List<Cars> GetAllCars()
        {
            List<Cars> result = new List<Cars>();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    using (cmd.Connection = new SQLiteConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM Cars";

                        SQLiteDataReader reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            Cars c = new Cars
                            {
                                ID = (long)reader["ID"],
                                Manufacturer = reader["Manufacturer"].ToString(),
                                Year = (long)reader["YEAR"],
                                Model = reader["Model"].ToString(),
                            };
                            result.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                my_logger.Error("Failed Function GetAllCars by query (select * from cars)");
                my_logger.Error($"Failed to read from data base. Error : {ex}");
               
            }
            return result;

        }
        public void UpdateCars(Cars c, int id)
        {
            ExecuteNonQuery(
                $"UPDATE Cars SET Manufacturer='{c.Manufacturer}', Year={c.Year},Model='{c.Model}'" +
                $"WHERE ID={id}");
        }
        public void RemoveCars(int id)
        {
            int result = ExecuteNonQuery($"DELETE FROM Cars WHERE ID={id}");
            
        }
        public List<Cars> GetAllCarsSameManufacturer(string manuf)
        {
            List<Cars> result = new List<Cars>();

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                using (cmd.Connection = new SQLiteConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM Cars WHERE Manufacturer='{manuf}';";

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cars c = new Cars()
                        {
                            ID = (long)reader["ID"],
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Year = (long)reader["YEAR"],
                            Model = reader["Model"].ToString(),
                        };
                        result.Add(c);

                    }
                }
            }
            return result;
        }
        public List<object> GetAllCarsAndTests()
        {
            List<object> result = new List<object>();

            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                using (cmd.Connection = new SQLiteConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM Cars c inner join Tests t on c.ID=t.Cars_ID order by c.ID ";
         
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())        
                    {
                        var t = new  
                        {
                            Cars_ID = (long)reader["Cars_id"],
                            TestTimeStamp = DateTime.Parse(reader["timestamp"].ToString()),
                            IsPassedTest = (bool)reader["IsPassed"],
                            ID = (long)reader["ID"],
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Year = (long)reader["YEAR"],
                            Model = reader["Model"].ToString(),
                        };
                        result.Add(t);

                    }
                }
            }
            return result;
        }


    }

}
