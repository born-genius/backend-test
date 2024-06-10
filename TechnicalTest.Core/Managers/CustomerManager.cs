using Microsoft.Extensions.Logging;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Interfaces.Managers;
using TechnicalTest.Core.Interfaces.Repositories;
using TechnicalTest.Core.Models;
using TechnicalTest.Core.Utiities;

namespace TechnicalTest.Core.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ILogger<CustomerManager> _log;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOtpRepository _otpRepository;

        public CustomerManager(ILogger<CustomerManager> log, ICustomerRepository customerRepository, IOtpRepository otpRepository)
        {
            _log = log;
            _customerRepository = customerRepository;
            _otpRepository = otpRepository;
        }

        public async Task<ResponseModel> OnboardCustomer(CustomerDTO request)
        {
            if (request == null)
            {
                return new ResponseModel
                {
                    Succeeded = false,
                    Message = "Invalid request, request body required",
                    StatusCode = 400
                };
            }

            try
            {
                request.Password = UtilEtensions.ComputeSha256Hash(request.Password);

                var result = await _customerRepository.SaveCustomer(request);

                if (result != null && result.Succeeded)
                {
                    string otp = ReferenceUtils.GenerateRandomDigits(6);

                    //Log the OTP for testing purposes. Should not be logged on production
                    _log.LogInformation($"OTP for {request.PhoneNumber}");

                    //Save OTP to the database against the customer
                    await _otpRepository.SaveOtp(otp, request.PhoneNumber);
                    result.Message = $"OTP sent to {request.PhoneNumber}, valid for 30 minutes";

                    return result;
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }

            return new ResponseModel
            {
                Succeeded = false,
                Message = "Something went wrong",
                StatusCode = 500
            };
        }

        public async Task<ResponseModel> ValidateCustomerOtp(OtpValidationRequest request)
        {
            if (request == null)
            {
                return new ResponseModel
                {
                    Succeeded = false,
                    Message = "Invalid request, request body required",
                    StatusCode = 400
                };
            }

            //Get saved otp
            var otpog = await _otpRepository.GetOtp(request.Otp, request.PhoneNumber);

            if(otpog != null)
            {
                //Both updates below should be done in a single database call with stored procedure with more time at hand
                await _customerRepository.ActivateCustomer(request.PhoneNumber);
                await _otpRepository.UpdateUsedOtp(otpog.Id);

                return new ResponseModel
                {
                    Succeeded = true,
                    Message = "Account activated",
                    StatusCode = 200
                };
            }

            return new ResponseModel
            {
                Succeeded = false,
                Message = "Validation not successful",
                StatusCode = 400
            };
        }

            public async Task<Page<CustomerDTO>> ListCustomers(int pageNumber, int pageSize)
        {
            return await _customerRepository.ListCustomers(pageNumber, pageSize);
        }
    }
}
