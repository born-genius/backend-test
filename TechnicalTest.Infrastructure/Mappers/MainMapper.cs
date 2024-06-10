using TechnicalTest.Core.DTOs;
using TechnicalTest.Infrastructure.Entities;

namespace TechnicalTest.Infrastructure.Mappers
{
    public static class MainMapper
    {
        public static CustomerDTO Map(this Customer model)
        {
            if (model == null) return null;

            return new CustomerDTO
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                CustomerLGAId = model.CustomerLGAId
            };
        }
        public static Customer Map(this CustomerDTO entity)
        {
            if (entity == null) return null;

            return new Customer
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.Password,
                PhoneNumber = entity.PhoneNumber,
                CustomerLGAId = entity.CustomerLGAId
            };
        }
    }
}
