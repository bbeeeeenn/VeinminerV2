using BenMiner.Events;
using BenMiner.Models;
using TerrariaApi.Server;

namespace BenMiner;

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
