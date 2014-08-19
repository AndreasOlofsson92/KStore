using KStore.Data;
using KStore.Model;
using KStore.Website.Services;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace KStore.Website.Controllers
{
    public class PayPalController : Controller
    {

        public PayPalService PayPalService { get; set; }

        public PayPalController()
        {
            PayPalService = new PayPalService();
        }
        public void Index()
        {

        }
        // GET: PayPal
        public ActionResult SetExpressCheckout(List<CartItem> cartItems)
        {
        
            double total=0;
            List<PaymentDetailsItemType> paymentItems = PayPalService.CreatePaymentDetails(cartItems, ref total);
            PaymentDetailsType paymentDetail = new PaymentDetailsType();
            
            paymentDetail.PaymentAction = (PaymentActionCodeType)EnumUtils.GetValue("Sale", typeof(PaymentActionCodeType));
            paymentDetail.OrderTotal = new BasicAmountType((CurrencyCodeType)EnumUtils.GetValue("SEK", typeof(CurrencyCodeType)), total.ToString());
            List<PaymentDetailsType> paymentDetails = new List<PaymentDetailsType>();
            paymentDetails.Add(paymentDetail);
            paymentDetail.PaymentDetailsItem = paymentItems;
            SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = "http://localhost:14591/paypal/getExpressCheckoutDetails?success=true";
            ecDetails.CancelURL = "http://localhost:14591/#?cancel=true";
            ecDetails.PaymentDetails = paymentDetails;
            ecDetails.LocaleCode = "sv_SE";
            ecDetails.SolutionType = (SolutionTypeType)EnumUtils.GetValue("Sole", typeof(SolutionTypeType));
            SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();
            request.Version = "104.0";
            request.SetExpressCheckoutRequestDetails = ecDetails;

            SetExpressCheckoutReq wrapper = new SetExpressCheckoutReq();
            wrapper.SetExpressCheckoutRequest = request;

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(PayPalService.GetSdkConfig());
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + setECResponse.Token;

            return Json(new { result = "Redirect", url = url });
        }


        public ActionResult GetExpressCheckoutDetails(string token, string payerID)
        {
            GetExpressCheckoutDetailsRequestType request = new GetExpressCheckoutDetailsRequestType();
            request.Version = "104.0";
            request.Token = token;
            GetExpressCheckoutDetailsReq wrapper = new GetExpressCheckoutDetailsReq();
            wrapper.GetExpressCheckoutDetailsRequest = request;
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(PayPalService.GetSdkConfig());
            GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);
            PayPalService.CreateOrderDetails(ecResponse);
            DoExpressCheckoutPayment(token, payerID, ecResponse);
           return Redirect("http://localhost:14591/#?success=true");
        }

        public void DoExpressCheckoutPayment(string token, string payerID, GetExpressCheckoutDetailsResponseType ecResponse)
        {
            PaymentDetailsType paymentDetail = new PaymentDetailsType();
            paymentDetail.NotifyURL = "http://replaceIpnUrl.com";
            paymentDetail.PaymentAction = (PaymentActionCodeType)EnumUtils.GetValue("Sale", typeof(PaymentActionCodeType));
            paymentDetail.OrderTotal = new BasicAmountType((CurrencyCodeType)EnumUtils.GetValue("SEK", typeof(CurrencyCodeType)), ecResponse.GetExpressCheckoutDetailsResponseDetails.PaymentDetails.First().ItemTotal.value.ToString());
            List<PaymentDetailsType> paymentDetails = new List<PaymentDetailsType>();
            paymentDetails.Add(paymentDetail);

            DoExpressCheckoutPaymentRequestType request = new DoExpressCheckoutPaymentRequestType();
            request.Version = "104.0";
            DoExpressCheckoutPaymentRequestDetailsType requestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
            requestDetails.PaymentDetails = paymentDetails;
            requestDetails.Token = token;
            requestDetails.PayerID = payerID;
            request.DoExpressCheckoutPaymentRequestDetails = requestDetails;

            DoExpressCheckoutPaymentReq wrapper = new DoExpressCheckoutPaymentReq();
            wrapper.DoExpressCheckoutPaymentRequest = request;
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(PayPalService.GetSdkConfig());
            DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(wrapper);
           
        }
    }
}