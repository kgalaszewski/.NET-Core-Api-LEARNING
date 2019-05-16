using EspressoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspressoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        // public DbSet<Submenu> Submenus { get; set; } jest potrzebne jesli chcemy oddzielnie przechowywac SubMenus ale w naszym przypadku trzymamy w liscie i nie ma luznych submenu
    }
}
