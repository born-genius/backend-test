using Microsoft.EntityFrameworkCore;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Interfaces.Repositories;
using TechnicalTest.Core.Models;
using TechnicalTest.Infrastructure.Data;
using TechnicalTest.Infrastructure.Entities;

namespace TechnicalTest.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly DataEntities _context;

        public OtpRepository(DataEntities context)
        {
            _context = context;
        }

        public async Task<ResponseModel> SaveOtp(string otp, string phoneNumber)
        {
            OtpLog log = new OtpLog
            {
                OTP = otp,
                PhoneNumber = phoneNumber,
                ExpiresOn = DateTime.Now.AddMinutes(30),
                CreatedOn = DateTime.Now
            };

            var otpLog = await _context.OtpLogs.AddAsync(log);

            await _context.SaveChangesAsync();

            return new ResponseModel
            {
                Succeeded = otpLog != null,
                Message = "Completed"
            };
        }

        public async Task<OtpLogDTO> GetOtp(string otp, string phoneNumber)
        {
            var otpLog = await (from t in _context.OtpLogs
                                where t.PhoneNumber == phoneNumber
                                && t.OTP.Equals(otp)
                                && t.ExpiresOn >= DateTime.Now
                                && !t.IsUsed
                                select new OtpLogDTO
                                {
                                    Id = t.Id,
                                    PhoneNumber = t.PhoneNumber,
                                    OTP = t.OTP,
                                    IsUsed = t.IsUsed
                                })
                                .FirstOrDefaultAsync();
            return otpLog;
        }

        public async Task<ResponseModel> UpdateUsedOtp(int id)
        {
            var otpLog = await (from t in _context.OtpLogs
                                where t.Id == id
                                select t)
                                .FirstOrDefaultAsync();
            if (otpLog != null)
            {
                otpLog.IsUsed = true;
                await _context.SaveChangesAsync();
            }

            return new ResponseModel
            {
                Succeeded = otpLog != null,
                Message = "Completed",
                Data = otpLog
            };
        }
    }
}
