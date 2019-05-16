using Microsoft.EntityFrameworkCore;
using QuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
