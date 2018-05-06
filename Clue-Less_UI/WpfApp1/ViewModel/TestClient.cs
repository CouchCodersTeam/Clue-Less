using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    class TestClient
    {
        public TestClient() { }

        public event EventHandler MoveEvent;

        public bool MoveUp()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Scarlet, 
                                                               Board_Controller.Room.Billiard, 
                                                               Board_Controller.Room.HallBilliardRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        public bool MoveDown()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Plum,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.BilliardRoomBallroom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        public bool MoveLeft()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Mustard,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.LibraryBilliardRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        public bool MoveRight()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Peacock,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.BilliardRoomDiningRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        public bool ActivateLoungeSecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.White,
                                                               Board_Controller.Room.Lounge,
                                                               Board_Controller.Room.Conservatory);
            MoveEvent(this, args);
            return success;
        }

        public bool ActivateConservatorySecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Peacock,
                                                               Board_Controller.Room.Conservatory,
                                                               Board_Controller.Room.Lounge);
            MoveEvent(this, args);
            return success;
        }

        public bool ActivateKitchenSecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Mustard,
                                                               Board_Controller.Room.Kitchen,
                                                               Board_Controller.Room.Study);
            MoveEvent(this, args);
            return success;
        }


        public bool ActivateStudySecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.White,
                                                               Board_Controller.Room.Study,
                                                               Board_Controller.Room.Kitchen);
            MoveEvent(this, args);
            return success;
        }

        public bool MakeSuggestion(Board_Controller.Person person, Board_Controller.Room room, Board_Controller.Weapon weapon)
        {
            bool success = true;

            return success;
        }

        public bool MakeAccusation(Board_Controller.Person person, Board_Controller.Room room, Board_Controller.Weapon weapon)
        {
            bool success = true;

            return success;
        }

        public List<string> GetCards()
        {
            List<string> hand = new List<string>();

            hand.Add("Miss Scarlet");
            hand.Add("Mrs White");
            hand.Add("Knife");
            hand.Add("Rope");
            hand.Add("Revolver");
            hand.Add("Study");

            return hand;
        }

        public List<Board_Controller.PersonLocation> GetBoardState()
        {
            List<Board_Controller.PersonLocation> peoplesLocations = new List<Board_Controller.PersonLocation>();

            Board_Controller.PersonLocation player1 = new Board_Controller.PersonLocation(Board_Controller.Person.Scarlet, Board_Controller.Room.Billiard);
            Board_Controller.PersonLocation player2 = new Board_Controller.PersonLocation(Board_Controller.Person.Mustard, Board_Controller.Room.Billiard);
            Board_Controller.PersonLocation player3 = new Board_Controller.PersonLocation(Board_Controller.Person.Peacock, Board_Controller.Room.Billiard);
            Board_Controller.PersonLocation player4 = new Board_Controller.PersonLocation(Board_Controller.Person.Plum, Board_Controller.Room.Billiard);
            Board_Controller.PersonLocation player5 = new Board_Controller.PersonLocation(Board_Controller.Person.White, Board_Controller.Room.Study);
            Board_Controller.PersonLocation player6 = new Board_Controller.PersonLocation(Board_Controller.Person.White, Board_Controller.Room.Lounge);
            Board_Controller.PersonLocation player7 = new Board_Controller.PersonLocation(Board_Controller.Person.Peacock, Board_Controller.Room.Conservatory);
            Board_Controller.PersonLocation player8 = new Board_Controller.PersonLocation(Board_Controller.Person.Mustard, Board_Controller.Room.Kitchen);

            peoplesLocations.Add(player1);
            peoplesLocations.Add(player2);
            peoplesLocations.Add(player3);
            peoplesLocations.Add(player4);
            peoplesLocations.Add(player5);
            peoplesLocations.Add(player6);
            peoplesLocations.Add(player7);
            peoplesLocations.Add(player8);

            return peoplesLocations;
        }
    }
}
