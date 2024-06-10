using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Interfaces.ExternalServices
{
    public interface IBankService
    {
        Task<Bank> GetBanks(string url, string token = null);
    }
}
