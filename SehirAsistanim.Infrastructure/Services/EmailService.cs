using Microsoft.Extensions.Caching.Memory;
using SehirAsistanim.Domain.Interfaces;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SehirAsistanim.Infrastructure.Services
{
    public class EmailService
    {
        private readonly ISmtpService _smtpService;
        private readonly IMemoryCache _memoryCache;

        public EmailService(ISmtpService smtpService, IMemoryCache memoryCache)
        {
            _smtpService = smtpService;
            _memoryCache = memoryCache;
        }


        public static string GenerateCode()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        int randomValue = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF; // pozitif sayı yap
        return (randomValue % 900000 + 100000).ToString(); // 100000-999999 arası sayı
    }


    public async Task SendEmailVerificationCode(string email)
        {
            // 6 haneli rastgele doğrulama kodu üret
            var code = GenerateCode();

            // Cache'e 1 dakikalığına e-posta-kod eşlemesi kaydet
            _memoryCache.Set($"EmailVerification:{email}", code, TimeSpan.FromMinutes(1));

            var mesaj = $"<b>Email doğrulama kodunuz: {code}</b><br/>Bu kod 1 dakika boyunca geçerlidir.";
            await _smtpService.Gonder(email, "Email Doğrulama", mesaj);
        }

        public bool VerifyEmailCode(string email, string userCode)
        {
            // Cache'den kodu al
            if (_memoryCache.TryGetValue($"EmailVerification:{email}", out string? storedCode))
            {
                // Girilen kod ile eşleşiyorsa doğrulandı
                return storedCode == userCode;
            }

            // Kod bulunamadı veya süresi geçti
            return false;
        }
    }
}
