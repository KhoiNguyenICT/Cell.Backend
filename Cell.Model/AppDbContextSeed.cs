using Cell.Common.Extensions;
using Cell.Model.Entities.SecurityUserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Cell.Model
{
    public class AppDbContextSeed : IWebHostInitializer
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly string _defaultUserFile = Path.Combine("Setup", "SecurityUser/setting-user-data.json");

        public AppDbContextSeed(
            AppDbContext context,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("CellContextSeed");
        }

        public async Task InitAsync()
        {
            await InitAccount();
        }

        private async Task InitAccount()
        {
            if (await _context.Set<SecurityUser>().AsNoTracking().AnyAsync())
                return;
            var content = File.ReadAllText(_defaultUserFile);
            var account = JsonConvert.DeserializeObject<SecurityUser>(content);
            _context.SecurityUsers.Add(account);
            _context.SaveChanges();
        }
    }
}