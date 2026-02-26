using Microsoft.AspNetCore.Mvc;
using TestProject.DTOs;
using TestProject.Interfaces;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;
        public RegistrationController(IRegistrationService service) => _service = service;

        /// <summary>
        /// Check if user is New or Migrated
        /// </summary>
        [HttpGet("status/{ic}")]
        public async Task<IActionResult> CheckStatus(string ic)
        {
            var result = await _service.GetUserStatus(ic);
            // Returns: { "success": true, "data": { "existing": false } }
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// Save Step 1 personal information
        /// </summary>
        [HttpPost("register-details")]
        public async Task<IActionResult> Step1(Step1Request req)
        {
            var userId = await _service.ProcessStep1(req);
            return Ok(new { success = true, message = "Personal details saved successfully.", data = new { userId = userId } });
        }

        /// <summary>
        /// Generate Mock OTP (1234)
        /// </summary>
        [HttpPost("generate-otp")]
        public async Task<IActionResult> RequestOtp(string ic, string type)
        {
            var otp = await _service.GenerateMockOtp(ic, type);
            return Ok(new { success = true, message = $"OTP generated successfully for {type}.", data = new { mockOtp = otp } });
        }

        /// <summary>
        /// Validate OTP and update flow to Step 2
        /// </summary>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> Verify(VerifyOtpRequest req)
        {
            await _service.ValidateOtp(req);
            return Ok(new { success = true, message = "OTP verified successfully." });
        }

        /// <summary>
        /// Finalize registration with PIN and Step 4 status
        /// </summary>
        [HttpPost("finalize-registration")]
        public async Task<IActionResult> Finalize(PinRequest req)
        {
            await _service.CompleteRegistration(req);
            return Ok(new { success = true, message = "Registration completed successfully. You can now login." });
        }
    }
}