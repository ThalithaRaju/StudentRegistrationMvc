using StudentRegistrationMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationMvc.Controllers
{
    public class StudentController : Controller
    {
       
        // GET: Student
        public ActionResult  StudentRegistration ()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult StudentRegistration(Student s)
        {
            SqlConnection conn = new SqlConnection(@"Server=LAPTOP-Q2N7MAED\SQLEXPRESS ;Database=StudentRegistration;Trusted_Connection=True");
            conn.Open();
            SqlCommand cmdd = new SqlCommand("StudentSave", conn);
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.AddWithValue("@Name", s.Name);
            cmdd.Parameters.AddWithValue("@Maths", s.Maths);
            cmdd.Parameters.AddWithValue("@Chemistry", s.Chemistry);
            cmdd.Parameters.AddWithValue("@Physics", s.Physics);
            cmdd.Parameters.AddWithValue("@Phone", s.PhoneNumber);
            cmdd.Parameters.AddWithValue("@Email", s.Email);
            cmdd.Parameters.AddWithValue("@Address", s.Address);
            cmdd.ExecuteNonQuery();
            //  cmdd.Parameters.AddWithValue("@CollegeId",);
            conn.Close();

          
          return RedirectToAction("StudentList");
           
        }
        public ActionResult StudentList()
        {
            List<Student> students = new List<Student>();
            SqlConnection con = new SqlConnection(@"Server=LAPTOP-Q2N7MAED\SQLEXPRESS ;Database=StudentRegistration;Trusted_Connection=True");
            con.Open();
            // MessageBox.Show("connected");
            SqlCommand cmd = new SqlCommand("StudentGet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Id", 3);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student student = new Student();
               student.Id = Convert.ToInt32(dr["Id"]); 
               student.Name = dr["Name"].ToString();
                student.Total = Convert.ToInt32(dr["Total"]);
                student.PhoneNumber = dr["Phone"].ToString();
                student.Email = dr["Email"].ToString();
               // student.Address = dr["Address"].ToString();
                students.Add(student);
            }
            con.Close();
            return View(students);
           

          
        }
    }
}