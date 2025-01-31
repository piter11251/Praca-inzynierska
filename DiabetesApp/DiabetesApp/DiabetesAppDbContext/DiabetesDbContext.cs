using DiabetesApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.DiabetesAppDbContext
{
    public class DiabetesDbContext: IdentityDbContext<User>
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<BloodPressure> Pressures { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        //public DbSet<User> Users { get; set; }

        public DiabetesDbContext(DbContextOptions<DiabetesDbContext> options): base(options)
        {

        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-396HNS8;Initial Catalog=DiabetesAppDb;Integrated Security=True;Trust Server Certificate=True;");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserPreferences)
                .WithOne(up => up.User)
                .HasForeignKey<UserPreferences>(up => up.UserId);

            /*modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u => u.Entries)
                .HasForeignKey(e => e.UserId);*/
            modelBuilder.Entity<Entry>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BloodPressure>()
                .HasOne(bp => bp.User)
                .WithMany(u => u.Pressures)
                .HasForeignKey(bp => bp.UserId);

            modelBuilder.Entity<UserPreferences>()
                .HasMany(p => p.PreferableSugarLevels)
                .WithOne(s => s.UserPrefences)
                .HasForeignKey(s => s.UserPreferencesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entry>()
                .Property(e => e.MealTime)
                .HasColumnType("datetime");

            /*modelBuilder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u => u.Entries)
                .HasForeignKey(p => p.UserId);*/

            /*modelBuilder.Entity<BloodPressure>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pressures)
                .HasForeignKey(p => p.UserId);*/

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
