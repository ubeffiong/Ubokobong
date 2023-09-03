using System.Collections.Generic;

namespace IHVNMedix.Models
{
    public class ApiResponse<T>
    {
        public List<T> Items { get; set; }
    }
}
