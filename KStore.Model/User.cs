using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Domain.Model
{

        public class User : IdentityUser
        {
            
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public DateTime Created { get; set; }

          

        }
    
}
