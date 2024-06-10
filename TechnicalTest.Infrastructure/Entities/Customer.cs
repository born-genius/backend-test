using System.ComponentModel.DataAnnotations;
using TechnicalTest.Infrastructure.Enums;
namespace TechnicalTest.Infrastructure.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(16)]
        public string PhoneNumber { get; set; }


        [StringLength(254)]
        public string Email { get; set; }

        public string Password { get; set; }
        public string RegistrationStatus { get; set; } = OnboardStatus.PendingVerification.ToString();

        public int CustomerLGAId { get; set; }
        public CustomerLGA CustomerLGA { get; set; } //Navigation property

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
