using Microsoft.Extensions.Caching.Memory;
using SehirAsistanim.Domain.Interfaces;
using System.Security.Cryptography;

namespace SehirAsistanim.Infrastructure.Services
{
    public class EmailService
    {
        private readonly ISmtpService _smtpService;
        private readonly IMemoryCache _memoryCache;

        #region Constructor
        public EmailService(ISmtpService smtpService, IMemoryCache memoryCache)
        {
            _smtpService = smtpService;
            _memoryCache = memoryCache;
        }
        #endregion

        #region Kod Üret
        public static string GenerateCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);
            int randomValue = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
            return (randomValue % 900000 + 100000).ToString();
        }
        #endregion

        #region Kod Gönder
        public async Task SendVerificationCode(string email)
        {
            var code = GenerateCode();
            _memoryCache.Set($"EmailVerification:{email}", code, TimeSpan.FromMinutes(1));

            var mesaj = $"<b>Email doğrulama kodunuz: {code}</b><br/>Bu kod 1 dakika boyunca geçerlidir.";
            await _smtpService.Gonder(email, "Email Doğrulama", mesaj);
        }
        #endregion

        #region Kod Doğrula
        public bool IsVerified(string email, string userCode)
        {
            if (_memoryCache.TryGetValue($"EmailVerification:{email}", out string? storedCode))
            {
                return storedCode == userCode;
            }

            return false;
        }
        #endregion
    }
}
