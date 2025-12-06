using Template.Models;
using Template.RestRoutes;
using TShockAPI;

namespace Template;

public class RestManager
{
    public static readonly List<RestRoute> Routes = new()
    {
        // Routes
        new TestRoute(),
    };

    public static void RegisterAll()
    {
        foreach (RestRoute route in Routes)
        {
            TShock.RestApi.Register(route.Path, route.Callback);
        }
    }
}
