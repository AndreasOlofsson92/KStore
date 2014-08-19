﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public string Postcode { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<OrderLine> OrderLines { get; set; }

       

        
    }
}
