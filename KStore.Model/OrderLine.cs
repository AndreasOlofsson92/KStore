using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
   public class OrderLine
    {

        public int Order_Id { get; set; }

        public int Product_Id { get; set; }

        [ForeignKey("Order_Id")]
        public Order Order { get; set; }

        [ForeignKey("Product_Id")]
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
