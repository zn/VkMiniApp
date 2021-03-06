﻿using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("PostRepositoryTests")]

namespace Infrastructure.Data
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
