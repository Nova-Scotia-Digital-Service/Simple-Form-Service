using Microsoft.EntityFrameworkCore;
using SimpleFormsService.API.Entities;

namespace SimpleFormsService.API.Data
{
    public class PostgreSqlContext : DbContext
    {
        public DbSet<Form_Template> Form_Templates { get; set; }
        public DbSet<Form_Submission> Form_Submissions { get; set; }
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
