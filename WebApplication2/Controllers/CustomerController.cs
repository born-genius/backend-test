using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Core.DTOs;
using TechnicalTest.Core.Interfaces.Managers;
using TechnicalTest.Core.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;

        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        [HttpPost("Onboard")]
        public async Task<IActionResult> OnboardCustomer([FromBody] CustomerDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerManager.OnboardCustomer(model);

                return StatusCode(result.StatusCode, result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("ValidateOtp")]
        public async Task<IActionResult> ActivateCustomer([FromBody] OtpValidationRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerManager.ValidateCustomerOtp(model);

                return StatusCode(result.StatusCode, result);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("ListCustomers")]
        public async Task<IActionResult> ListCustomers([FromQuery] int pageSize = 50,
            [FromQuery] int pageNumber = 1)
        {
            var result = await _customerManager.ListCustomers(pageNumber, pageSize);

            return Ok(result);
        }
    }
}
