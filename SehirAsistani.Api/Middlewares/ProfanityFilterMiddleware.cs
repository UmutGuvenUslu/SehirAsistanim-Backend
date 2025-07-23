using System.Text;
using System.Text.RegularExpressions;

public class ProfanityFilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<string> _bannedWords;

    public ProfanityFilterMiddleware(RequestDelegate next)
    {
        _next = next;

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "kotusoz.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Küfür listesi bulunamadı: kotusoz.txt");
            _bannedWords = new List<string>();
        }
        else
        {
            _bannedWords = File.ReadAllLines(filePath)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Trim().ToLower())
                .ToList();
        }
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (ContainsProfanity(body))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Uygunsuz kelime tespit edildi.");
                return;
            }
        }

        await _next(context);
    }

    private bool ContainsProfanity(string text)
    {
        foreach (var word in _bannedWords)
        {
            var pattern = $@"\b{Regex.Escape(word)}\b";
            if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
                return true;
        }

        return false;
    }
}
