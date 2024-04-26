#region

using System.Reflection;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDDomain;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Chipsoft.Assignments.EPDCInfrastructure.Contexts
{
    public class EPDDbContext : DbContext, IEPDDbContext
    {
        public EPDDbContext(DbContextOptions<EPDDbContext> options)
            : base(options)
        {
        }

        public EPDDbContext()
        {
            
        }

        // The following configures EF to create a Sqlite database file in the
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=epd.db");
        //public DbSet<Patient> Patients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public IQueryable<T> GetQueryable<T>()
            where T : class, IBaseEntity
            => Set<T>();

        public IQueryable<T> GetNoTrackingQueryable<T>()
            where T : class, IBaseEntity
            => GetQueryable<T>().AsNoTracking();

    }
}
