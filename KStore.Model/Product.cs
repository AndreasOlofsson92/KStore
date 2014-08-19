using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string DeliveryTime { get; set; }

        public int Views { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public bool Visible { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double PurchasePrice { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int StockStatus { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
                

        public DateTime? Modified { get; set; }


       [ForeignKey("Category")]
        public int? Category_Id { get; set; }


        public virtual ProductCategory Category { get; set; }

        public int? Brand_Id { get; set; }
        [ForeignKey("Brand_Id")]
        public  Brand Brand { get; set; }

        public string ImagePath { get; set; }

    }
}
