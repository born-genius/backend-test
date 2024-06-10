using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Models;
using TechnicalTest.Core.Utiities;

namespace TechnicalTest.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<Page<CustomerDTO>> ListCustomers(int pageNumber, int pageSize);
        Task<ResponseModel> SaveCustomer(CustomerDTO model);
        Task<ResponseModel> ActivateCustomer(string phoneNumber);
    }
}
