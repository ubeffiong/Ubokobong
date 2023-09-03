using System.ComponentModel.DataAnnotations;

namespace IHVNMedix.Models
{
    public class HealthItem
    {
        [Key] // This specifies that 'Id' is the primary key
        public int Id { get; set; }
    }
}
