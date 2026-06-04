using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PSBackend.BAL.Interfaces;
using PSBackend.Models;
using Razorpay.Api;
using System.Collections.Generic;

namespace PSBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IConfiguration _config;

    public OrdersController(IOrderRepository orderRepo, IConfiguration config)
    {
        _orderRepo = orderRepo;
        _config = config;
    }

    [HttpPost("GetOrderById")]
    public ActionResult<GetOrderByIdOutputModel> GetOrderById([FromBody] GetOrderByIdInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var order = _orderRepo.GetOrderById(input.OrderId);
            if (order == null)
            {
                return NotFound($"Order with ID {input.OrderId} not found.");
            }
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult<CreateOrderOutputModel> CreateOrder([FromBody] CreateOrderInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("Order object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            
            // 1. Generate local Order Number
            input.OrderNumber = $"PRS-{DateTime.UtcNow:yyyyMMddHHmmssfff}";

            // 2. Create Razorpay Order
            var client = new RazorpayClient(_config["Razorpay:KeyId"], _config["Razorpay:KeySecret"]);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", (int)(input.TotalAmount * 100)); // amount in paisa
            options.Add("receipt", input.OrderNumber);
            options.Add("currency", "INR");
            
            Razorpay.Api.Order rzpOrder = client.Order.Create(options);
            input.RazorpayOrderId = rzpOrder["id"].ToString();

            // 3. Save to database
            var result = _orderRepo.CreateOrder(input);
            return Ok(new { 
                orderId = result.OrderId, 
                razorpayOrderId = input.RazorpayOrderId,
                amount = input.TotalAmount
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("item")]
    public ActionResult<CreateOrderItemOutputModel> CreateOrderItem([FromBody] CreateOrderItemInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("OrderItem object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var result = _orderRepo.AddOrderItem(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("GetOrderItems")]
    public ActionResult<List<GetOrderItemsOutputModel>> GetOrderItems([FromBody] GetOrderItemsInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var items = _orderRepo.GetOrderItems(input.OrderId);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public class VerifyPaymentInputModel 
    {
        public int OrderId { get; set; }
        public string RazorpayOrderId { get; set; } = string.Empty;
        public string RazorpayPaymentId { get; set; } = string.Empty;
        public string RazorpaySignature { get; set; } = string.Empty;
    }

    [HttpPost("VerifyPayment")]
    public ActionResult VerifyPayment([FromBody] VerifyPaymentInputModel input)
    {
        try
        {
            string secret = _config["Razorpay:KeySecret"];
            string signaturePayload = input.RazorpayOrderId + "|" + input.RazorpayPaymentId;
            
            var crypt = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(secret));
            string expectedSignature = BitConverter.ToString(crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signaturePayload))).Replace("-", "").ToLower();
            
            if (expectedSignature == input.RazorpaySignature)
            {
                _orderRepo.UpdateOrderPayment(new UpdateOrderPaymentInputModel {
                    OrderId = input.OrderId,
                    PaymentId = input.RazorpayPaymentId,
                    PaymentStatus = "Success",
                    OrderStatus = "Confirmed"
                });
                return Ok(new { success = true });
            }
            else 
            {
                return BadRequest(new { success = false, message = "Invalid signature" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
