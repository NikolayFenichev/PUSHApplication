using Microsoft.EntityFrameworkCore;
using Registration.DAL.Models;

namespace Registration.DAL
{
    public class RegistrationContext : DbContext
    {
        public DbSet<MobileApp> MobileApps { get; set; }

        public RegistrationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
