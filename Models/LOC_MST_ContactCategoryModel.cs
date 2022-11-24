namespace AddressBook.Models
{
    public class LOC_MST_ContactCategoryModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Modification { get; set; }
    }
    public class LOC_MST_ContactCategoryDropDownModel
    {
        public int ContactCategoryID { get; set; }
        public string ContactCategoryName { get; set; }
    }
}
