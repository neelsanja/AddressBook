using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class LOC_CON_ContactController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CON_ContactController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_CON_Contact_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_CON_ContactList", dt);
        }
        public IActionResult Delete(int ContactID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_CON_Contact_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Add(int? ContactID)
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

            List<LOC_CityDropDownModel> list5 = new List<LOC_CityDropDownModel>();
            ViewBag.LOC_CityList = list5;




            /*String str3 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn3 = new SqlConnection(str3);
            conn3.Open();
            SqlCommand cmd3 = conn3.CreateCommand();
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            cmd3.CommandText = "PR_LOC_City_DropDown";
            DataTable dt3 = new DataTable();
            SqlDataReader sdr3 = cmd3.ExecuteReader();
            dt3.Load(sdr3);
            conn3.Close();

            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();

            foreach (DataRow dr in dt3.Rows)
            {
                LOC_CityDropDownModel vlst = new LOC_CityDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = Convert.ToString(dr["CityName"]);
                list3.Add(vlst);
            }

            ViewBag.LOC_CityList = list3;*/









            String str4 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn4 = new SqlConnection(str4);
            conn4.Open();
            SqlCommand cmd4 = conn4.CreateCommand();
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            cmd4.CommandText = "PR_LOC_MST_ContactCategory_DropDown";
            DataTable dt4 = new DataTable();
            SqlDataReader sdr4 = cmd4.ExecuteReader();
            dt4.Load(sdr4);
            conn4.Close();

            List<LOC_MST_ContactCategoryDropDownModel> list4 = new List<LOC_MST_ContactCategoryDropDownModel>();

            foreach (DataRow dr in dt4.Rows)
            {
                LOC_MST_ContactCategoryDropDownModel vlst = new LOC_MST_ContactCategoryDropDownModel();
                vlst.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst.ContactCategoryName = Convert.ToString(dr["ContactCategoryName"]);
                list4.Add(vlst);
            }

            ViewBag.LOC_MST_ContactCategoryList = list4;




            if (ContactID != null)
            {
                String str = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_CON_Contact_SelectByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                LOC_CON_ContactModel modelLOC_CON_Contact = new LOC_CON_ContactModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_CON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelLOC_CON_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelLOC_CON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_CON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_CON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_CON_Contact.ContactName = dr["ContactName"].ToString();
                    modelLOC_CON_Contact.Address = dr["Address"].ToString();
                    modelLOC_CON_Contact.PinCode = Convert.ToInt32(dr["PinCode"]);
                    modelLOC_CON_Contact.Mobile = Convert.ToInt32(dr["Mobile"]);
                    modelLOC_CON_Contact.AlternetContact = Convert.ToInt32(dr["AlternetContact"]);
                    modelLOC_CON_Contact.Email = dr["Email"].ToString();
                    modelLOC_CON_Contact.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                    modelLOC_CON_Contact.LinkedIn = dr["LinkedIn"].ToString();
                    modelLOC_CON_Contact.Twitter = dr["Twitter"].ToString();
                    modelLOC_CON_Contact.Instagram = dr["Instagram"].ToString();
                    modelLOC_CON_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_CON_Contact.Modification = Convert.ToDateTime(dr["Modification"]);
                }
                return View("LOC_CON_ContactAddEdit", modelLOC_CON_Contact);
            }
            return View("LOC_CON_ContactAddEdit");
        }

        public IActionResult Save(LOC_CON_ContactModel modelLOC_CON_Contact)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (modelLOC_CON_Contact.ContactID == 0)
            {
                cmd.CommandText = "PR_LOC_CON_Contact_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = modelLOC_CON_Contact.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelLOC_CON_Contact.ContactID;
            }
            cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelLOC_CON_Contact.ContactCategoryID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_CON_Contact.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_CON_Contact.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_CON_Contact.CityID;
            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.ContactName;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.Address;
            cmd.Parameters.Add("@PinCode", SqlDbType.Int).Value = modelLOC_CON_Contact.PinCode;
            cmd.Parameters.Add("@Mobile", SqlDbType.Int).Value = modelLOC_CON_Contact.Mobile;
            cmd.Parameters.Add("@AlternetContact", SqlDbType.Int).Value = modelLOC_CON_Contact.AlternetContact;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.Email;
            cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = modelLOC_CON_Contact.BirthDate;
            cmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.LinkedIn;
            cmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.Twitter;
            cmd.Parameters.Add("@Instagram", SqlDbType.NVarChar).Value = modelLOC_CON_Contact.Instagram;
            cmd.Parameters.Add("@Modification", SqlDbType.DateTime).Value = modelLOC_CON_Contact.Modification;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_CON_Contact.ContactID == 0)
                {
                    TempData["ContactInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ContactInsertMsg"] = "Record Updated Successfully";
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
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
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

        public IActionResult DropDownByState(int StateID)
        {
            String str1 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_City_DropDownByStateID";
            cmd1.Parameters.AddWithValue("@StateID", StateID);
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            conn1.Close();

            List<LOC_CityDropDownModel> list1 = new List<LOC_CityDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CityDropDownModel vlst = new LOC_CityDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = Convert.ToString(dr["CityName"]);
                list1.Add(vlst);
            }
            var vModel = list1;
            return Json(vModel);
        }
    }
}
