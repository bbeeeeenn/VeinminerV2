using BenMiner.Commands;
using BenMiner.Models;

namespace BenMiner;

public class CommandManager
{
    public static readonly List<Command> Commands = new()
    {
        // Commands
        new Veinmine(),
    };

    public static void RegisterAll()
    {
        foreach (Command command in Commands)
        {
            TShockAPI.Commands.ChatCommands.Add(command);
        }
    }
}
