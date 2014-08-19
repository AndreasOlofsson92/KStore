using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string DeliveryPostcode { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryAddress { get; set; }
        
        [Display(Name="Payment method")]
        public string PaymentMethod { get; set; }

        public Customer Customer { get; set; }

        public OrderStatus OrderStatus { get; set; }


        public DateTime DateShipped { get; set; }

        public User User { get; set; }

        public List<OrderLine> OrderLines { get; set; }

        public DateTime Created { get; set; }

        public bool Completed { get; set; }


    }
}
