using System;
using System.Collections;
using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        //I will add more Patient info
        public ICollection<Encounter> Encounters { get; set; }
    }
}
