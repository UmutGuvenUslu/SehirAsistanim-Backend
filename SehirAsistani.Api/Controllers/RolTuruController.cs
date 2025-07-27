using Microsoft.AspNetCore.Mvc;
using SehirAsistanim.Domain.Entities;
using SehirAsistanim.Domain.Enums;
using SehirAsistanim.Domain.Interfaces;

[Route("[controller]/[action]")]
[ApiController]
public class RolTuruController : ControllerBase
{
    private readonly IRolService _rolService;

    public RolTuruController(IRolService rolService)
    {
        _rolService = rolService;
    }

    [HttpGet]
    public IActionResult GetRolTurleri()
    {
        var values = Enum.GetValues(typeof(rolturu))
                         .Cast<rolturu>()
                         .Select(e => new
                         {
                             Name = e.ToString(),
                             id = (int)e
                         });

        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> AddRol([FromBody] RolTuruRequest request)
    {
        var result = await _rolService.AddRol(request);
        return result;
    }
}
