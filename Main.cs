using BenMiner.Models;
using Microsoft.Xna.Framework;
using TerrariaApi.Server;
using TShockAPI;

namespace BenMiner
{
    [ApiVersion(2, 1)]
    public class Core : TerrariaPlugin
    {
        public static readonly string PluginName = "VeinminerV2";
        public override string Name => PluginName;
        public override string Author => "TRANQUILZOIIP - github.com/bbeeeeenn";
        public override string Description => "Excavates your ore vein.";
        public override Version Version => new(1, 0, 1);

        public Core(Terraria.Main game)
            : base(game) { }

        public override void Initialize()
        {
            // Load config
            TShock.Log.ConsoleInfo(Settings.Load().Text);

            // Load events
            EventManager.RegisterAll(this);

            // Load commands
            CommandManager.RegisterAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventManager.DeregisterAll(this);
            }
            base.Dispose(disposing);
        }

        public static readonly List<KeyValuePair<Point, Vein.State>> TileToDestroy = new();
    }
}
