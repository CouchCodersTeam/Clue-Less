using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    enum CardType
    {
        Room,
        Location,
        Weapon
    }

    class Card
    {
        private CardType cardType;
        private String cardValue;
    }
}
