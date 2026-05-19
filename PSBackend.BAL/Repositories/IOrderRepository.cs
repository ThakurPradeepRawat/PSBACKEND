using PSBackend.Models;

namespace PSBackend.BAL.Repositories;

public interface IOrderRepository
{
    CreateOrderOutputModel CreateOrder(CreateOrderInputModel order);
    GetOrderByIdOutputModel? GetOrderById(int orderId);
    CreateOrderItemOutputModel AddOrderItem(CreateOrderItemInputModel orderItem);
    List<GetOrderItemsOutputModel> GetOrderItems(int orderId);
}
