using Microsoft.EntityFrameworkCore;
using BulkyBook.Web.Razor.Models;

using System.Data;

namespace BulkyBook.Web.Razor.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)  : base(options)
        { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fiction", DisplayOrder = 1 },
                new Category { CategoryId =2, Name = "Autobiography", DisplayOrder = 2}
                
                );
               
        }
    }
}
