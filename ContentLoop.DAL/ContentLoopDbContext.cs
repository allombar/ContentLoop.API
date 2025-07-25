using ContentLoop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL
{
    public class ContentLoopDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get { return Set<UserEntity>(); } }
        public DbSet<ArticleEntity> Articles { get { return Set<ArticleEntity>(); } }
        public DbSet<ArticleCommentsEntity> Comments { get { return Set<ArticleCommentsEntity>(); } }
        public ContentLoopDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
