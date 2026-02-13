using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreBackend.Application.Models.Request;
using NetCoreBackend.Application.Models.Response;
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Application.Services.Service;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IAccountService _accountService;
        public UserController(IProfileService profileService, IAccountService accountService)
        {
            _profileService = profileService;
            _accountService = accountService;
        }

        [HttpPost("profile")]
        [Authorize]
        public async Task<ActionResult<ProfileResponse>> Login([FromBody] ProfileRequest request, CancellationToken cancellationToken)
        {
            var response = await _profileService.GetProfileByUsername(request.username);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateAccountResponse>> Register([FromBody] CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var response = await _accountService.CreateNewAccount(request.username, request.password);
            return Ok(response);
        }
    }
}