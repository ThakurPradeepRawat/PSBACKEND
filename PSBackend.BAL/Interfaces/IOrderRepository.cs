using PSBackend.Models;

namespace PSBackend.BAL.Interfaces;

public interface IOrderRepository
{
    CreateOrderOutputModel CreateOrder(CreateOrderInputModel order);
    GetOrderByIdOutputModel? GetOrderById(int orderId);
    CreateOrderItemOutputModel AddOrderItem(CreateOrderItemInputModel orderItem);
    List<GetOrderItemsOutputModel> GetOrderItems(int orderId);
    UpdateOrderPaymentOutputModel UpdateOrderPayment(UpdateOrderPaymentInputModel input);
}
