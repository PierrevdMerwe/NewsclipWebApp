using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsclipWebApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace NewsclipWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ProfileController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select ID, FirstName, LastName, Email, PhoneNumber, Links, Industry, Skills, Experience from dbo.Profile";
            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand=new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Profile profile)
        {
            string query = @"insert into dbo.Profile (FirstName, LastName, Email, PhoneNumber, Links, Industry, Skills, Experience) values (@FirstName, @LastName, @Email, @PhoneNumber, @Links, @Industry, @Skills, @Experience)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", profile.LastName);
                    myCommand.Parameters.AddWithValue("@Email", profile.Email);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", profile.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@Links", profile.Links);
                    myCommand.Parameters.AddWithValue("@Industry", profile.Industry);
                    myCommand.Parameters.AddWithValue("@Skills", profile.Skills);
                    myCommand.Parameters.AddWithValue("@Experience", profile.Experience);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Profile profile)
        {
            string query = @"update dbo.Profile set FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, Links=@Links, Industry=@Industry, Skills=@Skills, Experience=@Experience where ID=@ID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", profile.ID);
                    myCommand.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", profile.LastName);
                    myCommand.Parameters.AddWithValue("@Email", profile.Email);
                    myCommand.Parameters.AddWithValue("@PhoneNumber", profile.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@Links", profile.Links);
                    myCommand.Parameters.AddWithValue("@Industry", profile.Industry);
                    myCommand.Parameters.AddWithValue("@Skills", profile.Skills);
                    myCommand.Parameters.AddWithValue("@Experience", profile.Experience);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Profile where ID=@ID";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            } 
            catch (Exception)
            {
                return new JsonResult("anonymous.jpg");
            }
        }
    }
}
