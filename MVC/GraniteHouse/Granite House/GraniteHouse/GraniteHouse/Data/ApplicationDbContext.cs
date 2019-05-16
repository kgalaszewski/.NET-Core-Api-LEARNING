using System;
using System.Collections.Generic;
using System.Text;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<SpecialTag> SpecialTags { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<ProductsSelectedForAppointment> ProductsSelectedForAppointment  { get; set; }
    }
}
