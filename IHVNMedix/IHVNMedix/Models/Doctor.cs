namespace IHVNMedix.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } //= string.Empty;
        public string PhoneNumber { get; set; } 
        public string EmailAddress { get; set; }
        public string Specialty { get; set; }
    }
}
