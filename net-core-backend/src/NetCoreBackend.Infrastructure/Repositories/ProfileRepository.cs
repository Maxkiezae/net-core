using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;
using System.Globalization;

namespace NetCoreBackend.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private static readonly string AccountFilePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "accounts.txt");

        public ProfileRepository()
        {

        }

        public Task<ProfileEntity?> GetProfileByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || !File.Exists(AccountFilePath))
            {
                return Task.FromResult<ProfileEntity?>(null);
            }

            var target = username.Trim();
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

                var fileUsername = parts[2].Trim();
                if (!string.Equals(fileUsername, target, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                DateTime.TryParse(parts[0], null, DateTimeStyles.RoundtripKind, out var createdAtUtc);

                return Task.FromResult<ProfileEntity?>(new ProfileEntity
                {
                    Username = fileUsername,
                    Name = fileUsername,
                    Email = $"{fileUsername}@local.dev",
                    Tel = createdAtUtc == default ? "-" : createdAtUtc.ToString("yyyy-MM-dd HH:mm:ss 'UTC'")
                });
            }

            return Task.FromResult<ProfileEntity?>(null);
        }
    }
}
