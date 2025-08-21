namespace Domain.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; private set; }

    private Tenant() { } // EF
    public Tenant(string name) => Name = name;
}