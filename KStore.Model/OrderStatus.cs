using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KStore.Domain.Model
{
   public class OrderStatus
    {

        public int Id { get; set; }

          [Display(Name = "Order status")]
        public string Name { get; set; }
    }
}
