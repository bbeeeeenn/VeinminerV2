using Terraria;
using TShockAPI;

namespace VeinminerV2;

public static class Extension
{
    public static bool HasSlotFor(this TSPlayer player, int netId, int stack)
    {
        for (int i = 0; i < NetItem.InventorySlots; i++)
        {
            Item item = player.TPlayer.inventory[i];
            if (item.netID == netId && item.stack > 0 && item.stack + stack <= item.maxStack)
            {
                return true;
            }
        }
        return player.InventorySlotAvailable;
    }
}
