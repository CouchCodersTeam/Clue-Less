using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLess_Mockups.UI_Controller
{
    class Board_Controller : UI_Controller_Base
    {
        //This should probably be somewhere else
        enum Person { Scarlet, Mustard, Plum, Peacock, Green, White};
        enum Rooms { Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen }
        List<Person> StudyOccupants = new List<Person>();
        List<Person> HallOccupants = new List<Person>();
        List<Person> LoungeOccupants = new List<Person>();
        List<Person> LibraryOccupants = new List<Person>();
        List<Person> BilliardOccupants = new List<Person>();
        List<Person> DiningOccupants = new List<Person>();
        List<Person> ConservatoryOccupants = new List<Person>();

        public Board_Controller()
        {


        }
    }
}
