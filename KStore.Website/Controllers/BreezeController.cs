using KStore.Data;
using KStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.WebApi2;

namespace KStore.Website.Controllers
{
    [BreezeController]
    [AllowAnonymous]
    public class BreezeController : ApiController
    {
        Breeze.ContextProvider.EF6.EFContextProvider<EcommerceDbContext> contextProvider = new Breeze.ContextProvider.EF6.EFContextProvider<EcommerceDbContext>();

        [HttpGet]
        public string MetaData()
        {
            return contextProvider.Metadata();
        }
        [HttpGet]
        public IQueryable<Product> Products()
        {
            return contextProvider.Context.Products;
        }

        [HttpGet]
        public IQueryable<ProductCategory> ProductCategories()
        {
            return contextProvider.Context.ProductCategories;
        }
    }
}
