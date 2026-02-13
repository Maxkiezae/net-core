using System.Net;
using NetCoreBackend.Application.Models.Response;
using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Application.Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CreateAccountResponse> CreateNewAccount(string username, string password)
        {
            var account = new AccountEntity()
            {
                AccountId = Guid.NewGuid(),
                Username = username,
                Password = password
            };
            var data = await _accountRepository.AddNewAccount(account);
            if (data.AccountId != Guid.Empty)
            {
                return new CreateAccountResponse()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Account created successfully",
                    Data = new CreateAccountResponseData()
                    {
                        username = data.Username,
                    }
                };
            }
            else
            {
                return new CreateAccountResponse()
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity,
                    Message = "Failed to create account",
                    Data = new CreateAccountResponseData()
                };
            }
        }
    }
}