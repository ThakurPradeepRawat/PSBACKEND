using PSBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSBackend.DAL
{
  public interface IPSDAL
  {
    RegisterUserOutputModel Registeruser(RegisterUserInputModel input);
    GetUserByEmailOutputModel GetUserByEmail(GetUserByEmailInputModel input);
    GetUserByIdOutputModel GetUserById(GetUserByIdInputModel input);

    List<GetAllTemplesOutputModel> GetAllTemples();
    GetTempleByIdOutputModel GetTempleById(GetTempleByIdInputModel input);
    CreateTempleOutputModel CreateTemple(CreateTempleInputModel input);

    List<GetPrasadByTempleIdOutputModel> GetPrasadByTempleId(GetPrasadByTempleIdInputModel input);
    GetPrasadByIdOutputModel GetPrasadById(GetPrasadByIdInputModel input);
    CreatePrasadOutputModel CreatePrasad(CreatePrasadInputModel input);
    List <GetPrasadByIdOutputModel>  GetPopularPrasad();
    CreateOrderOutputModel CreateOrder(CreateOrderInputModel input);
    GetOrderByIdOutputModel GetOrderById(GetOrderByIdInputModel input);
    CreateOrderItemOutputModel CreateOrderItem(CreateOrderItemInputModel input);
    List<GetOrderItemsOutputModel> GetOrderItems(GetOrderItemsInputModel input);
  }
}
