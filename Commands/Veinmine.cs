using Microsoft.Xna.Framework;
using Terraria;
using TShockAPI;

namespace BenMiner.Commands;

public class Veinmine : Models.Command
{
    public override bool AllowServer => false;
    public override string[] Aliases { get; set; } = Settings.Config.CommandAliases;
    public override string PermissionNode { get; set; } = Settings.Config.PermissionNode;

    public override void Execute(CommandArgs args)
    {
        if (!args.Player.IsLoggedIn)
        {
            args.Player.SendErrorMessage("You must log-in first.");
            return;
        }
        if (!Settings.Config.Enabled)
        {
            args.Player.SendErrorMessage("Veinmining is disabled.");
            return;
        }
        TSPlayer player = args.Player;

        player.SetData("veinmining", !player.GetData<bool>("veinmining"));
        player.SendSuccessMessage(
            $"Veinmining is turned {(player.GetData<bool>("veinmining") ? "ON".Color(Color.LightBlue.Hex3()) : "OFF".Color(Color.OrangeRed.Hex3()))}."
        );
    }
}
