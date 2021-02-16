using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace hello.world.load.balancer
{
    public class SqlOperations
    {
        public static void Insert(string server_who_loaded_page, string server_who_saved_submission, string color, string client, string connectionString)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "insert into data (server_who_loaded_page, server_who_saved_submission, color, client) values (@1, @2, @3, @4)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@1", sqlDbType: System.Data.SqlDbType.VarChar, 50).Value = server_who_loaded_page;
                        command.Parameters.Add("@2", System.Data.SqlDbType.VarChar, 50).Value = server_who_saved_submission;
                        command.Parameters.Add("@3", System.Data.SqlDbType.VarChar, 50).Value = color;
                        command.Parameters.Add("@4", System.Data.SqlDbType.VarChar, 50).Value = client;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Data> Read(string client, string connectionString)
        {
            List<Data> result = new List<Data>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select server_who_loaded_page, server_who_saved_submission, datetime, color, client from data where client = @1 order by datetime desc";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@1", System.Data.SqlDbType.VarChar, 50).Value = client;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Data
                                {
                                    server_who_loaded_page = reader.GetString(0),
                                    server_who_saved_submission = reader.GetString(1),
                                    datetime = reader.GetDateTime(2),
                                    color = reader.GetString(3),
                                    client = reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
