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
        Suspect,
        Weapon
    }

    enum Room
    {
        Study,
        Kitchen,
        Ballroom,
        Conservatory,
        Billiard,
        Library,
        Hall,
        Lounge,
        Dining
    }

    enum Suspect
    {
        Scarlet,
        Mustard,
        White,
        Green,
        Peacock,
        Plum
    }

    enum Weapon
    {
        Candlestick,
        Revolver,
        Knife,
        Pipe,
        Rope,
        Wrench
    }

    class Card
    {
        private CardType cardType;
        private String cardValue;

        if (CardType == 0)
            {
                int Study = (int)Room.Study;
                int Kitchen = (int)Room.Kitchen;
                int Ballroom = (int)Room.Ballroom;
                int Conservatory = (int)Room.Conservatory;
                int Billiard = (int)Room.Billiard;
                int Library = (int)Room.Library;
                int Hall = (int)Room.Hall;
                int Lounge = (int)Room.Lounge;
                int Dining = (int)Room.Dining;

                if (Study == 0)
                    cardValue = "Study";
                else-if(Kitchen == 1)
                    cardValue = "Kitchen";
                else-if(Ballroom == 2)
                    cardValue = "Ballroom";
                else-if(Conservatory == 3)
                    cardValue = "Conservatory";
                else-if(Billiard == 4)
                    cardValue = "Billarad";
                else-if(Library == 5)
                    cardValue = "Library";
                else-if(Hall == 6)
                    cardValue = "Hall";
                else-if(Lounge == 7)
                    cardValue = "Lounge";
                else-if(Dining == 8)
                    cardValue = "Dining";

            }
        else-if (CardType == 1)
            {
                int Scarlet = (int)Suspect.Scarlet;
                int Mustard = (int)Suspect.Mustard;
                int White = (int)Suspect.White;
                int Green = (int)Suspect.Green;
                int Peacock = (int)Suspect.Peacock;
                int Plum = (int)Suspect.Plum;

                if (Scarlet == 0)
                    cardValue = "Scarlet";
                else-if(Mustard == 1)
                    cardValue = "Mustard";
                else-if(White == 2)
                    cardValue = "White";
                else-if(Green == 3)
                    cardValue = "Green";
                else-if(Peacock == 4)
                    cardValue = "Peacock";
                else-if(Plum == 5)
                    cardValue = "Plum";
            }
        else-if (CardType == 2)
            {
                int Candlestick = (int)Weapon.Candlestick;
                int Revolver = (int)Weapon.Revolver;
                int Knife = (int)Weapon.Knife;
                int Pipe = (int)Weapon.Pipe;
                int Rope = (int)Weapon.Rope;
                int Wrench = (int)Weapon.Wrench;

                if (Candlestick == 0)
                    cardValue = "Candlestick";
                else-if(Revolver == 1)
                    cardValue = "Revolver";
                else-if(Knife == 2)
                    cardValue = "Knife";
                else-if(Pipe == 3)
                    cardValue = "Pipe";
                else-if(Rope == 4)
                    cardValue = "Rope";
                else-if(Wrench == 5)
                    cardValue = "Wrench";
            }


    }
    
}
