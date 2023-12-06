using datacheck.model;
using Microsoft.EntityFrameworkCore;

namespace datacheck.Database
{
    public class DataContext:DbContext
    {
        public DbSet<item> Items{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=programs;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
