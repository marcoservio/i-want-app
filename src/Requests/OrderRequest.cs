namespace IWantApp.Requests;

public record OrderRequest(List<Guid> ProductIds, string DeliveryAddress);
