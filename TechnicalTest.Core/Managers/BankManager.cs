using TechnicalTest.Core.Interfaces.ExternalServices;
using TechnicalTest.Core.Interfaces.Managers;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Managers
{
    public class BankManager : IBankManager
    {
        private readonly IBankService _bankService;
        private readonly ApiOptions _apiOptions;

        public BankManager(IBankService bankService, ApiOptions apiOptions)
        {
            _bankService = bankService;
            _apiOptions = apiOptions;
        }

        public async Task<Bank> GetBanks()
        {
            return await _bankService.GetBanks(_apiOptions.WemaAlat);
        }
    }
}
