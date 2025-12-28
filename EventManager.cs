using TerrariaApi.Server;
using VeinminerV2.Events;
using VeinminerV2.Models;

namespace VeinminerV2;

public class EventManager
{
    public static readonly List<Event> events = new()
    {
        // Events
        new OnReload(),
        new OnGetData(),
        new OnGameUpdate(),
        new OnPlayerLogin(),
    };

    public static void RegisterAll(TerrariaPlugin plugin)
    {
        foreach (Event _event in events)
        {
            _event.Enable(plugin);
        }
    }

    public static void DeregisterAll(TerrariaPlugin plugin)
    {
        foreach (Event _event in events)
        {
            _event.Disable(plugin);
        }
    }
}
