using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Interfaces.Managers
{
    public interface IBankManager
    {
        Task<Bank> GetBanks();
    }
}
