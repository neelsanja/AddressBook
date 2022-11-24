using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Controllers
{
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_Country_SelectAll";
            DataTable dt=new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_CountryList",dt);
        }
        [HttpPost]
        public IActionResult Delete(int CountryID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_DeleteByPK";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? CountryID)
        {
            if (CountryID!=null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_CountryModel modelLOC_Country=new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.Modification = Convert.ToDateTime(dr["Modification"]);
                }
                return View("LOC_CountryAddEdit",modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }

        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (modelLOC_Country.CountryID == null)
            {
                cmd.CommandText = "PR_LOC_Country_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = modelLOC_Country.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_Country_UpdateByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
            }
            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = modelLOC_Country.CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = modelLOC_Country.CountryCode;
            cmd.Parameters.Add("@Modification", SqlDbType.DateTime).Value = modelLOC_Country.Modification;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_Country.CountryID == null)
                {
                    TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CountryInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return View("LOC_CountryAddEdit");
        }
    }
}
    