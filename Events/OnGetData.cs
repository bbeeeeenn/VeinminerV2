using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using VeinminerV2.Models;

namespace VeinminerV2.Events;

public class OnGetData : Event
{
    public override void Disable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.NetGetData.Deregister(plugin, EventMethod);
    }

    public override void Enable(TerrariaPlugin plugin)
    {
        ServerApi.Hooks.NetGetData.Register(plugin, EventMethod);
    }

    private void EventMethod(GetDataEventArgs args)
    {
        TSPlayer player = TShock.Players[args.Msg.whoAmI];
        if (
            args.MsgID != PacketTypes.Tile
            || !Config.Settings.Enabled
            || !player.IsLoggedIn
            || !player.GetData<bool>("veinmining")
            || !player.HasPermission(Config.Settings.PermissionNode)
        )
        {
            return;
        }
        using BinaryReader reader = new(
            new MemoryStream(args.Msg.readBuffer, args.Index, args.Length)
        );
        byte action = reader.ReadByte();
        short tileX = reader.ReadInt16();
        short tileY = reader.ReadInt16();
        short flag = reader.ReadInt16();
        if (action != 0 || flag != 0)
        // Return if not a successful killtile packet
        {
            return;
        }

        ITile tile = Main.tile[tileX, tileY];
        if (!Config.Settings.TileWhitelists.Contains(tile.type))
        // Return if the block is not in the whitelist
        {
            return;
        }
        if (Core.TileToDestroy.Any(kv => kv.Key.Equals(new Point(tileX, tileY))))
        // Prevent veinmining an already claimed vein.
        {
            args.Handled = true;
            return;
        }

        WorldGen.KillTile_GetItemDrops(tileX, tileY, tile, out int dropItem, out _, out _, out _);

        if (
            Config.Settings.GiveItemsDirectly.DisableVeinmineWhenNoFreeSlot
            && !player.HasSlotFor(dropItem)
        )
        // Mine the ore normally
        {
            return;
        }

        args.Handled = true;
        Vein vein = Utils.GetVein(player, new Point(tileX, tileY));
        if (Config.Settings.GiveItemsDirectly.Enabled)
        {
            player.GiveItem(vein.dropNetId, vein.dropStack);
        }
        Core.TileToDestroy.AddRange(vein.tilePoints);
    }
}
