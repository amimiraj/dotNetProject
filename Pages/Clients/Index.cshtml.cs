using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {

        public List<ClientsInfo> ListClients = new List<ClientsInfo>();

        public void OnGet()
        {

            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                   connection.Open();

                   String sql = "select * from clients";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {

                        using (SqlDataReader reader = command.ExecuteReader()) {

                            while (reader.Read()) { 
                            
                                ClientsInfo clintinfo = new ClientsInfo();
                                clintinfo.id =""+reader.GetInt32(0);
                                clintinfo.name = reader.GetString(1);
                                clintinfo.email = reader.GetString(2);
                                clintinfo.phone = reader.GetString(3);
                                clintinfo.address = reader.GetString(4);
                                clintinfo.created_at = reader.GetDateTime(5).ToString();

                                ListClients.Add(clintinfo);

                            }

                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("There something wrong: " + ex);
            }


        }
    }


    public class ClientsInfo() {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }


}
