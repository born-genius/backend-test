using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Core.Interfaces.Managers;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IBankManager _bankManager;

        public BankController(IBankManager bankManager)
        {
            _bankManager = bankManager;
        }

        [HttpGet("ListBanks")]
        public async Task<IActionResult> ListBanks()
        {
            var result = await _bankManager.GetBanks();

            return Ok(result);
        }
    }
}
