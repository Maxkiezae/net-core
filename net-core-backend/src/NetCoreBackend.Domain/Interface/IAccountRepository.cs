using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Domain.Interface
{
    public interface IAccountRepository
    {
        Task<AccountEntity> AddNewAccount(AccountEntity profile);
    }
}