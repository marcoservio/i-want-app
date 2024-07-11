namespace IWantApp.Domain;

public class Pagination : Notifiable<Notification>
{
    public int? Page { get; private set; }
    public int? Rows { get; private set; }

    private Pagination() { }

    public Pagination(int? page, int? rows)
    {
        Page = page;
        Rows = rows;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Pagination>()
                    .IsNotNull(Page, "Page")
                    .IsNotNull(Rows, "Rows")
                    .IsLowerThan(Rows ?? 0, 10, "Rows");

        AddNotifications(contract);
    }
}
