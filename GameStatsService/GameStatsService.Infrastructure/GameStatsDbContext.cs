using GameStatsService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStatsService.Infrastructure
{
    public class GameStatsDbContext : DbContext
    {
        public DbSet<Scoreboard> Scoreboards { get; set; }
        public DbSet<GameResult> GameResults { get; set; }

        public GameStatsDbContext(DbContextOptions<GameStatsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scoreboard>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Id).ValueGeneratedOnAdd();
                entity.Property(n => n.Wins).IsRequired();
                entity.Property(n => n.Losses).IsRequired();
                entity.Property(n => n.Ties).IsRequired();
                entity.Property(n => n.UserId).IsRequired();
            });

            modelBuilder.Entity<GameResult>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Id).ValueGeneratedOnAdd();
                entity.Property(n => n.UserId).IsRequired();
                entity.Property(n => n.CreatedAt).IsRequired();
                entity.Property(n => n.UserChoice).IsRequired();
                entity.Property(n => n.ComputerChoice).IsRequired();
                entity.Property(n => n.Result).IsRequired();
            });
        }
    }
}
