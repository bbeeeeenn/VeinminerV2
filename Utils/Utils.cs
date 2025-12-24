using BenMiner.Models;
using Microsoft.Xna.Framework;
using Terraria;
using TShockAPI;

namespace BenMiner;

public class Utils
{
    public static Vein GetVein(TSPlayer player, Point start)
    {
        ITile startTile = Main.tile[start.X, start.Y];
        WorldGen.KillTile_GetItemDrops(
            start.X,
            start.Y,
            startTile,
            out int itemDrop,
            out _,
            out _,
            out _
        );
        int remainingSlot = player.RemainingSlotFor(itemDrop);

        Vein vein = new(player, itemDrop);

        Queue<KeyValuePair<Point, int>> queue = new();
        queue.Enqueue(new(start, 0));

        do
        {
            KeyValuePair<Point, int> _ = queue.Dequeue();
            Point pos = _.Key;
            int tick = _.Value;

            ITile tile = Main.tile[pos.X, pos.Y];
            if (
                tile.type != startTile.type
                || !tile.active()
                || pos.X < 0
                || pos.Y < 0
                || pos.X >= Main.maxTilesX
                || pos.Y >= Main.maxTilesY
                || vein.tilePoints.Keys.Any(tp => tp.Equals(pos))
            )
            {
                continue;
            }

            if (Settings.Config.GiveItemsDirectly.Enabled)
            {
                if (vein.dropStack < remainingSlot)
                {
                    vein.dropStack++;
                    vein.tilePoints.Add(pos, new(tick, true));
                }
                else if (!Settings.Config.GiveItemsDirectly.DisableVeinmineWhenNoFreeSlot)
                {
                    vein.tilePoints.Add(pos, new(tick, false));
                }
                else
                {
                    break;
                }
            }
            else
            {
                vein.tilePoints.Add(pos, new(tick, false));
            }

            // for (int xOffset = -1; xOffset <= 1; xOffset++)
            // {
            //     for (int yOffset = -1; yOffset <= 1; yOffset++)
            //     {
            //         if (xOffset == 0 && yOffset == 0)
            //             continue;

            //         queue.Enqueue(new(new Point(pos.X + xOffset, pos.Y + yOffset), tick + 2));
            //     }
            // }
            queue.Enqueue(new(new Point(pos.X, pos.Y - 1), tick + 2));
            queue.Enqueue(new(new Point(pos.X, pos.Y + 1), tick + 2));
            queue.Enqueue(new(new Point(pos.X - 1, pos.Y), tick + 2));
            queue.Enqueue(new(new Point(pos.X + 1, pos.Y), tick + 2));
        } while (queue.Any());

        return vein;
    }
}
