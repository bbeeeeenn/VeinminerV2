using TerrariaApi.Server;

namespace VeinminerV2 // TODO: Rename the entire namespace
{
    [ApiVersion(2, 1)]
    public class Core : TerrariaPlugin
    {
        public static readonly string PluginName = "VeinminerV2"; // TODO: Configure this
        public override string Name => PluginName;
        public override string Author => "TRANQUILZOIIP - github.com/bbeeeeenn";
        public override string Description => base.Description;
        public override Version Version => base.Version;

        public Core(Terraria.Main game)
            : base(game) { }

        public override void Initialize()
        {
            // Load config
            // TShock.Log.ConsoleInfo(Settings.Load().Text);

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
    }
}
