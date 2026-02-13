using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly string AccountFilePath = ResolveAccountFilePath();

        // db contxt this way
        public UserRepository()
        {
            // set up contxt
        }
        public Task<UserEntity?> GetUserByLogin(string Username, string Password)
        {
            // read file accounts.txt as Database
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                !File.Exists(AccountFilePath))
            {
                return Task.FromResult<UserEntity?>(null);
            }

            var targetUsername = Username.Trim();
            var lines = File.ReadAllLines(AccountFilePath);

            foreach (var line in lines.Reverse())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var parts = line.Split('|');
                if (parts.Length < 4)
                {
                    continue;
                }

                var accountIdText = parts[1].Trim();
                var fileUsername = parts[2].Trim();
                var fileEncryptedPassword = parts[3].Trim();

                if (!string.Equals(fileUsername, targetUsername, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!string.Equals(fileEncryptedPassword, Password, StringComparison.Ordinal))
                {
                    continue;
                }

                var userId = Guid.TryParse(accountIdText, out var parsedId) ? parsedId : Guid.NewGuid();
                return Task.FromResult<UserEntity?>(new UserEntity
                {
                    UserId = userId,
                    Username = fileUsername,
                    Role = "User"
                });
            }

            return Task.FromResult<UserEntity?>(null);
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

            return Path.Combine(Directory.GetCurrentDirectory(), "accounts.txt");
        }
    }
}
