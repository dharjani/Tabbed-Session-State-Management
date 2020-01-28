using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RegisterModel
    {
        ////[Required]
        ////[Display(Name = "First name")]
        public string FirstName { get; set; }

        ////[Required]
        ////[Display(Name = "Last name")]
        public string LastName { get; set; }

        ////[Required]
        ////[Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        ////[Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        ////[Display(Name = "Landmark")]
        public string Landmark { get; set; }

        ////[Required]
        ////[Display(Name = "City")]
        public string City { get; set; }

        ////[Required]
        ////[Display(Name = "State")]
        public string State { get; set; }

        ////[Required]
        ////[Display(Name = "Country")]
        public string Country { get; set; }

        ////[Required]
        ////[Display(Name = "Pin Code")]
        public string PinCode { get; set; }

        ////[Required]
        ////[Display(Name = "Primary Contact Name")]
        public string PrimaryContactName { get; set; }

        ////[Required]
        [DataType(DataType.EmailAddress)]
        ////[Display(Name = "Primary Email Id")]
        public string PrimaryEmailId { get; set; }

        [DataType(DataType.EmailAddress)]
        ////[Display(Name = "Secondary Email Id")]
        public string SecondaryEmailId { get; set; }

        ////[Required]
        [DataType(DataType.PhoneNumber)]
        ////[Display(Name = "Primary Contact Number")]
        public string PrimaryContactNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        ////[Display(Name = "Secondary Contact Number")]
        public string ContactNumber2 { get; set; }

    }
}