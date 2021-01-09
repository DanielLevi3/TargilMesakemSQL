using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargilMesakemSSMS
{
    class StoresDAO
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_conn_string;

        public StoresDAO(string conn_string)
        {
            m_conn_string = conn_string;
        }

        private int ExecuteNonQuery(string query)
        {
            int result = 0;

            using (SqlCommand cmd = new SqlCommand())
            {
                using (cmd.Connection = new SqlConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }
        public void AddStore(Stores s)
        {
            ExecuteNonQuery("INSERT INTO Stores(Name, Floor,Categories_ID)" +
            $"VALUES('{s.Name}',{s.Floor},{s.Categories_ID});");

        }

        public List<Stores> GetAllStores()
        {
            List<Stores> result = new List<Stores>();

            using (SqlCommand cmd = new SqlCommand())
            {
                using (cmd.Connection = new SqlConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Stores";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Stores s = new Stores()
                        {
                            ID = (long)reader["ID"],
                            Name = reader["Name"].ToString(),
                            Floor = (int)reader["Floor"],
                            Categories_ID=(long)reader["Categories_ID"],
                        };
                        result.Add(s);
                    }

                }
            }

            return result;
        }
        public List<Stores> GetAllStoresWithTheSameCatAndFloor(long cat,int floor)
        {
            List<Stores> result = new List<Stores>();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (cmd.Connection = new SqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = $"SELECT * FROM Stores WHERE Categories_ID={cat} AND Floor={floor};";

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Stores s = new Stores()
                            {
                                ID = (long)reader["ID"],
                                Name = reader["Name"].ToString(),
                                Floor = (int)reader["Floor"],
                                Categories_ID = (long)reader["Categories_ID"],
                            };
                            result.Add(s);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                my_logger.Error("Failed Function GetAllStoresWithTheSameCatAndFloor by query (SELECT * FROM Stores WHERE Categories_ID={cat} AND Floor={floor})");
                my_logger.Error($"Failed to read from data base. Error : {ex}");
            }

            return result;
        }
        public Stores GetStoreById(int id)
        {
            Stores result = null;

            using (SqlCommand cmd = new SqlCommand())
            {
                using (cmd.Connection = new SqlConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM Stores WHERE ID={id}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        result = new Stores
                        {
                            ID= (long)reader["ID"],
                            Name = reader["Name"].ToString(),
                            Floor= (int)reader["Floor"],
                            Categories_ID = (long)reader["Categories_ID"],
                        };
                    }

                }
            }

            return result;
        }
        public Object GetMaxStoresByCategory()
        {
            object result = null;
            using (SqlCommand cmd = new SqlCommand())
            {
                using (cmd.Connection = new SqlConnection(m_conn_string))
                {
                    cmd.Connection.Open();

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select Top 1 * from " +
                    "(select Categories_id id1, Count(*) count1 from Stores s " +
                    "group by Categories_id)c "+
                    "join Categories c3 on c3.ID = c.id1 "+
                    "order by count1 desc;";
                                           

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                           result = new
                        {
                            ID_Of_Category = (long)reader["id1"],
                            Name_of_MOST_Category = reader["Name"].ToString(),
                            Count_of_Most = (int)reader["count1"],
                            Categories_ID = (long)reader["ID"],
                        };
                    }              
                }
            }
            return result;
        }
        public void UpdateStore(Stores s, int id)
        {
           ExecuteNonQuery(
                $"UPDATE Stores SET Name='{s.Name}', Floor={s.Floor},Categories_ID={s.Categories_ID} WHERE ID={id}");
        }

        public int RemoveStore(int id)
        {
            int result = ExecuteNonQuery($"DELETE * FROM Stores WHERE ID={id}");
            return result;
        }
    }

}

