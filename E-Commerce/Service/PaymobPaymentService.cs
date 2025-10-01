using E_Commerce.Models;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Orders;
using X.Paymob.CashIn.Models.Payment;
namespace E_Commerce.Service
{
    public class PaymobPaymentService : IPaymobPaymentService
    {
        private readonly IPaymobCashInBroker _broker;
        private readonly IConfiguration _configuration;

        public PaymobPaymentService(IPaymobCashInBroker broker, IConfiguration configuration)
        {
            _broker = broker;
            _configuration = configuration;
        }

        public async Task<string> CreatePaymentIframeAsync(OrderHeader order, CartWithOrderVM cartWithOrderVM, ApplicationUser user)
        {
            var amountCents = (int)(order.OrderTotal * 100);

            var orderRequest = CashInCreateOrderRequest.CreateOrder(
                amountCents ,
                merchantOrderId: order.Id.ToString());
            var orderResponse = await _broker.CreateOrderAsync(orderRequest);

            var billingData = new CashInBillingData(
                firstName: cartWithOrderVM.ApplicationUser.UserName,
                lastName: cartWithOrderVM.ApplicationUser.UserName,
                phoneNumber: cartWithOrderVM.ApplicationUser.PhoneNumber,
                email: user.Email
            );

            var paymentKeyRequest = new CashInPaymentKeyRequest(
                integrationId: int.Parse(_configuration["Paymob:IntegrationId"]),
                orderId: orderResponse.Id,
                billingData: billingData,
                amountCents: amountCents
            );

            var paymentKeyResponse = await _broker.RequestPaymentKeyAsync(paymentKeyRequest);

            var iframeUrl = _broker.CreateIframeSrc(_configuration["Paymob:IframeId"], paymentKeyResponse.PaymentKey);

            return iframeUrl;
        }
    }
}
