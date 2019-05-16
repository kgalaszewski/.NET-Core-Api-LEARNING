using EspressoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspressoAPI.Data
{
    public static class FakeStaticSQLDb
    {
        public static List<Menu> Menus { get; set; }
        public static List<Menu> Reservations
        {
            get; set;
            // public DbSet<Submenu> Submenus { get; set; } jest potrzebne jesli chcemy oddzielnie przechowywac SubMenus ale w naszym przypadku trzymamy w liscie i nie ma luznych submenu
        }
    }
}
