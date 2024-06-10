using Microsoft.Extensions.Logging;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Models;
using TechnicalTest.Infrastructure.Data;
using TechnicalTest.Infrastructure.Entities;
using TechnicalTest.Infrastructure.Enums;
using TechnicalTest.Infrastructure.Mappers;
using TechnicalTest.Core.Utiities;
using TechnicalTest.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataEntities _context;
        protected readonly ILogger<CustomerRepository> _log;

        public CustomerRepository(DataEntities context, ILogger<CustomerRepository> log)
        {
            _context = context;
            _log = log;
        }

        public async Task<ResponseModel> SaveCustomer(CustomerDTO model)
        {
            try
            {
                Customer customer = model.Map();

                var saved = await _context.Customers.AddAsync(customer);

                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Succeeded = saved != null,
                    Message = "Completed",
                    Data = saved != null ? saved.Entity.Id : 0,
                    StatusCode = 200
                };
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

        public async Task<ResponseModel> ActivateCustomer(string phoneNumber)
        {
            try
            {
                var customer = await (from c in _context.Customers
                                where c.PhoneNumber.Equals(phoneNumber)
                                select c).FirstOrDefaultAsync();

                if(customer != null)
                {
                    customer.RegistrationStatus = OnboardStatus.Active.ToString();
                }

                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Succeeded = customer != null,
                    Message = "Completed",
                    Data = customer,
                    StatusCode = 200
                };
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

        public async Task<Page<CustomerDTO>> ListCustomers(int pageNumber, int pageSize)
        {
            var customers = from c in _context.Customers 
                            where c.RegistrationStatus.Equals(OnboardStatus.Active.ToString())
                            orderby c.CreatedOn descending
                            select c;
            var customerDtos = customers.OrderByDescending(x => x.CreatedOn).Select(x => x.Map());
            
            return await customerDtos.ToPageListAsync(pageNumber, pageSize);
        }
    }
}
