using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Interfaces.Repositories
{
    public interface IOtpRepository
    {
        Task<ResponseModel> SaveOtp(string otp, string phoneNumber);
        Task<OtpLogDTO> GetOtp(string otp, string phoneNumber);
        Task<ResponseModel> UpdateUsedOtp(int id);
    }
}
