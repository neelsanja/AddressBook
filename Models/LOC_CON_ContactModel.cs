namespace AddressBook.Models
{
    public class LOC_CON_ContactModel
    {
        public int ContactID { get; set; }

        public int ContactCategoryID { get; set; }

        public int CountryID { get; set; }

        public int StateID { get; set; }

        public int CityID { get; set; }
        public string ContactName { get; set; }

        public string Address { get; set; }

        public int PinCode { get; set; }

        public int Mobile { get; set; }

        public int AlternetContact { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string LinkedIn { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Modification { get; set; }
    }
}
