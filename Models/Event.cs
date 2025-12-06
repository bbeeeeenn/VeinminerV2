using TerrariaApi.Server;

namespace Template.Models;

public abstract class Event
{
    public abstract void Enable(TerrariaPlugin plugin);
    public abstract void Disable(TerrariaPlugin plugin);
}
