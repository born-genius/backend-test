using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Models;
using TechnicalTest.Core.Utiities;

namespace TechnicalTest.Core.Interfaces.Managers
{
    public interface ICustomerManager
    {
        Task<Page<CustomerDTO>> ListCustomers(int pageNumber, int pageSize);
        Task<ResponseModel> OnboardCustomer(CustomerDTO request);
        Task<ResponseModel> ValidateCustomerOtp(OtpValidationRequest request);
    }
}
