using System.Net;
using System.Security.Claims;
using NetCoreBackend.Application.Helper;
using NetCoreBackend.Application.Models.Request;
using NetCoreBackend.Application.Models.Response;
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Application.Services.Service
{
    public class AuthenService : IAuthenService
    {
        private readonly IUserRepository _userRepository;
        public AuthenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var pass_encypt = AES.EncyptCbc128(request.Password);
            var query = await _userRepository.GetUserByLogin(request.Username, pass_encypt);
            if (query is null)
            {
                return new LoginResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Invalid username or password",
                    Data = new LoginData
                    {
                        ExpireDate = null,
                        token = string.Empty
                    }
                };
            }

            var secret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? "this-is-a-dev-secret-key-change-me";
            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
                ?? "NetCoreBackend";
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
                ?? "NetCoreBackendClient";

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, query.UserId.ToString()),
                new(ClaimTypes.Name, query.Username),
                new(ClaimTypes.Role, query.Role)
            };

            var tokenResult = JWTEncyption.Encyption(claims, secret, issuer, audience, 60);
            return new LoginResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Success",
                Data = new LoginData
                {
                    ExpireDate = tokenResult.ExpireDateUtc,
                    token = tokenResult.Token
                }
            };
        }
    }
}
