using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.ViewModel
{
    class EventArgStructures
    {
        public class MoveEventCommand : EventArgs
        {
            public string p;
            public int fromX;
            public int fromY;
            public int toX;
            public int toY;

            public MoveEventCommand(string p, int fromX, int fromY, int toX, int toY)
            {
                this.p = p;
                this.fromX = fromX;
                this.fromY = fromY;
                this.toX = toX;
                this.toY = toY;
            }
        }

        public class AddPlayerEventCommand : EventArgs
        {
            public string person;
            public int x;
            public int y;

            public AddPlayerEventCommand(string person, int x, int y)
            {
                this.person = person;
                this.x = x;
                this.y = y;
            }
        }

        public class SuggestionIncomming : EventArgs
        {
            public Board_Controller.Person suggester;
            public Board_Controller.Person p;
            public Board_Controller.Room r;
            public Board_Controller.Weapon w;

            public SuggestionIncomming(Board_Controller.Person suggester, Board_Controller.Person p, Board_Controller.Room r, Board_Controller.Weapon w)
            {
                this.suggester = suggester;
                this.p = p;
                this.r = r;
                this.w = w;
            }
        }

        public class StringVal : EventArgs
        {
            public string val;

            public StringVal(string val)
            {
                this.val = val;
            }
        }

        public class GameOver : EventArgs
        {
            public Board_Controller.Person winner;
            public Board_Controller.Person p;
            public Board_Controller.Room r;
            public Board_Controller.Weapon w;

            public GameOver(Board_Controller.Person winner, Board_Controller.Person p, Board_Controller.Room r, Board_Controller.Weapon w)
            {
                this.winner = winner;
                this.p = p;
                this.r = r;
                this.w = w;
            }
        }
    }
}
