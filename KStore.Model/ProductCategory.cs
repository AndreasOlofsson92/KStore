using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
    public class ProductCategory
    {

        public ProductCategory()
        {
            Products = new List<Product>();
            SubCategories = new List<ProductCategory>();                       
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public virtual IList<Product> Products { get; set; }

        public IList<ProductCategory> SubCategories { get; set; }

    }
}
