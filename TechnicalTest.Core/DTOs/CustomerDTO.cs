using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechnicalTest.Core.DTOs
{
    public class CustomerDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [StringLength(16, ErrorMessage = "Phone number should not be more than 16 characters")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address input")]
        [MaxLength(250, ErrorMessage = "Email address should not be more than 250 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(250, ErrorMessage = "Password should not be more than 250 characters")]
        public string Password { get; set; }
        public int CustomerLGAId { get; set; }
    }
}
