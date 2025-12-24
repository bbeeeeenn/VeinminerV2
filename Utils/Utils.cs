using Microsoft.Xna.Framework;
using Terraria;
using TShockAPI;
using VeinminerV2.Models;

namespace VeinminerV2;

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

        Vein vein = new(player, itemDrop);

        Stack<KeyValuePair<Point, int>> stack = new();
        stack.Push(new(start, 0));

        do
        {
            KeyValuePair<Point, int> _ = stack.Pop();
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
            if (vein.owner.HasSlotFor(itemDrop, vein.dropStack + 1))
            {
                vein.dropStack++;
            }
            vein.tilePoints.Add(
                pos,
                new(tick, !vein.owner.HasSlotFor(itemDrop, vein.dropStack + 1))
            );
            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (int yOffset = -1; yOffset <= 1; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0)
                        continue;

                    stack.Push(new(new Point(pos.X + xOffset, pos.Y + yOffset), tick + 2));
                }
            }
        } while (stack.Any());

        return vein;
    }
}
