using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class MovieShopDbContext : DbContext 
    {
        //inject dbcontecxtopt
        //把startup中的options inject into 这里的constructor（xx<xxx> ）
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }
        //Dbset represent your table
        public DbSet<Genre> Genres { get; set; }//Genere class through the Dbset type
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get;set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<Role> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //通过icollection 先把movie和genre连起来，然后再m.genre
            //many to many
           modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                        .UsingEntity<Dictionary<string, object>>("MovieGenre",
                         m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                         g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

            
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Favorite>(ConfigureFavorites);
    }
        private void ConfigureFavorites(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);

            builder.HasOne(f=>f.User).WithMany(f=>f.Favorites).HasForeignKey(f=>f.UserId);
            builder.HasOne(f=>f.Movie).WithMany(f=>f.Favorites).HasForeignKey(f=>f.MovieId);
        }
        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p =>p.Id);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.HasOne(p=>p.User).WithMany(p=>p.Purchases).HasForeignKey(p=>p.UserId);
            builder.HasOne(p=>p.Movie).WithMany(p=>p.Purchases).HasForeignKey(p=>p.MovieId);
        }
        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            //builder.Ignore(r => r.Rating);
            builder.Property(r => r.Rating).HasColumnType("decimal(3,2)");
            //builder.HasOne(r =>r.User ).WithMany(r=>r.Reviews).HasForeignKey(r => r.UserId);
            //builder.HasOne(r=>r.Movie).WithMany(r=>r.Reviews).HasForeignKey(r => r.MovieId);

            builder.HasOne(r => r.Movie).WithMany(r => r.Reviews).HasForeignKey(r => r.MovieId);
            builder.HasOne(r => r.User).WithMany(r => r.Reviews).HasForeignKey(r => r.UserId);


        }
        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId,ur.RoleId });
            builder.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur=>ur.UserId);
            builder.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId);
        }
        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(256);
            builder.Property(u => u.LastName).HasMaxLength(4096);
            builder.Property(u =>u.Email).HasMaxLength(512);
            builder.Property(u =>u.HashedPassword ).HasMaxLength(2084);
            builder.Property(u =>u.Salt).HasMaxLength(2084);
            builder.Property(u =>u.PhoneNumber).HasMaxLength(2084);
            builder.Property(u => u.DateOfBirth).HasDefaultValueSql("getdate()"); ;
            builder.Property(u => u.LockoutEndDate).HasDefaultValueSql("getdate()"); ;
            builder.Property(u => u.LastLoginDateTime).HasDefaultValueSql("getdate()"); ;
            builder.Property(u => u.AccessFailedCount);
            builder.Property(u => u.IsLocked);
        }
        
        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mcr => new { mcr.CrewId, mcr.MovieId, mcr.Department,mcr.Job });
            builder.HasOne(mcr => mcr.Movie).WithMany(mcr => mcr.MovieCrews).HasForeignKey(mcr => mcr.MovieId);
            builder.HasOne(mcr => mcr.Crew).WithMany(mcr => mcr.MovieCrews).HasForeignKey(mcr => mcr.CrewId);
        }
        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.ProfilePath).HasMaxLength(2084);
            builder.Property(cr => cr.Name).HasMaxLength(128);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character });
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
            builder.Property(c => c.Name).HasMaxLength(128);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            
           builder.ToTable("Trailer");
           builder.HasKey(t => t.Id);
           builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
           builder.Property(t => t.Name).HasMaxLength(2084);

        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            //user fluent API rules
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Ignore(m => m.Rating);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");


        }
    }
}
