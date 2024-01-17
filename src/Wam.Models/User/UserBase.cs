namespace Wam.Models.User;

public abstract record UserBase : IWamModel
{
    protected UserBase(string id)
    {
        Id = id;
    }
    public string Id { get; }
}
