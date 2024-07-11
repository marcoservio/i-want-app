namespace IWantApp.Domain;

public class Category : EntityBase
{
    public string Name { get; private set; } = string.Empty;

    private Category() { }

    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
                    .IsNotNullOrWhiteSpace(Name, "Name")
                    .IsGreaterOrEqualsThan(Name, 3, "Name")
                    .IsNotNullOrWhiteSpace(CreatedBy, "CreatedBy")
                    .IsNotNullOrWhiteSpace(EditedBy, "EditedBy");

        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active, string editedBy)
    {
        Active = active;
        Name = name;
        EditedBy = editedBy;
        EditedOn = DateTime.UtcNow;

        Validate();
    }
}
