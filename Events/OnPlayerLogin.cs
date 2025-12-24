using TerrariaApi.Server;
using TShockAPI.Hooks;

namespace BenMiner.Events;

public class OnPlayerLogin : Models.Event
{
    public override void Disable(TerrariaPlugin plugin)
    {
        PlayerHooks.PlayerPostLogin -= EventMethod;
    }

    public override void Enable(TerrariaPlugin plugin)
    {
        PlayerHooks.PlayerPostLogin += EventMethod;
    }

    private void EventMethod(PlayerPostLoginEventArgs e)
    {
        e.Player.SetData("veinmining", true);
    }
}
