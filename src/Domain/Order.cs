namespace IWantApp.Domain;

public class Order : EntityBase
{
    public string ClientId { get; private set; } = string.Empty;
    public IList<Product> Products { get; private set; } = [];
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; } = string.Empty;

    private Order() { }

    public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        DeliveryAddress = deliveryAddress;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Total = 0;
        if (products?.Count > 0)
        {
            foreach (var product in products)
                Total += product.Price;
        }

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "Client")
            .IsTrue(Products?.Count > 0, "Products")
            .IsNotNullOrWhiteSpace(DeliveryAddress, "DeliveryAddress");

        AddNotifications(contract);
    }
}
