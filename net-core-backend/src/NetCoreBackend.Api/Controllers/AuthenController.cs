using Microsoft.AspNetCore.Mvc;
using NetCoreBackend.Application.Models.Request;
using NetCoreBackend.Application.Models.Response;
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenController : ControllerBase
{
    private readonly IAuthenService _authenService;

    public AuthenController(IAuthenService authenService)
    {
        _authenService = authenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authenService.Login(request, cancellationToken);
        return Ok(response);
    }
}
