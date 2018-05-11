using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Lobby
    {
        private int MAXPLAYERS = 6;
        private int MINPLAYERS = 3;
        private List<Player> playersInLobby;

        public Lobby()
        {
            playersInLobby = new List<Player>();

        }

    }
}
