using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_CityList", dt);
        }

        public IActionResult Delete(int CityID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_DeleteByPK";
            cmd.Parameters.AddWithValue("@CityID", CityID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? CityID)
        {
            /*String str1 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_DropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            conn1.Close();

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = Convert.ToString(dr["StateName"]);
                list1.Add(vlst);
            }
            ViewBag.LOC_StateList = list1;*/




            String str2 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_Country_DropDown";
            DataTable dt2 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt2.Load(sdr2);
            conn2.Close();

            List<LOC_CountryDropDownModel> list2 = new List<LOC_CountryDropDownModel>();

            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = Convert.ToString(dr["CountryName"]);
                list2.Add(vlst);
            }

            ViewBag.LOC_CountryList = list2;

            List<LOC_StateDropDownModel> list3 = new List<LOC_StateDropDownModel>();
            ViewBag.LOC_StateList = list3;



            if (CityID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_CityModel modelLOC_City = new LOC_CityModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.PinCode = Convert.ToInt32(dr["PinCode"]);
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.Modification = Convert.ToDateTime(dr["Modification"]);
                }
                return View("LOC_CityAddEdit", modelLOC_City);
            }
            return View("LOC_CityAddEdit");
        }

        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (modelLOC_City.CityID == null)
            {
                cmd.CommandText = "PR_LOC_City_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = modelLOC_City.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_City_UpdateByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;
            }
            cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            cmd.Parameters.Add("@PinCode", SqlDbType.Int).Value = modelLOC_City.PinCode;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            cmd.Parameters.Add("@Modification", SqlDbType.DateTime).Value = modelLOC_City.Modification;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_City.CityID == null)
                {
                    TempData["CityInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CityInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return RedirectToAction("Add");
        }

        public IActionResult DropDownByCountry(int CountryID)
        {
            String str1 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_DropDownByCountryID";
            cmd1.Parameters.AddWithValue("@CountryID",CountryID);
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            conn1.Close();

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = Convert.ToString(dr["StateName"]);
                list1.Add(vlst);
            }
            var vModel = list1;
            return Json(vModel);
        }
    }
}
