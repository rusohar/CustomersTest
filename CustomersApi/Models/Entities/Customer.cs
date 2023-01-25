using System.ComponentModel.DataAnnotations;

namespace CustomersApi.Models.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string CompanyName { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
