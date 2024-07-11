namespace IWantApp.Domain;

public class Product : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool HasStock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public decimal Price { get; private set; }
    public IList<Order> Orders { get; private set; } = [];

    private Product() { }

    public Product(string name, Category category, string description, bool hasStock, decimal price, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrWhiteSpace(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrWhiteSpace(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsGreaterOrEqualsThan(Price, 0, "Price")
            .IsNotNullOrWhiteSpace(CreatedBy, "CreatedBy")
            .IsNotNullOrWhiteSpace(EditedBy, "EditedBy");

        AddNotifications(contract);
    }
}
