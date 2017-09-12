using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manytomanyloadingtest.Models
{
    public class MTMContext : DbContext
    {
        public MTMContext(DbContextOptions<MTMContext> options) 
            : base(options)
        { }

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserConversation>()
                .HasKey(t => new { t.UserConversationId, t.UserId, t.ConversationId });

            modelBuilder.Entity<UserConversation>()
             .HasOne(uc => uc.User)
             .WithMany(u => u.UserConversations)
             .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserConversation>()
             .HasOne(uc => uc.Conversation)
             .WithMany(c => c.UserConversations)
             .HasForeignKey(uc => uc.ConversationId);
        }
    }
}
