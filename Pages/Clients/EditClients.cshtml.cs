using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class EditClientsModel : PageModel
    {

        public ClientsInfo clientsInfo = new ClientsInfo();
        public String errorMessage = "";
        public String successMessage = "";



        public void OnGet()
        {

            String id = Request.Query["id"];



            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM clients where  id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientsInfo.id = "" + reader.GetInt32(0);
                                clientsInfo.name = reader.GetString(1);
                                clientsInfo.email = reader.GetString(2);
                                clientsInfo.phone = reader.GetString(3);
                                clientsInfo.address = reader.GetString(4);
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }





        }




        public void OnPost() {

            Console.WriteLine("Pst working");

            clientsInfo.id = Request.Form["id"];
            clientsInfo.name = Request.Form["name"];
            clientsInfo.email = Request.Form["email"];
            clientsInfo.phone = Request.Form["phone"];
            clientsInfo.address = Request.Form["address"];

            if (clientsInfo.name.Length == 0)
            {
                errorMessage = "There is null value";
                return;
            }


            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "UPDATE clients SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clientsInfo.id);
                        command.Parameters.AddWithValue("@name", clientsInfo.name);
                        command.Parameters.AddWithValue("@email", clientsInfo.email);
                        command.Parameters.AddWithValue("@phone", clientsInfo.phone);
                        command.Parameters.AddWithValue("@address", clientsInfo.address);
                        command.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            errorMessage = "Client updated";
            Response.Redirect("/Clients");
        }



    }
    
}
