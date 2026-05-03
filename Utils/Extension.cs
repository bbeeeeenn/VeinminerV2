using Terraria;
using TShockAPI;

namespace VeinminerV2;

public static class Extension
{
    public static bool HasSlotFor(this TSPlayer player, int type, int stack = 1)
    {
        if (!Config.Settings.GiveItemsDirectly.Enabled)
            return true;

        for (int i = 0; i < NetItem.InventorySlots; i++)
        {
            Item item = player.TPlayer.inventory[i];
            if (item.type == type && item.stack > 0 && item.stack + stack <= item.maxStack)
            {
                return true;
            }
        }
        return player.InventorySlotAvailable;
    }

    public static int RemainingSlotFor(this TSPlayer player, int type)
    {
        if (!Config.Settings.GiveItemsDirectly.Enabled)
            return 0;

        Item sample = TShock.Utils.GetItemById(type);
        int count = 0;
        for (int i = 0; i < 50; i++)
        {
            Item currSlot = player.TPlayer.inventory[i];
            if (currSlot.stack == 0 || currSlot.type == type)
            {
                count += sample.maxStack - currSlot.stack;
            }
        }
        return count;
    }
}
