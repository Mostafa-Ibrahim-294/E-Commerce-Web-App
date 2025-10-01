using System.Text.Json;
using E_Commerce.Models;
using E_Commerce.Service.IService;
using Microsoft.AspNetCore.Mvc;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Callback;
namespace E_Commerce.Controllers
{
    [ApiController]
    [Route("api/paymob")]
    public class PaymobWebhookController : ControllerBase
    {
        private readonly IPaymobCashInBroker _broker;
        private readonly IOrderService _orderService;
        private readonly ILogger<PaymobWebhookController> _logger;

        public PaymobWebhookController(
            IPaymobCashInBroker broker,
            IOrderService orderService,
            ILogger<PaymobWebhookController> logger)
        {
            _broker = broker;
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Receive([FromQuery] string hmac, [FromBody] CashInCallback callback)
        {
            if (callback.Type is null || callback.Obj is null)
                return BadRequest("Invalid callback payload");

            var content = ((JsonElement)callback.Obj).GetRawText();

            switch (callback.Type.ToUpperInvariant())
            {
                case CashInCallbackTypes.Transaction:
                    var transaction = JsonSerializer.Deserialize<CashInCallbackTransaction>(content)!;

                    var valid = _broker.Validate(transaction, hmac);
                    if (!valid)
                        return BadRequest("Invalid HMAC");

                    var orderId = transaction.Order?.MerchantOrderId.ToString(); 
                    if (string.IsNullOrEmpty(orderId))
                    {
                        _logger.LogWarning(" Transaction received without MerchantOrderId");
                        return Ok();
                    }

                    var order = await _orderService.GetOrderHeader(int.Parse(orderId));
                    if (order == null)
                    {
                        _logger.LogWarning("Order {OrderId} not found in DB", orderId);
                        return Ok();
                    }

                    if (transaction.Success)
                    {
                        order.PaymentStatus = "Approved";
                        order.OrderStatus = "Approved";
                        _logger.LogInformation("Payment succeeded for Order {OrderId}", order.Id);
                    }
                    else
                    {
                        order.PaymentStatus = "Failed";
                        order.OrderStatus = "Cancelled";
                        _logger.LogWarning("Payment failed for Order {OrderId}", order.Id);
                    }
                    order.PaymentIntentId = transaction.Id.ToString();
                    order.PaymentDate = DateTime.Now;
                    await _orderService.Save();
                    return Ok();

                default:
                    _logger.LogWarning("Unexpected callback type {Type}", callback.Type);
                    return Ok();
            }
        }
    }
}
