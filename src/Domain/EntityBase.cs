namespace IWantApp.Domain;

public abstract class EntityBase : Notifiable<Notification>
{
    protected EntityBase() => Id = Guid.NewGuid();

    public Guid Id { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; } = string.Empty;
    public DateTime EditedOn { get; set; }
    public bool Active { get; protected set; } = true;
}
