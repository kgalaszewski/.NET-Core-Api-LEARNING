using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models.ViewModel
{
    public class ProductsViewModel // needed when your view is a combination of more than 1 model
    {
        public Products Products { get; set; }

        public IEnumerable<ProductTypes> ProductTypes { get; set; }

        public IEnumerable<SpecialTag> SpecialTags { get; set; }
    }
}
