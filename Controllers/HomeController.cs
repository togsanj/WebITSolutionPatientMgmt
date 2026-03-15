using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebITPatientMgmt.Models;

namespace WebITPatientMgmt.Controllers
{
    public class HomeController : Controller
    {
string CS = "Data Source=ROHIT-U6JU28T5\\SQLEXPRESS;Initial Catalog=CRUD_Practice_StoredProcedure;Integrated Security=True;Encrypt=False";
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Class Patient)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spWebItPatient", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    Patient.PtotalAttr = Patient.PRoReAttr * Patient.PnoAttr; 

                    cmd.Parameters.AddWithValue("@PatientName", Patient.PNameAttr);
                    cmd.Parameters.AddWithValue("@PatientAge", Patient.PAgeAttr);
                    cmd.Parameters.AddWithValue("@AdmissionDate", Patient.PAdmsnAttr);
                    cmd.Parameters.AddWithValue("@Investigation", Patient.PInvestAttr);
                    cmd.Parameters.AddWithValue("@DischargeDate", Patient.PDiscDate);
                    cmd.Parameters.AddWithValue("@RoomRent", Patient.PRoReAttr);
                    cmd.Parameters.AddWithValue("@Noofdays", Patient.PnoAttr);
                    cmd.Parameters.AddWithValue("@TotalAmount", Patient.PtotalAttr);
                    cmd.Parameters.AddWithValue("@Action", 1);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return Content("<script>alert('data inserted successfully');location.href='/Home/About'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('data insert failed');location.href='Home/Index'</script>");
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex.Message;
            }
            return View();
        }

        public ActionResult About()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spWebItPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", 2);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
             }
            return View(dt);
        }

        public ActionResult Contact(int? PatientID)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spWebItPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.Parameters.AddWithValue("@Action", 3);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            if (dt.Rows.Count > 0)
            {
                return View(dt.Rows[0]);
            }
            else
            {
                return HttpNotFound("record not found");
            }        
        }

        [HttpPost]
        public ActionResult Contact(Class Update)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spWebItPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Update.PtotalAttr = Update.PRoReAttr * Update.PnoAttr; 

                cmd.Parameters.AddWithValue("@PatientID", Update.PatientID);
                cmd.Parameters.AddWithValue("@PatientName", Update.PNameAttr);
                cmd.Parameters.AddWithValue("@PatientAge", Update.PAgeAttr);
                cmd.Parameters.AddWithValue("@AdmissionDate", Update.PAdmsnAttr);
                cmd.Parameters.AddWithValue("@Investigation", Update.PInvestAttr);
                cmd.Parameters.AddWithValue("@DischargeDate", Update.PDiscDate);
                cmd.Parameters.AddWithValue("@RoomRent", Update.PRoReAttr);
                cmd.Parameters.AddWithValue("@Noofdays", Update.PnoAttr);
                cmd.Parameters.AddWithValue("@TotalAmount", Update.PtotalAttr);
                cmd.Parameters.AddWithValue("@Action", 4);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if(result>0)
                {
                    return Content("<script>alert('data updated successfully');location.href='/Home/About'</script>");
                }
    else
    {
        return Content("<script>alert('data update failed');location.href='Home/Contact?PatientID=" + Update.PatientID + "'</script>");
    }
            }
        }

        public ActionResult DeletePatientById(int PatientID)
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spWebItPatient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientID", PatientID);
                cmd.Parameters.AddWithValue("@Action", 5);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return Content("<script>alert('data deleted successfully');location.href='/Home/About'</script>");
                }
        else
        {
            return Content("<script>alert('data delete failed');location.href='/Home/Contact?PatientID'=" + PatientID + "</script>");
        }
            }
        }
    }
}