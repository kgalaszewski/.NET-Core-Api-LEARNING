using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspressoAPI.Models
{
    public class Submenu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }

        public ICollection<Submenu> SubMenus { get; set; }
    }
}
