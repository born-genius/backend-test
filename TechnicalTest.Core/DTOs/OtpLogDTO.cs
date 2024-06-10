namespace TechnicalTest.Core.DTOs
{
    public class OtpLogDTO
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public string OTP { get; set; }
        public bool IsUsed { get; set; }
    }
}
