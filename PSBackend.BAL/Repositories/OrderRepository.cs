using System.Collections.Generic;
using PSBackend.BAL.Interfaces;
using PSBackend.DAL;
using PSBackend.Models;

namespace PSBackend.BAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IPSDAL _sqlDataClient;

        public OrderRepository(IPSDAL psdal)
        {
            _sqlDataClient = psdal;
        }

        public CreateOrderOutputModel CreateOrder(CreateOrderInputModel order)
        {
            if (string.IsNullOrWhiteSpace(order.OrderNumber))
            {
                order.OrderNumber = $"PRS-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            }

            order.DeliveryType ??= "Standard";
            order.SpecialInstructions ??= string.Empty;

            return _sqlDataClient.CreateOrder(order);
        }

        public GetOrderByIdOutputModel? GetOrderById(int orderId)
        {
            return _sqlDataClient.GetOrderById(new GetOrderByIdInputModel { OrderId = orderId });
        }

        public CreateOrderItemOutputModel AddOrderItem(CreateOrderItemInputModel orderItem)
        {
            return _sqlDataClient.CreateOrderItem(orderItem);
        }

        public List<GetOrderItemsOutputModel> GetOrderItems(int orderId)
        {
            return _sqlDataClient.GetOrderItems(new GetOrderItemsInputModel { OrderId = orderId });
        }

        public UpdateOrderPaymentOutputModel UpdateOrderPayment(UpdateOrderPaymentInputModel input)
        {
            return _sqlDataClient.UpdateOrderPayment(input);
        }
    }
}
