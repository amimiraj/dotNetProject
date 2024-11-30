using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class CreateClientsModel : PageModel
    {

        public ClientsInfo clientsInfo= new ClientsInfo();
        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {
        }


        public void OnPost() {

            clientsInfo.name = Request.Form["name"];
            clientsInfo.email = Request.Form["email"];
            clientsInfo.phone = Request.Form["phone"];
            clientsInfo.address = Request.Form["address"];

            if (clientsInfo.name.Length == 0)
            {
                errorMessage = "There is null value";
                return;
            }




            try {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO clients" + "(name, email, phone, address)" + " VALUES (@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientsInfo.name);
                        command.Parameters.AddWithValue("@email", clientsInfo.email);
                        command.Parameters.AddWithValue("@phone", clientsInfo.phone);
                        command.Parameters.AddWithValue("@address", clientsInfo.address);

                        command.ExecuteNonQuery();
                    }
                }


            } catch (Exception ex) { 

                errorMessage = ex.Message;
                return;
            }



            clientsInfo.name = "";
            clientsInfo.email = "";
            clientsInfo.phone = ""; 
            clientsInfo.address = "";

            successMessage = "Client added";

            Response.Redirect("/Clients");

        }
    }
}
