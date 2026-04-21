namespace QuillApp.Blazor.State;

public class CurrentApp
{
    public int? AppId { get; private set; }

    public void Set(int appId)
    {
        AppId = appId;
    }

    public void Clear()
    {
        AppId = null;
    }
}