using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_State_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_StateList", dt);
        }

        public IActionResult Delete(int StateID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_DeleteByPK";
            cmd.Parameters.AddWithValue("@StateID", StateID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? StateID)
        {
            String str1 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_DropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            conn1.Close();  

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = Convert.ToString(dr["CountryName"]);
                list.Add(vlst); 
            }

            ViewBag.LOC_CountryList = list;


            if (StateID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_StateModel modelLOC_State = new LOC_StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.Modification = Convert.ToDateTime(dr["Modification"]);
                }
                return View("LOC_StateAddEdit", modelLOC_State);
            }
            return View("LOC_StateAddEdit");
        }

        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (modelLOC_State.StateID == null)
            {
                cmd.CommandText = "PR_LOC_State_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = modelLOC_State.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_State_UpdateByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;
            }
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = modelLOC_State.StateName;
            cmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
            cmd.Parameters.Add("@Modification", SqlDbType.DateTime).Value = modelLOC_State.Modification;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_State.StateID == null)
                {
                    TempData["StateInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["StateInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return RedirectToAction("Add");
        }
    }
}
