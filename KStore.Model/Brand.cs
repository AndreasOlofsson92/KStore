using KStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
