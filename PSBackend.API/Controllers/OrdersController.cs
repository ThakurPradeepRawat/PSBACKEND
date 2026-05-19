using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepo;

    public OrdersController(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
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

            var result = _orderRepo.CreateOrder(input);
            return Ok(result);
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
}
