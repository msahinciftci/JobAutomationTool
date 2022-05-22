using System;

namespace JobConsole.Models
{
    public class CustomerModel
    {
        public int RowNumber { get; set; }
        public int Group { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string IpAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
