using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;
using NetCoreBackend.Application.Helper;

namespace NetCoreBackend.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private static readonly List<AccountEntity> Accounts = new();
        private static readonly object FileLock = new();
        private static readonly string AccountFilePath = ResolveAccountFilePath();

        // db contxt this way
        public AccountRepository()
        {
            // set up contxt
        }

        public Task<AccountEntity> AddNewAccount(AccountEntity new_account)
        {
            if (string.IsNullOrWhiteSpace(new_account.Username))
            {
                return Task.FromResult<AccountEntity>(new AccountEntity());
            }

            if (string.IsNullOrWhiteSpace(new_account.Password))
            {
                return Task.FromResult<AccountEntity>(new AccountEntity());
            }

            var username = new_account.Username.Trim();
            var isDuplicate = Accounts.Any(x =>
                string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase));

            if (isDuplicate)
            {
                return Task.FromResult<AccountEntity>(new AccountEntity());
            }

            var encryptedPassword = AES.EncyptCbc128(new_account.Password);
            var account = new AccountEntity
            {
                AccountId = new_account.AccountId == Guid.Empty ? Guid.NewGuid() : new_account.AccountId,
                Username = username,
                Password = encryptedPassword
            };

            Accounts.Add(account);
            WriteAccountToTextFile(account);
            return Task.FromResult(account);
        }

        private static void WriteAccountToTextFile(AccountEntity account)
        {
            var line = $"{DateTime.UtcNow:O}|{account.AccountId}|{account.Username}|{account.Password}";
            lock (FileLock)
            {
                var directory = Path.GetDirectoryName(AccountFilePath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.AppendAllText(AccountFilePath, line + Environment.NewLine);
            }
        }

        private static string ResolveAccountFilePath()
        {
            var candidates = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "accounts.txt"),
                Path.Combine(AppContext.BaseDirectory, "accounts.txt"),
                Path.Combine(Directory.GetCurrentDirectory(), "src", "NetCoreBackend.Api", "accounts.txt"),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "accounts.txt")),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "src", "NetCoreBackend.Api", "accounts.txt"))
            };

            foreach (var path in candidates.Distinct())
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            var apiProjectDir = Path.Combine(Directory.GetCurrentDirectory(), "src", "NetCoreBackend.Api");
            if (Directory.Exists(apiProjectDir))
            {
                return Path.Combine(apiProjectDir, "accounts.txt");
            }

            return Path.Combine(Directory.GetCurrentDirectory(), "accounts.txt");
        }

    }
}
