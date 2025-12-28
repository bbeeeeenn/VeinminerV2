using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using VeinminerV2.Models;

namespace VeinminerV2.Events;

public class OnGameUpdate : Event
{
    public override void Disable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.GameUpdate.Deregister(plugin, EventMethod);
    }

    public override void Enable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.GameUpdate.Register(plugin, EventMethod);
    }

    private void EventMethod(EventArgs args)
    {
        for (int i = Core.TileToDestroy.Count - 1; i >= 0; i--)
        {
            KeyValuePair<Point, Vein.State> tileInfo = Core.TileToDestroy[i];
            tileInfo.Value.tick--;
            if (tileInfo.Value.tick < 0)
            {
                WorldGen.KillTile(tileInfo.Key.X, tileInfo.Key.Y, noItem: tileInfo.Value.noItem);
                NetMessage.SendData(
                    (int)PacketTypes.Tile,
                    -1,
                    -1,
                    null,
                    0,
                    tileInfo.Key.X,
                    tileInfo.Key.Y
                );
                Core.TileToDestroy.RemoveAt(i);
            }
        }
    }
}
