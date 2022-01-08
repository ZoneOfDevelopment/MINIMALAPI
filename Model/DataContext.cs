using Microsoft.EntityFrameworkCore;

namespace MinimalAPI.Model
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

        public DbSet<Dog> Dogs => Set<Dog>();
    }
}
