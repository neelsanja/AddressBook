﻿namespace AddressBook.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        public string StateName { get; set; }

        public string StateCode { get; set; }

        public int CountryID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime Modification { get; set; }
    }

    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
