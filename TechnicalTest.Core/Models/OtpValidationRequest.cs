using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Core.Models
{
    public class OtpValidationRequest
    {
        [Required(ErrorMessage = "OTP is require")]
        [MaxLength(6, ErrorMessage = "OTP must be 6 characters")]
        public string Otp { get; set; }

        [Required(ErrorMessage = "Phone number is require")]
        [MaxLength(16, ErrorMessage = "Phone number must not be more than 16 characters")]
        public string PhoneNumber { get; set; }
    }
}
