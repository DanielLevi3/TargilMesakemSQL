using Npgsql;
using System;
using System.Collections.Generic;

namespace TargilMesakemPostgresQ5Part2
{
    class MoviesDAO
        {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_conn_string;

            public MoviesDAO(string conn_string)
            {
                m_conn_string = conn_string;
            }

            private int ExecuteNonQuery(string query)
            {
                int result = 0;
            
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    using (cmd.Connection = new NpgsqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = query;

                        result = cmd.ExecuteNonQuery();
                    }
                }

                return result;
            }
            public void AddMovie(Movies m)
            {
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    using (cmd.Connection = new NpgsqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "INSERT INTO movies(name,release_date,genre_id)" +
                                       $" VALUES('{m.Name}','{m.Release_Date}',{m.Genre_ID});";

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                my_logger.Error("Failed Function AddMovie by query (INSERT INTO movies(name, release_date, genre_id)" +
                    " VALUES('{m.Name}','{m.Release_Date}',{m.Genre_ID});");
                my_logger.Error($"Failed to read from data base. Error : {ex}");
            }
        }

            public List<Movies> GetAllMovies()
            {
                List<Movies> result = new List<Movies>();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    using (cmd.Connection = new NpgsqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "SELECT * FROM movies";

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                          Movies m = new Movies()
                            {
                                ID = (long)reader["ID"],
                                Name = reader["Name"].ToString(),
                                Release_Date = (DateTime)reader["release_date"],
                                Genre_ID = (long)reader["genre_id"],
                            };
                            result.Add(m);
                        }

                    }
                }

                return result;
            }
            public List<Movies> GetAllMoviesByActorsBirthday()
            {
                List<Movies> result = new List<Movies>();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    using (cmd.Connection = new NpgsqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "select * from movies m inner join movies_actors ma on m.id = ma.movie_id" +
                        " inner join actors a on a.id = ma.actor_id where a.birth_date <= '1965-06-01' order by m.id;";

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Movies m = new Movies()
                            {
                                ID = (long)reader["ID"],
                                Name = reader["Name"].ToString(),
                                Release_Date = (DateTime)reader["release_date"],
                                Genre_ID = (long)reader["genre_id"],
                            };
                            result.Add(m);
                        }

                    }
                }

                return result;
            }
        public Movies GetMovieById(int id)
            {
                 Movies result = null;

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    using (cmd.Connection = new NpgsqlConnection(m_conn_string))
                    {
                        cmd.Connection.Open();

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = $"SELECT * FROM movies WHERE id={id}";

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new Movies
                            {
                                ID = (long)reader["ID"],
                                Name = reader["Name"].ToString(),
                                Release_Date = (DateTime)reader["release_date"],
                                Genre_ID = (long)reader["genre_id"],
                            };
                        }

                    }
                }

                return result;
            }
            public void UpdateMovie(Movies m, int id)
            {
                ExecuteNonQuery(
                     $"UPDATE movies SET name='{m.Name}', release_date='{m.Release_Date}',genre_id={m.Genre_ID} WHERE id={id}");
            }

            public void RemoveMovie(long id)
            {
               ExecuteNonQuery($"delete from movies where id = {id}");
                
            }
        }

    }
