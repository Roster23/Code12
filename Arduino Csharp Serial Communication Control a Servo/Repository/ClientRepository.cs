using Arduino_Csharp_Serial_Communication_Control_a_Servo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino_Csharp_Serial_Communication_Control_a_Servo.Repository
{
    public class ClientRepository
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=Automated Desk System;Integrated Security=True;Trust Server Certificate=True";


        public List<Client> GetClients()
        {
            var clients = new List<Client>();


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Details";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Client client = new Client();
                                client.id = reader.GetString(1);
                                client.name = reader.GetString(2);
                                client.course = reader.GetString(3);
                                client.address = reader.GetString(4);

                                clients.Add(client);

                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }


            return clients;

        }
      

    }
}