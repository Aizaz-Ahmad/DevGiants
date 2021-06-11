using System;
using System.Collections.Generic;

#nullable disable

namespace DevGiants.Models
{
    public partial class JobApplication
    {
        public int ApplicationId { get; set; }
        public string Filename { get; set; }
        public string Name { get; set; }
        public string Fathername { get; set; }
        public string Placeofposting { get; set; }
        public string Appliedfor { get; set; }
        public string Dob { get; set; }
        public string Ageinyear { get; set; }
        public string Cnic { get; set; }
        public string Domicile { get; set; }
        public string Contact { get; set; }
        public string OtherContact { get; set; }
        public string PostalAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Email { get; set; }
        public bool Already { get; set; }
        public bool Disability { get; set; }
        public bool Hafiz { get; set; }
        public bool Marital { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public DateTime AppliedAt { get; set; }
        //private string NullOrValue(string val)
        //{
        //    return String.IsNullOrEmpty(val) ? null : val;
        //}
        //public JobApplication() { }
        //public JobApplication(string filename, Microsoft.AspNetCore.Http.IFormCollection obj)
        //{
        //    this.Filename = filename;
        //    this.Name = NullOrValue(obj["name"]);
        //    this.Fathername = NullOrValue(obj["fathername"]);
        //    this.Placeofposting = NullOrValue(obj["placeofposting"]);
        //    this.Appliedfor = NullOrValue(obj["appliedfor"]);
        //    this.Ageinyear = NullOrValue(obj["ageinyear"]);
        //    this.Dob = NullOrValue(obj["dob"]);
        //    this.Cnic = NullOrValue(obj["cnic"]);
        //    this.Gender = NullOrValue(obj["gender"]);
        //    this.Domicile = NullOrValue(obj["domicile"]);
        //    this.Contact = NullOrValue(obj["contact"]);
        //    this.OtherContact = obj["otherContact"];
        //    this.PostalAddress = NullOrValue(obj["postalAddress"]);
        //    this.PermanentAddress = NullOrValue(obj["permanentAddress"]);
        //    this.Email = NullOrValue(obj["email"]);
        //    this.Religion = NullOrValue(obj["religion"]);
        //    this.Already = Boolean.Parse(obj["already"]);
        //    this.Hafiz = Boolean.Parse(obj["hafiz"]);
        //    this.Disability = Boolean.Parse(obj["disability"]);
        //    this.Marital = Boolean.Parse(obj["marital"]);
        //}
    }
}
