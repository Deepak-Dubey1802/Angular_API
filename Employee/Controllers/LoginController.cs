using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        [HttpPost]

        public bool Loginuser(login lg)
        {
            NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; Database = BloodBank; User Id = postgres; Password = 1802;");


            string query = "Select email,password from employee where email =@email and password =@password ";
            NpgsqlCommand cm = new NpgsqlCommand(query, con);
            con.Open();
            cm.Parameters.AddWithValue("@email", lg.email);
            cm.Parameters.AddWithValue("@password", lg.password);
            NpgsqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                return true;

            }
            return false;

        }

    }
}
