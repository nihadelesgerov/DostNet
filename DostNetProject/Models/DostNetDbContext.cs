using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DostNetProject.Models
{
    public class DostNetDbContext : IdentityDbContext<DostNetUser>
    {
        public DostNetDbContext(DbContextOptions options): base(options)
        {
            
        }
        // Tables will create in db (Identity Tables  is Default)
        public DbSet<Post> PostsTable { get; set; }
        public DbSet<Article> ArticlesTable { get; set; }
        public DbSet<InterViewQuestion> InterViewQuestionsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // DostnetUser ile Postlar arasindaki elaqe
            modelBuilder.Entity<Post>().HasOne(a=>a.User).WithMany(a=>a.Posts).HasForeignKey(a=>a.UserId).OnDelete(DeleteBehavior.Cascade);

            // DostnetUser ile Article arasindaki elaqe
            modelBuilder.Entity<Article>().HasOne(a => a.User).WithMany(a => a.Articles).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);

            // DostnetUser ile InterViewQuestions arasindaki elaqe

            modelBuilder.Entity<InterViewQuestion>().HasOne(a => a.User).WithMany(a => a.InterViewQuestions).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
