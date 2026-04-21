namespace QuillApp.Blazor.State;

public class CurrentUser
{
    public int? UserId { get; private set; }
    public string? Email { get; private set; }

    public bool IsAuthenticated => UserId is not null;

    public void Set(int userId, string? email = null)
    {
        UserId = userId;
        Email = email;
    }

    public void Clear()
    {
        UserId = null;
        Email = null;
    }
}
