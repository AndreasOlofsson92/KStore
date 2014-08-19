using KStore.Data;
using KStore.Domain.Model;
using KStore.Model;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KStore.Website.Services
{
    public class PayPalService
    {
        public EcommerceDbContext Db { get; set; }

        public PayPalService()
        {
            Db = new EcommerceDbContext();
        }

        public Dictionary<string, string> GetSdkConfig()
        {
            Dictionary<string, string> sdkConfig = new Dictionary<string, string>();
            sdkConfig.Add("mode", "sandbox");
            sdkConfig.Add("account1.apiUsername", "andreas.olofsson92-facilitator_api1.gmail.com");
            sdkConfig.Add("account1.apiPassword", "1394883853");
            sdkConfig.Add("account1.apiSignature", "AFcWxV21C7fd0v3bYYYRCpSSRl31Aoa5OHQDK66vKdS0.4M6mxYaUfVQ");

            return sdkConfig;

        }


        public List<PaymentDetailsItemType> CreatePaymentDetails(List<CartItem> cartItems, ref double total)
        {
            List<PaymentDetailsItemType> paymentItems = new List<PaymentDetailsItemType>();


            CurrencyCodeType currency = (CurrencyCodeType)EnumUtils.GetValue("SEK", typeof(CurrencyCodeType));
            foreach (var item in cartItems)
            {
                PaymentDetailsItemType paymentItem = new PaymentDetailsItemType();
                var product = Db.Products.Find(item.ProductId);
                paymentItem.Name = item.Name;
                paymentItem.Number = item.ProductId.ToString();
                double itemAmount = product.Price;
                paymentItem.Amount = new BasicAmountType(currency, itemAmount.ToString());
                int itemQuantity = item.Quantity;
                total += itemAmount * itemQuantity;
                paymentItem.Quantity = itemQuantity;
                paymentItem.ItemCategory = (ItemCategoryType)EnumUtils.GetValue("Physical", typeof(ItemCategoryType));
                paymentItems.Add(paymentItem);
            }


            return paymentItems;
        }

        public void CreateOrderDetails(GetExpressCheckoutDetailsResponseType ecResponse)
        {
            Order order=new Order();
            order.Created=DateTime.Now;
    
            var payerInfo=ecResponse.GetExpressCheckoutDetailsResponseDetails.PayerInfo;
          

            var customer= new Customer{
            Address = payerInfo.Address.Street1,
            Location=payerInfo.Address.CityName,
            Postcode= payerInfo.Address.PostalCode,
            FirstName=payerInfo.PayerName.FirstName,
            LastName=payerInfo.PayerName.LastName,
            PhoneNumber=payerInfo.ContactPhone,
            Email=payerInfo.Payer
            };

            order.Customer = customer;
            order.DeliveryAddress=payerInfo.Address.Street1;
             order.DeliveryLocation=payerInfo.Address.CityName;
             order.DeliveryPostcode = payerInfo.Address.PostalCode;
             order.PaymentMethod = "Paypal";
             
             Db.Orders.Add(order);
             Db.SaveChanges();
            
           
            List<OrderLine> orderLines = new List<OrderLine>();
            var paymentDetails = ecResponse.GetExpressCheckoutDetailsResponseDetails.PaymentDetails[0];
            foreach (var item in paymentDetails.PaymentDetailsItem)
            {
                
                OrderLine orderLine = new OrderLine
                {
                    Order_Id=order.Id,
                    Product_Id=Convert.ToInt32(item.Number) ,
                    Quantity = Convert.ToInt32(item.Quantity)
                };

                orderLines.Add(orderLine);
            }
            order.OrderLines = orderLines;
            Db.SaveChanges();

        }


    }
}