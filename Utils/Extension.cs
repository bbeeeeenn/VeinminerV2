using Terraria;
using TShockAPI;

namespace BenMiner;

public static class Extension
{
    public static bool HasSlotFor(this TSPlayer player, int netId, int stack = 1)
    {
        if (!Settings.Config.GiveItemsDirectly.Enabled)
            return true;

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

    public static int RemainingSlotFor(this TSPlayer player, int netId)
    {
        if (!Settings.Config.GiveItemsDirectly.Enabled)
            return 0;

        Item sample = TShock.Utils.GetItemById(netId);
        int count = 0;
        for (int i = 0; i < 50; i++)
        {
            Item currSlot = player.TPlayer.inventory[i];
            if (currSlot.stack == 0 || currSlot.netID == netId)
            {
                count += sample.maxStack - currSlot.stack;
            }
        }
        return count;
    }
}
