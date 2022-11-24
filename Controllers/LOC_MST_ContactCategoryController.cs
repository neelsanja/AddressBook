using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_MST_ContactCategoryController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_MST_ContactCategory_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_MST_ContactCategoryList", dt);
        }

        public IActionResult Delete(int ContactCategoryID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_MST_ContactCategory_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_MST_ContactCategory_SelectByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_MST_ContactCategoryModel modelLOC_MST_ContactCategory = new LOC_MST_ContactCategoryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_MST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelLOC_MST_ContactCategory.ContactCategoryName = dr["StateName"].ToString();
                    modelLOC_MST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_MST_ContactCategory.Modification = Convert.ToDateTime(dr["Modification"]);
                }
                return View("LOC_MST_ContactCategoryAddEdit", modelLOC_MST_ContactCategory);
            }
            return View("LOC_MST_ContactCategoryAddEdit");
        }

        public IActionResult Save(LOC_MST_ContactCategoryModel modelLOC_MST_ContactCategory)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (modelLOC_MST_ContactCategory.ContactCategoryID == 0)
            {
                cmd.CommandText = "PR_LOC_MST_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = modelLOC_MST_ContactCategory.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_MST_ContactCategory_UpdateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelLOC_MST_ContactCategory.ContactCategoryID;
            }
            cmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelLOC_MST_ContactCategory.ContactCategoryName;
            cmd.Parameters.Add("@Modification", SqlDbType.DateTime).Value = modelLOC_MST_ContactCategory.Modification;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_MST_ContactCategory.ContactCategoryID == 0)
                {
                    TempData["ContactCategoryInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ContactCategoryInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return View("LOC_MST_ContactCategoryAddEdit");
        }
    }
}
