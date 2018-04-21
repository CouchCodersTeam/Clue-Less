using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    class Game
    {
        private List<Player> players;  // Ryan changed from set to list
        private Board board;
        private Card[] caseFile;       // Casefile may be its own class, will leave as array for now
    }
}
