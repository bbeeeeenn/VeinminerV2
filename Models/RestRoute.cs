namespace Template.Models;

public abstract class RestRoute
{
    public abstract string Path { get; set; }
    public abstract object Callback(Rests.RestRequestArgs args);
}
