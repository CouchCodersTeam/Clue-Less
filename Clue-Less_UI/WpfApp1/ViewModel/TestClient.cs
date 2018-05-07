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

        //These events can be packed and sent any time to invoke these actions on the UI
        public event EventHandler MoveEvent;
        public event EventHandler SuggestionEvent;

        //Move Up command received from the GUI
        //Should have logic to figure out where to move the main player this client is controlling
        public bool MoveUp()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Scarlet, 
                                                               Board_Controller.Room.Billiard, 
                                                               Board_Controller.Room.HallBilliardRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        //Move Down command received from the GUI
        //Should have logic to figure out where to move the main player this client is controlling
        public bool MoveDown()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Plum,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.BilliardRoomBallroom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        //Move Left command received from the GUI
        //Should have logic to figure out where to move the main player this client is controlling
        public bool MoveLeft()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Mustard,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.LibraryBilliardRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }

        //Move Right command received from the GUI
        //Should have logic to figure out where to move the main player this client is controlling
        public bool MoveRight()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Peacock,
                                                               Board_Controller.Room.Billiard,
                                                               Board_Controller.Room.BilliardRoomDiningRoom_Hallway);
            MoveEvent(this, args);
            return success;
        }


        //This command initiates movement through the Lounge secret passageway
        //Should have logic to pass back the move command with the main player for this client
        public bool ActivateLoungeSecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.White,
                                                               Board_Controller.Room.Lounge,
                                                               Board_Controller.Room.Conservatory);
            MoveEvent(this, args);
            return success;
        }

        //This command initiates movement through the Conservatory secret passageway
        //Should have logic to pass back the move command with the main player for this client
        public bool ActivateConservatorySecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Peacock,
                                                               Board_Controller.Room.Conservatory,
                                                               Board_Controller.Room.Lounge);
            MoveEvent(this, args);
            return success;
        }

        //This command initiates movement through the Kitchen secret passageway
        //Should have logic to pass back the move command with the main player for this client
        public bool ActivateKitchenSecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.Mustard,
                                                               Board_Controller.Room.Kitchen,
                                                               Board_Controller.Room.Study);
            MoveEvent(this, args);
            return success;
        }

        //This command initiates movement through the Study secret passageway
        //Should have logic to pass back the move command with the main player for this client
        public bool ActivateStudySecretPassage()
        {
            bool success = true;
            var args = new EventArgStructures.MoveEventCommand(Board_Controller.Person.White,
                                                               Board_Controller.Room.Study,
                                                               Board_Controller.Room.Kitchen);
            MoveEvent(this, args);
            return success;
        }

        //Send an event to the UI in order to ask player to disprove suggestion
        //The real client should have data coming in from the server that it populates this event with
        public void DisproveSuggestion()
        {

            var args = new EventArgStructures.SuggestionIncomming(Board_Controller.Person.Peacock, 
                                                                  Board_Controller.Person.White,
                                                                  Board_Controller.Room.Study,
                                                                  Board_Controller.Weapon.Knife);
            SuggestionEvent(this, args);
        }

        //If the user says they can disprove the suggestion I thought the client might 
        //want to know so it can put the game on hold until it receives the disproval info
        public void WaitForDisproveInfo()
        {
            //some logic to wait till the UI submits disproval data?
        }

        //Command coming from the UI with the card that the user thinks disproves the suggestion
        //May want to add a pop-up on the UI side that says if it was successfull or not
        public bool ReceiveDisproval(string card)
        {
            //no smarts here, waiting for real code to decide if it was a success
            bool success = false;

            return success;
        }

        //Command coming from the UI that triggers a suggestion
        //I'm guessing this routes through the server to another client and turns into a DisprovesSuggestion() call
        public bool MakeSuggestion(Board_Controller.Person person, Board_Controller.Room room, Board_Controller.Weapon weapon)
        {
            bool success = true;

            return success;
        }
        
        //Command coming from the UI that triggers a suggestion
        //ToDo: create something that tells the UI if the game is over and who won based on an accusation
        public bool MakeAccusation(Board_Controller.Person person, Board_Controller.Room room, Board_Controller.Weapon weapon)
        {
            bool success = true;

            return success;
        }

        //Returns a hand of cards
        //Real code probably randomly deals these
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

        //Returns the initial board state
        //This code just puts palyers into opportune places on the board so I could test the buttons
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
