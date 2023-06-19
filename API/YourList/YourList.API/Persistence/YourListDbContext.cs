using Microsoft.EntityFrameworkCore;
using YourList.API.Models;

namespace YourList.API.Persistence
{
    public class YourListDbContext : DbContext
    {
        public DbSet<DailyTasks> DailyTasks { get; set; }
        public DbSet<Passos> Passos { get; set; }

        public YourListDbContext(DbContextOptions<YourListDbContext> Options) : base(Options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<DailyTasks>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Titulo).IsRequired(true);
                e.Property(x => x.Descricao)
                    .HasMaxLength(500)
                    .HasColumnType("varchar(500)");
                e.HasMany(x => x.Passos)
                    .WithOne()
                    .HasForeignKey(x => x.DailyTaskId);
            });

            modelBuilder.Entity<Passos>(e =>
            {
                e.HasKey(x => x.Id);
            });
        }
    }
}
