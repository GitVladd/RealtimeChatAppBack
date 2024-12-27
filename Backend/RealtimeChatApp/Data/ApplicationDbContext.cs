using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<SentimentAnalysis> SentimentAnalysis { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>()
                .HasKey(cm => cm.Id);

            modelBuilder.Entity<SentimentAnalysis>()
                .HasKey(sa => sa.Id);

            modelBuilder.Entity<SentimentAnalysis>()
                .HasOne(sa => sa.ChatMessage)
                .WithOne(cm => cm.SentimentAnalysis)
                .HasForeignKey<SentimentAnalysis>(sa => sa.ChatMessageId);
        }
    }
}
