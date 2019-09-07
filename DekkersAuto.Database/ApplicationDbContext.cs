using System;
using System.Collections.Generic;
using System.Text;
using DekkersAuto.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DekkersAuto.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ListingOption> ListingOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
    }
}
