using PSBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
namespace PSBackend.DAL
{
  public  class PSDAL : IPSDAL
  {
    private readonly SqlDataClient _sqlDataClient;
    public PSDAL(SqlDataClient sqlDataClient) {
      _sqlDataClient = sqlDataClient;

    }
    public RegisterUserOutputModel  Registeruser(RegisterUserInputModel input)
    {
      return _sqlDataClient.SingleModel<RegisterUserInputModel, RegisterUserOutputModel>("[auth].[auth_sp_CreateUser]", input);
    }

    public GetUserByEmailOutputModel GetUserByEmail(GetUserByEmailInputModel input)
    {
      return _sqlDataClient.SingleModel<GetUserByEmailInputModel, GetUserByEmailOutputModel>("[auth].[auth_sp_GetUserByEmail]", input);
    }

    public GetUserByIdOutputModel GetUserById(GetUserByIdInputModel input)
    {
      return _sqlDataClient.SingleModel<GetUserByIdInputModel, GetUserByIdOutputModel>("[auth].[auth_sp_GetUserById]", input);
    }

    public List<GetAllTemplesOutputModel> GetAllTemples()
    {
        return _sqlDataClient.ListModel<object, GetAllTemplesOutputModel>("[temple].[temple_sp_GetAllTemples]", new {});
    }

    public GetTempleByIdOutputModel GetTempleById(GetTempleByIdInputModel input)
    {
        return _sqlDataClient.SingleModel<GetTempleByIdInputModel, GetTempleByIdOutputModel>("[temple].[temple_sp_GetTempleById]", input);
    }

    public CreateTempleOutputModel CreateTemple(CreateTempleInputModel input)
    {
        return _sqlDataClient.SingleModel<CreateTempleInputModel, CreateTempleOutputModel>("[temple].[temple_sp_CreateTemple]", input);
    }

    public List<GetPrasadByTempleIdOutputModel> GetPrasadByTempleId(GetPrasadByTempleIdInputModel input)
    {
        return _sqlDataClient.ListModel<GetPrasadByTempleIdInputModel, GetPrasadByTempleIdOutputModel>("[catalog].[catalog_sp_GetPrasadByTempleId]", input);
    }

    public GetPrasadByIdOutputModel GetPrasadById(GetPrasadByIdInputModel input)
    {
        return _sqlDataClient.SingleModel<GetPrasadByIdInputModel, GetPrasadByIdOutputModel>("[catalog].[catalog_sp_GetPrasadById]", input);
    }

    public CreatePrasadOutputModel CreatePrasad(CreatePrasadInputModel input)
    {
        return _sqlDataClient.SingleModel<CreatePrasadInputModel, CreatePrasadOutputModel>("[catalog].[catalog_sp_CreatePrasad]", input);
    }

    public List<GetPrasadByIdOutputModel> GetPopularPrasad()
    {
      return _sqlDataClient.ListModel<object, GetPrasadByIdOutputModel>("[catalog].[catalog_sp_GetPopularPrasad]", new { });
    }
    public CreateOrderOutputModel CreateOrder(CreateOrderInputModel input)
    {
        return _sqlDataClient.SingleModel<CreateOrderInputModel, CreateOrderOutputModel>("[orders].[orders_sp_CreateOrder]", input);
    }

    public GetOrderByIdOutputModel GetOrderById(GetOrderByIdInputModel input)
    {
        return _sqlDataClient.SingleModel<GetOrderByIdInputModel, GetOrderByIdOutputModel>("[orders].[orders_sp_GetOrderById]", input);
    }

    public CreateOrderItemOutputModel CreateOrderItem(CreateOrderItemInputModel input)
    {
        return _sqlDataClient.SingleModel<CreateOrderItemInputModel, CreateOrderItemOutputModel>("[orders].[orders_sp_CreateOrderItem]", input);
    }

    public List<GetOrderItemsOutputModel> GetOrderItems(GetOrderItemsInputModel input)
    {
        return _sqlDataClient.ListModel<GetOrderItemsInputModel, GetOrderItemsOutputModel>("[orders].[orders_sp_GetOrderItems]", input);
    }

    public UpdateOrderPaymentOutputModel UpdateOrderPayment(UpdateOrderPaymentInputModel input)
    {
        return _sqlDataClient.SingleModel<UpdateOrderPaymentInputModel, UpdateOrderPaymentOutputModel>("[orders].[orders_sp_UpdateOrderPayment]", input);
    }

    public List<FestivalModel> GetAllFestivals()
    {
        return _sqlDataClient.ListModel<object, FestivalModel>("[common].[common_sp_GetAllFestivals]", new { });
    }

    public List<FestivalProductModel> GetAllFestivalProducts()
    {
        return _sqlDataClient.ListModel<object, FestivalProductModel> ("[common].[common_sp_GetAllFestivalProducts]", new { });
    }

    public List<PujaCategoryModel> GetAllPujaCategories()
    {
        return _sqlDataClient.ListModel<object, PujaCategoryModel>("[common].[common_sp_GetAllPujaCategories]", new { });
    }

    public List<PujaModel> GetAllPujas()
    {
        return _sqlDataClient.ListModel<object, PujaModel>("[common].[common_sp_GetAllPujas]", new { });
    }

    public List<RatingModel> GetAllRatings()
    {
        return _sqlDataClient.ListModel<object, RatingModel>(" [common].[common_sp_GetAllRatings]", new { });
    }

    public List<RegionModel> GetAllRegions()
    {
        return _sqlDataClient.ListModel<object, RegionModel>("[common].[common_sp_GetAllRegions]", new { });
    }

    public List<GiftBoxModel> GetAllGiftBoxes()
    {
        return _sqlDataClient.ListModel<object, GiftBoxModel>("[common].[common_sp_GetAllGiftBoxes]", new { });
    }

    public List<GiftOccasionModel> GetAllGiftOccasions()
    {
        return _sqlDataClient.ListModel<object, GiftOccasionModel>("[common].[common_sp_GetAllGiftOccasions] ", new { });
    }
  }
}
