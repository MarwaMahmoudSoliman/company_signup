namespace AiCompany.Data
{
    using Microsoft.EntityFrameworkCore;
  
    using global::AiCompany.Data.Models;

    namespace AiCompany.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Company> Companies { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Company>(entity =>
                {
                    entity.HasKey(e => e.Id);

                    entity.Property(e => e.ArabicName)
                          .IsRequired()
                          .HasMaxLength(100);

                    entity.Property(e => e.EnglishName)
                          .IsRequired()
                          .HasMaxLength(100);

                    entity.Property(e => e.Email)
                          .IsRequired()
                          .HasMaxLength(100);

                    entity.HasIndex(e => e.Email)
                          .IsUnique();
                });
            }
        }
    }

}
