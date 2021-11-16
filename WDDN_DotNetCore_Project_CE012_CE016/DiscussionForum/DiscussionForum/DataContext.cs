using DiscussionForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionForum
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Questions> QuestionDetails { get; set; }
        public DbSet<QuestionUser> QuestionUserDetails { get; set; }
        public DbSet<Comment> CommentDetails { get; set; }
        public DbSet<CommentQuestion> CommentQuestionDetails { get; set; }
    }
}
