using ClueLessClient.Network;
using ClueLessServer.Models;
using System.Collections.Generic;

namespace ClueLessServer.Helpers
{
    public class CommandInterface
    {
        private static Dictionary<long, Command> commands = new Dictionary<long, Command>();
        private static Dictionary<string, Command> playerCommands = new Dictionary<string, Command>();

        public static Command GetCommand(GameModel game, PlayerModel player)
        {
            
            Command command = GetCommandForPlayer(player.Name);
            if (command != null)
                return command;

            commands.TryGetValue(game.Id, out command);
            return command;
        }

        public static void SetCommandForEveryone(GameModel game, Command command)
        {
            
            commands.Remove(game.Id);
            if (command != null)
                commands.Add(game.Id, command);
        }

        public static void SetCommandForPlayer(string playerName, Command command)
        {
            playerCommands.Remove(playerName);
            if (command != null)
                playerCommands.Add(playerName, command);
        }

        public static Command GetCommandForPlayer(string playerName)
        {
            Command command = null;
            playerCommands.TryGetValue(playerName, out command);
            return command;
        }

    }

}