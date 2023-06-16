using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Drawing;


namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class employeeController : ControllerBase
    {

        NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; Database = BloodBank; User Id = postgres; Password = 1802;");



        [HttpGet]
        public List<employee> GetEmployees()
        {
            // String cs = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            List<employee> EmployeeList = new List<employee>();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from employee", con);
            // cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                employee emp = new employee();


                emp.employeeid = Convert.ToInt32(rdr.GetValue(0).ToString());
                emp.firstname = rdr.GetValue(1).ToString();


                emp.lastname = rdr.GetValue(2).ToString();
                emp.email = rdr.GetValue(3).ToString();
                emp.password = rdr.GetValue(4).ToString();
                emp.addressline = rdr.GetValue(5).ToString();
                emp.city = rdr.GetValue(6).ToString();

                



                EmployeeList.Add(emp);

            }

            con.Close();
            return EmployeeList;
        }

        [HttpPost]
        public bool AddEmployee(employee emp)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("insert into employee (firstname,lastname,email,password,addressline,city) values(@firstname,@lastname,@email,@password,@addressline,@city)", con);
            //  cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@firstname", emp.firstname);

            cmd.Parameters.AddWithValue("@lastname", emp.lastname);
            cmd.Parameters.AddWithValue("@email", emp.email);
            cmd.Parameters.AddWithValue("@password", emp.password);
            cmd.Parameters.AddWithValue("@addressline", emp.addressline);
            cmd.Parameters.AddWithValue("@city", emp.city);
            
            // NpgsqlCommand cmd = new NpgsqlCommand("insert into donor values(@donorid,@fullname,@lastdonationdate,@phoneno,@panno,@address,@email,@password,@cityid,@stateid,@countryid,@bloodgroupid,@genderid", con);

            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();


            if (i > 0)
            {
                return true;

            }
            else
            {
                return false;
            }


        }


        [HttpPut]

        public bool UpdateEployee(employee emp)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("update  employee set firstname =@firstname,lastname =@lastname,email =@email,password =@password,addressline =@addressline,city =@city where employeeid=@employeeid ", con);
            // cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employeeid", emp.employeeid);

            cmd.Parameters.AddWithValue("@firstname", emp.firstname);
            cmd.Parameters.AddWithValue("@lastname", emp.lastname);
            cmd.Parameters.AddWithValue("@email", emp.email);
            cmd.Parameters.AddWithValue("@password", emp.password);
            cmd.Parameters.AddWithValue("@addressline", emp.addressline);
            cmd.Parameters.AddWithValue("@city", emp.city);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;

            }
            else
            {
                return false;
            }


        }


        [HttpDelete]

        public bool DeleteEmployee(int employeeid)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from employee where employeeid= @employeeid", con);



            cmd.Parameters.AddWithValue("@employeeid", employeeid);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;

            }
            else
            {
                return false;
            }


        }



    }
}
