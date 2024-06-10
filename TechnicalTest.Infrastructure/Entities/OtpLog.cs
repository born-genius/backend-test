using System.ComponentModel.DataAnnotations;

namespace TechnicalTest.Infrastructure.Entities
{
    public class OtpLog
    {
        public int Id { get; set; }

        [MaxLength(16)]
        public string PhoneNumber { get; set; }

        [MaxLength(6)]
        public string OTP { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ExpiresOn { get; set; }
        public bool IsUsed { get; set; }
    }
}
