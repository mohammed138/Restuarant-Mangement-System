using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Res.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Res.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         
        public DbSet<Product> Products { get; set; }
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Orders> Orders { get; set; }
         public DbSet<Booking> Booking { get; set; } 
        public DbSet<BookingType> BookingType { get; set; }
        public DbSet<NumberOfPeople> NumberOfPeople { get; set; }
        public DbSet<SpecialEvent> SpecialEvent { get; set; } 
        public DbSet<ApplicationUser> ApplicationUser { get; set; } 
         public DbSet<OrdersStatus> OrdersStatus { get; set; } 
        public DbSet<BookingStatus> BookingStatus { get; set; }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();


        }



    }
}
