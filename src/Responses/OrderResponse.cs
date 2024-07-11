namespace IWantApp.Responses;

public record OrderResponse(Guid Id, string EmailName, List<OrderProduct> Products, string DeliveryAddress, decimal Total);

public record OrderProduct(Guid Id, string Nome);