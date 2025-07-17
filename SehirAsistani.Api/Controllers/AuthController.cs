using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Infrastructure.Services;

namespace SehirAsistanim.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly EmailService _emailService;

        public AuthController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendEmailVerification(string email)
        {
            await _emailService.SendEmailVerificationCode(email);
            return Ok(new { message = "Doğrulama kodu gönderildi." });
        }

        [HttpPost("verify-code")]
        public IActionResult VerifyCode(string email, string code)
        {
            bool isValid = _emailService.VerifyEmailCode(email, code);

            if (isValid) {
                return Ok(new { message = "Email doğrulandı." });
            }
            return BadRequest(new { message = "Kod geçersiz veya süresi dolmuş." });
        }
    }
}
