using DiabetesApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.DiabetesAppDbContext
{
    public class DiabetesDbContext: DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Pressure> Pressures { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-396HNS8;Initial Catalog=DiabetesAppDb;Integrated Security=True;Trust Server Certificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .Property(e => e.MealTime)
                .HasColumnType("datetime");

            modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u => u.Entries)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Pressure>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pressures)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserPreferences)
                .WithOne(p => p.User)
                .HasForeignKey<UserPreferences>(p => p.UserId);

            modelBuilder.Entity<UserPreferences>()
                .HasMany(p => p.PreferableSugarLevels)
                .WithOne(s => s.UserPrefences)
                .HasForeignKey(s => s.UserPreferencesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
