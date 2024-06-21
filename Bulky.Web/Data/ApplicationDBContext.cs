﻿using Bulky.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Web.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
             new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
             new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
             new Category { CategoryId =3, Name = "History", DisplayOrder=3}
                );
        }
    }
}
