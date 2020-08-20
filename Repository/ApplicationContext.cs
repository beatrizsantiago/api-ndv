using System;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Life> Lifes { get; set; }
        public DbSet<ProgressStepsLife> ProgressStepsLifes { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Visitant> Visitants { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp(0)");
            }
            base.OnModelCreating(builder);
        }
    }
}