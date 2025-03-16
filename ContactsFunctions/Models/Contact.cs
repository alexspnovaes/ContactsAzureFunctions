using System.ComponentModel.DataAnnotations;

namespace ContactsFunctions.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Ddd { get; set; }
    }
}
