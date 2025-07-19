using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Dto_s;
using SehirAsistanim.Domain.Interfaces;
using SehirAsistanim.Infrastructure.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly EmailService _emailService;
    private readonly IAuthService _authService;

    public AuthController(EmailService emailService, IAuthService authService)
    {
        _emailService = emailService;
        _authService = authService;
    }

    [HttpPost("send-verification-code")]
    public async Task<IActionResult> SendVerificationCode([FromQuery] string email)
    {
        await _emailService.SendVerificationCode(email);
        return Ok(new { message = "Doğrulama kodu gönderildi." });
    }

    [HttpPost("verify-and-register")]
    public async Task<IActionResult> VerifyAndRegister([FromBody] RegisterDto dto)
    {
        try
        {
            if (!_emailService.IsVerified(dto.Email, dto.Kod))
            {
                return BadRequest(new { message = "Doğrulama kodu geçersiz veya süresi dolmuş." });
            }

            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Loglama yapabilirsin
            return StatusCode(500, new { message = "Sunucu hatası: " + ex.Message });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    #region KullaniciEmailVarMi
    [HttpGet("IsEmailRegistered")]
    public async Task<IActionResult> IsEmailRegistered([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email boş olamaz.");

        var result = await _authService.IsEmailRegistered(email);
        return Ok(new { emailVar = result });
    }
    #endregion

    #region KullaniciTCVarMi
    [HttpGet("IsTCRegistered")]
    public async Task<IActionResult> IsTCRegistered([FromQuery] string tc)
    {
        if (string.IsNullOrWhiteSpace(tc))
            return BadRequest("TC boş olamaz.");

        var result = await _authService.IsPhoneRegistered(tc);
        return Ok(new { tcVar = result });
    }
    #endregion



}
