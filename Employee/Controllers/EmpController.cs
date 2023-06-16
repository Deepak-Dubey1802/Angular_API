using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {

        NpgsqlConnection con = new NpgsqlConnection("Server = localhost; Port = 5432; Database = BloodBank; User Id = postgres; Password = 1802;");



        [HttpGet]
        public List<Emp> GetEmps()
        {
            // String cs = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            List<Emp> EmpList = new List<Emp>();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from emp", con);
            // cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Emp emp = new Emp();


                emp.employeeid = Convert.ToInt32(rdr.GetValue(0).ToString());
                emp.firstname = rdr.GetValue(1).ToString();


                emp.lastname = rdr.GetValue(2).ToString();
                emp.gender = rdr.GetValue(3).ToString();
                emp.designation = rdr.GetValue(4).ToString();
                emp.salary = Convert.ToInt32(rdr.GetValue(5).ToString());
                emp.email = rdr.GetValue(6).ToString();
                emp.city = rdr.GetValue(7).ToString();






                EmpList.Add(emp);

            }

            con.Close();
            return EmpList;
        }




        [HttpPost]
        public bool AddEmp(Emp emp)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("insert into emp (firstname,lastname,gender,designation,salary,email,city) values(@firstname,@lastname,@gender,@designation,@salary,@email,@city)", con);
            //  cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstname", emp.firstname);

            cmd.Parameters.AddWithValue("@lastname", emp.lastname);
            cmd.Parameters.AddWithValue("@gender", emp.gender);
            cmd.Parameters.AddWithValue("@designation", emp.designation);
            cmd.Parameters.AddWithValue("@salary", emp.salary);
            cmd.Parameters.AddWithValue("@email", emp.email);
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
        public bool UpdateEmp(Emp emp)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("update  emp set firstname =@firstname,lastname =@lastname,gender =@gender,designation =@designation,salary =@salary,email =@email,city =@city where employeeid=@employeeid", con);
            //  cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@employeeid", emp.employeeid);

            cmd.Parameters.AddWithValue("@firstname", emp.firstname);

            cmd.Parameters.AddWithValue("@lastname", emp.lastname);
            cmd.Parameters.AddWithValue("@gender", emp.gender);
            cmd.Parameters.AddWithValue("@designation", emp.designation);
            cmd.Parameters.AddWithValue("@salary", emp.salary);
            cmd.Parameters.AddWithValue("@email", emp.email);
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



        [HttpDelete]

        public bool DeleteEmp(int employeeid)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from emp where employeeid= @employeeid", con);



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
