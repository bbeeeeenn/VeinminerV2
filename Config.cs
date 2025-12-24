using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.ID;
using TShockAPI;
using VeinminerV2.Models;

namespace VeinminerV2;

public class Settings
{
    public static readonly string PluginDirectory = Path.Combine(TShock.SavePath, Core.PluginName);
    public static readonly string ConfigPath = Path.Combine(PluginDirectory, "config.json");

    public static Settings Config { get; set; } = new();
    #region Configs
    public bool Enabled = true;
    public ConfigGiveItemsDirectly GiveItemsDirectly = new();
    public int MaxTileDestroy = 1000;
    public List<int> TileWhitelists = new()
    {
        TileID.Copper,
        TileID.Tin,
        TileID.Iron,
        TileID.Lead,
        TileID.Silver,
        TileID.Tungsten,
        TileID.Gold,
        TileID.Platinum,
        TileID.Meteorite,
        TileID.Demonite,
        TileID.Crimtane,
        TileID.Obsidian,
        TileID.Hellstone,
        TileID.Cobalt,
        TileID.Palladium,
        TileID.Mythril,
        TileID.Orichalcum,
        TileID.Adamantite,
        TileID.Titanium,
        TileID.Chlorophyte,
        TileID.LunarOre,
        TileID.Diamond,
        TileID.Ruby,
        TileID.Emerald,
        TileID.Sapphire,
        TileID.Topaz,
        TileID.Amethyst,
        TileID.ExposedGems,
        TileID.Silt,
        TileID.Slush,
        TileID.DesertFossil,
    };
    public string[] CommandAliases = { "veinminer", "vm" };
    public string PermissionNode = "veinminer";

    #endregion
    public static void Save()
    {
        string configJson = JsonConvert.SerializeObject(Config, Formatting.Indented);
        File.WriteAllText(ConfigPath, configJson);
    }

    public static ResponseMessage Load()
    {
        if (!Directory.Exists(PluginDirectory))
        {
            Directory.CreateDirectory(PluginDirectory);
        }
        if (!File.Exists(ConfigPath))
        {
            Save();
            return new ResponseMessage()
            {
                Text =
                    $"[{Core.PluginName}] Config file doesn't exist yet. A new one has been created.",
                Color = Color.Yellow,
            };
        }
        else
        {
            try
            {
                string json = File.ReadAllText(ConfigPath);
                Settings? deserializedConfig = JsonConvert.DeserializeObject<Settings>(
                    json,
                    new JsonSerializerSettings()
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace,
                    }
                );
                if (deserializedConfig != null)
                {
                    Config = deserializedConfig;
                    Save();
                    return new ResponseMessage()
                    {
                        Text = $"[{Core.PluginName}] Loaded config.",
                        Color = Color.LimeGreen,
                    };
                }
                else
                {
                    return new ResponseMessage()
                    {
                        Text =
                            $"[{Core.PluginName}] Config file was found, but deserialization returned null.",
                        Color = Color.Red,
                    };
                }
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError($"[{Core.PluginName}] Error loading config: {ex.Message}");
                return new ResponseMessage()
                {
                    Text =
                        $"[{Core.PluginName}] Error loading config. Check logs for more details.",
                    Color = Color.Red,
                };
            }
        }
    }

    public class ConfigGiveItemsDirectly
    {
        public bool Enabled = true;
        public bool DisableVeinmineWhenNoFreeSlot = false;
    }
}
