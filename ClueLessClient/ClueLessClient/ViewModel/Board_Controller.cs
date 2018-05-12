using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ClueLessClient.Network;

namespace ClueLessClient.ViewModel
{
    class Board_Controller : ObservableObject
    {
        //These enums might make more sense if defined elsewhere
        public enum Person { Scarlet, Mustard, Plum, Peacock, Green, White, None };
        public enum Room { Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen,
            StudyHall_Hallway, HallLounge_Hallway, StudyLibrary_Hallway, HallBilliardRoom_Hallway,
            LoungeDiningRoom_Hallway, LibraryBilliardRoom_Hallway, BilliardRoomDiningRoom_Hallway,
            LibraryConservatory_Hallway, BilliardRoomBallroom_Hallway, DiningRoomKitchen_Hallway,
            ConservatoryBallroom_Hallway, BallroomKitchen_Hallway, None }

        public enum Weapon { Candlestick, Knife, Rope, Revolver, LeadPipe, Wrench, None };

        //Used when receiving info on where people are located from the client
        public struct PersonLocation
        {
            public Person person;
            public Room room;
            public PersonLocation(Person p, Room r)
            {
                person = p;
                room = r;
            }
        }

        //JoinGame WPF Variables
        public ObservableCollection<string> PeopleStrings { get; set; }
        public ObservableCollection<string> GameStrings { get; set; }

        private string _SelectedGame;
        //Update the avaiable character list when the selected game changes
        public string SelectedGame
        {
            get { return _SelectedGame; }
            set
            {
                _SelectedGame = value;
                SelectedGameChanged();
            }
        }
        private string _SelectedPerson;
        public string SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                _SelectedPerson = value;
            }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
        set
            {
                _UserName = value;
            }
        }
        
        //Room contents
        public ObservableCollection<string> StudyOccupants { get; set; }
        public ObservableCollection<string> HallOccupants { get; set; }
        public ObservableCollection<string> LoungeOccupants { get; set; }
        public ObservableCollection<string> LibraryOccupants { get; set; }
        public ObservableCollection<string> BilliardOccupants { get; set; }
        public ObservableCollection<string> DiningOccupants { get; set; }
        public ObservableCollection<string> ConservatoryOccupants { get; set; }
        public ObservableCollection<string> BallroomOccupants { get; set; }
        public ObservableCollection<string> KitchenOccupants { get; set; }

        //Hallway Contents
        public ObservableCollection<string> StudyHall_HallwayOccupant { get; set; }
        public ObservableCollection<string> HallLounge_HallwayOccupant { get; set; }
        public ObservableCollection<string> StudyLibrary_HallwayOccupant { get; set; }
        public ObservableCollection<string> HallBilliardRoom_HallwayOccupant { get; set; }
        public ObservableCollection<string> LoungeDiningRoom_HallwayOccupant { get; set; }
        public ObservableCollection<string> LibraryBilliardRoom_HallwayOccupant { get; set; }
        public ObservableCollection<string> BilliardRoomDiningRoom_HallwayOccupant { get; set; }
        public ObservableCollection<string> LibraryConservatory_HallwayOccupant { get; set; }
        public ObservableCollection<string> BilliardRoomBallroom_HallwayOccupant { get; set; }
        public ObservableCollection<string> DiningRoomKitchen_HallwayOccupant { get; set; }
        public ObservableCollection<string> ConservatoryBallroom_HallwayOccupant { get; set; }
        public ObservableCollection<string> BallroomKitchen_HallwayOccupant { get; set; }

        //Suggest/Accuse Contents
        public ObservableCollection<string> SuggestPeopleStrings { get; set; }
        public ObservableCollection<string> SuggestRoomStrings { get; set; }
        public ObservableCollection<string> SuggestWeaponStrings { get; set; }
        public ObservableCollection<string> AccusePeopleStrings { get; set; }
        public ObservableCollection<string> AccuseRoomStrings { get; set; }
        public ObservableCollection<string> AccuseWeaponStrings { get; set; }
        public string SuggestionPerson { get; set; }
        public string SuggestionRoom { get; set; }
        public string SuggestionWeapon { get; set; }
        public string AccusePerson { get; set; }
        public string AccuseRoom { get; set; }
        public string AccuseWeapon { get; set; }

        public ObservableCollection<string> MyCards { get; set; }

        public string DisproveCard { get; set; }
        public bool DisproveEnabled { get; set; }

        public Dictionary<Room, ObservableCollection<string>> RoomContents { get; set; }

        //Test Client used to try things out
        //Should be replaced once we have a real client
        //TestClient client = new TestClient();
        CluelessServerConnection connect = CluelessServerConnection.getConnection(
                "localhost", 50351);

        //Initialize all variables and set up the starting UI state
        public Board_Controller()
        {
            StudyOccupants = new ObservableCollection<string>();
            HallOccupants = new ObservableCollection<string>();
            LoungeOccupants = new ObservableCollection<string>();
            LibraryOccupants = new ObservableCollection<string>();
            BilliardOccupants = new ObservableCollection<string>();
            DiningOccupants = new ObservableCollection<string>();
            ConservatoryOccupants = new ObservableCollection<string>();
            BallroomOccupants = new ObservableCollection<string>();
            KitchenOccupants = new ObservableCollection<string>();

            StudyHall_HallwayOccupant = new ObservableCollection<string>();
            HallLounge_HallwayOccupant = new ObservableCollection<string>();
            StudyLibrary_HallwayOccupant = new ObservableCollection<string>();
            HallBilliardRoom_HallwayOccupant = new ObservableCollection<string>();
            LoungeDiningRoom_HallwayOccupant = new ObservableCollection<string>();
            LibraryBilliardRoom_HallwayOccupant = new ObservableCollection<string>();
            BilliardRoomDiningRoom_HallwayOccupant = new ObservableCollection<string>();
            LibraryConservatory_HallwayOccupant = new ObservableCollection<string>();
            BilliardRoomBallroom_HallwayOccupant = new ObservableCollection<string>();
            DiningRoomKitchen_HallwayOccupant = new ObservableCollection<string>();
            ConservatoryBallroom_HallwayOccupant = new ObservableCollection<string>();
            BallroomKitchen_HallwayOccupant = new ObservableCollection<string>();


            PeopleStrings = new ObservableCollection<string>();
            GameStrings = new ObservableCollection<string>();
            UserName = "Type User Name";
            RaisePropertyChangedEvent("UserName");
            
            AccusePeopleStrings = new ObservableCollection<string>();
            AccusePeopleStrings.Add("Miss Scarlet");
            AccusePeopleStrings.Add("Col Mustard");
            AccusePeopleStrings.Add("Mrs White");
            AccusePeopleStrings.Add("Mr Green");
            AccusePeopleStrings.Add("Mrs Peacock");
            AccusePeopleStrings.Add("Prof Plum");

            SuggestPeopleStrings = new ObservableCollection<string>();
            SuggestPeopleStrings.Add("Miss Scarlet");
            SuggestPeopleStrings.Add("Col Mustard");
            SuggestPeopleStrings.Add("Mrs White");
            SuggestPeopleStrings.Add("Mr Green");
            SuggestPeopleStrings.Add("Mrs Peacock");
            SuggestPeopleStrings.Add("Prof Plum");

            AccuseWeaponStrings = new ObservableCollection<string>();
            AccuseWeaponStrings.Add("Candlestick");
            AccuseWeaponStrings.Add("Knife");
            AccuseWeaponStrings.Add("Rope");
            AccuseWeaponStrings.Add("Revolver");
            AccuseWeaponStrings.Add("Lead Pipe");
            AccuseWeaponStrings.Add("Wrench");
            SuggestWeaponStrings = new ObservableCollection<string>();
            SuggestWeaponStrings.Add("Candlestick");
            SuggestWeaponStrings.Add("Knife");
            SuggestWeaponStrings.Add("Rope");
            SuggestWeaponStrings.Add("Revolver");
            SuggestWeaponStrings.Add("Lead Pipe");
            SuggestWeaponStrings.Add("Wrench");

            AccuseRoomStrings = new ObservableCollection<string>();
            AccuseRoomStrings.Add("Study");
            AccuseRoomStrings.Add("Hall");
            AccuseRoomStrings.Add("Lounge");
            AccuseRoomStrings.Add("Library");
            AccuseRoomStrings.Add("Billiard Room");
            AccuseRoomStrings.Add("Dining Room");
            AccuseRoomStrings.Add("Conservatory");
            AccuseRoomStrings.Add("Ballroom");
            AccuseRoomStrings.Add("Kitchen");
            SuggestRoomStrings = new ObservableCollection<string>();
            SuggestRoomStrings.Add("Study");
            SuggestRoomStrings.Add("Hall");
            SuggestRoomStrings.Add("Lounge");
            SuggestRoomStrings.Add("Library");
            SuggestRoomStrings.Add("Billiard Room");
            SuggestRoomStrings.Add("Dining Room");
            SuggestRoomStrings.Add("Conservatory");
            SuggestRoomStrings.Add("Ballroom");
            SuggestRoomStrings.Add("Kitchen");

            MyCards = new ObservableCollection<string>();

            DisproveEnabled = false;

            RoomContents = new Dictionary<Room, ObservableCollection<string>>()
            {
                { Room.Study, StudyOccupants },
                { Room.Hall, HallOccupants },
                { Room.Lounge, LoungeOccupants },
                { Room.Library, LibraryOccupants },
                { Room.Billiard, BilliardOccupants },
                { Room.Dining, DiningOccupants },
                { Room.Conservatory, ConservatoryOccupants },
                { Room.Ballroom, BallroomOccupants },
                { Room.Kitchen, KitchenOccupants },
                { Room.StudyHall_Hallway, StudyHall_HallwayOccupant },
                { Room.HallLounge_Hallway, HallLounge_HallwayOccupant },
                { Room.StudyLibrary_Hallway, StudyLibrary_HallwayOccupant },
                { Room.HallBilliardRoom_Hallway, HallBilliardRoom_HallwayOccupant },
                { Room.LoungeDiningRoom_Hallway, LoungeDiningRoom_HallwayOccupant },
                { Room.LibraryBilliardRoom_Hallway, LibraryBilliardRoom_HallwayOccupant },
                { Room.BilliardRoomDiningRoom_Hallway, BilliardRoomDiningRoom_HallwayOccupant },
                { Room.LibraryConservatory_Hallway, LibraryConservatory_HallwayOccupant },
                { Room.BilliardRoomBallroom_Hallway, BilliardRoomBallroom_HallwayOccupant },
                { Room.DiningRoomKitchen_Hallway, DiningRoomKitchen_HallwayOccupant },
                { Room.ConservatoryBallroom_Hallway, ConservatoryBallroom_HallwayOccupant },
                { Room.BallroomKitchen_Hallway, BallroomKitchen_HallwayOccupant }
            };

            /*
            client.MoveEvent += this.HandleMoveEvent;
            client.SuggestionEvent += this.HandleDisprovingSuggestionEvent;
            client.AddGameEvent += this.HandleAddGameEvent;
            client.RemoveGameEvent += this.HandleRemoveGameEvent;
            client.RemoveAvailableCharacterEvent += this.HandleRemoveAvailableCharacterEvent;
            client.GameOverEvent += this.HandleGameOverEvent;

            client.SendTestEvents();*/
        }

        //Moves a person from one location to another location
        void MovePerson(Person person, Room fromHere, Room toHere)
        {
            if(RemovePersonFromRoom(person, fromHere))
            {
                AddPersonToRoom(person, toHere);
            }
            else
            {
                //Can't move someone from one room to another room if they aren't in the room they're coming from!
                MessageBox.Show($"Move Command Failed: " + person.ToString() + " was not in the "+ fromHere);
            }
        }

        //Remove a person from a room
        bool RemovePersonFromRoom(Person person, Room room)
        {
            bool success = false;
            if (RoomContents[room].Contains(person.ToString()))
            {
                success = true;
                RoomContents[room].Remove(person.ToString());
            }
            return success;
        }

        //Adds a person to a room if they weren't already in that roo
        bool AddPersonToRoom(Person person, Room room)
        {
            bool success = false;
            if (!RoomContents[room].Contains(person.ToString()))
            {
                success = true;
                RoomContents[room].Add(person.ToString());
            }
            else
            {
                MessageBox.Show($"Add Person to Room Command Failed: " + person.ToString() + " was already in the " + room);
            }
            return success;
        }

        //Used to build up a hand of cards
        void AddCardToHand(string card)
        {
            if (!MyCards.Contains(card))
            {
                MyCards.Add(card);
            }
            else
            {
                //Can't place 2 of the same card in your hand
                MessageBox.Show($"Add Card Command Failed: " + card + " was already in your hand ");
            }
        }
        
        //Start of the functions usd to interact with the GUI
        //Insert client/game functions into these
        private void MoveUp()
        {
            //client.MoveUp();
            
        }

        private void MoveDown()
        {
            //client.MoveDown();
        }

        private void MoveLeft()
        {
            //client.MoveLeft();
        }

        private void MoveRight()
        {
            //client.MoveRight();
        }

        private void ActivateLoungeSecretPassage()
        {
            //client.ActivateLoungeSecretPassage();
        }

        private void ActivateConservatorySecretPassage()
        {
            //client.ActivateConservatorySecretPassage();
        }

        private void ActivateKitchenSecretPassage()
        {
            //client.ActivateKitchenSecretPassage();
        }


        private void ActivateStudySecretPassage()
        {
            //client.ActivateStudySecretPassage();
        }

        private void MakeSuggestion()
        {
            /*bool success = client.MakeSuggestion(ConvertStringToPerson(SuggestionPerson), 
                                                ConvertStringToRoom(SuggestionRoom), 
                                                ConvertStringToWeapon(SuggestionWeapon));*/

            MessageBox.Show($"You have suggested that " + SuggestionPerson + " killed the victim using the " + SuggestionWeapon + " in the " + SuggestionRoom);
        }

        private void MakeAccusation()
        {
            /*bool success = client.MakeAccusation(ConvertStringToPerson(AccusePerson),
                                                ConvertStringToRoom(AccuseRoom),
                                                ConvertStringToWeapon(AccuseWeapon));*/
            MessageBox.Show($"You have accused " + AccusePerson + " of killing the victim using the " + AccuseWeapon + " in the " + AccuseRoom);
        }

        //Initializes the game
        //May add calls into the client to search/connect to a server
        private void JoinGame()
        {
            connect.registerAsPlayer(UserName);
            List<RequestModels.Lobby> lobbies = connect.Lobbies.GetLobbies();

            foreach (RequestModels.Lobby lobby in lobbies)
            {
                if (lobby.Hostname == SelectedGame)
                {
                    connect.Lobbies.JoinLobby(lobby);

                    connect.registerToGame(lobby);
                }
            }
        }
        //Creates a new game
        //May add calls into the client to search/connect to a server
        private void CreateGame()
        {
            connect.registerAsPlayer(UserName);
            connect.Lobbies.CreateLobby();
            RefreshGameList();
        }

        private void GetGames()
        {
            connect.registerAsPlayer(UserName);
            RefreshGameList();
        }

        private void RefreshGameList()
        {
            List<RequestModels.Lobby> lobbies = connect.Lobbies.GetLobbies();

            GameStrings.Clear();

            foreach (RequestModels.Lobby lobby in lobbies)
            {
                GameStrings.Add(lobby.Hostname);
            }
        }

        private void StartGame()
        {
            if(connect.Lobbies.StartGame())
            {
                MessageBox.Show("Game Started");
            }

            if (connect.Lobbies.WaitForGameStart())
            {
                List<Model.Game.Card> hand = connect.Gameplay.GetPlayerHand();

                foreach (Model.Game.Card card in hand)
                {
                    AddCardToHand(card.cardValue);
                }
            }
           
        }
        // End of the functions used to interact

        //Start of the list of event handlers that take data from the client

        //Start of the list of event handlers that take data from the client
        void HandleAddGameEvent(object sender, EventArgs m)
        {
            EventArgStructures.StringVal input = (EventArgStructures.StringVal)m;
            if (!GameStrings.Contains(input.val))
                GameStrings.Add(input.val);
        }

        //Start of the list of event handlers that take data from the client
        void HandleRemoveGameEvent(object sender, EventArgs m)
        {
            EventArgStructures.StringVal input = (EventArgStructures.StringVal)m;
            if (GameStrings.Contains(input.val))
                GameStrings.Remove(input.val);
        }
        //Start of the list of event handlers that take data from the client
        void HandleRemoveAvailableCharacterEvent(object sender, EventArgs m)
        {
            EventArgStructures.StringVal input = (EventArgStructures.StringVal)m;
            if (PeopleStrings.Contains(input.val))
                PeopleStrings.Remove(input.val);
        }

        void HandleMoveEvent(object sender, EventArgs m)
        {
            EventArgStructures.MoveEventCommand moveData = (EventArgStructures.MoveEventCommand) m;

            MovePerson(moveData.p, moveData.from, moveData.to);
        }

        void HandleGameOverEvent(object sender, EventArgs m)
        {
            EventArgStructures.GameOver gameOverData = (EventArgStructures.GameOver) m;

            MessageBox.Show(gameOverData.winner + "Has won the game! They correctly accused " + gameOverData.p +
                                                    " of killing the victim using the " + gameOverData.w +
                                                    " in the " + gameOverData.r + "!", "Game Over!");
        }

        void HandleDisprovingSuggestionEvent(object sender, EventArgs m)
        {
            EventArgStructures.SuggestionIncomming suggestionData = (EventArgStructures.SuggestionIncomming)m;


            MessageBoxResult result = MessageBox.Show(suggestionData.suggester + "Has accused " + suggestionData.p + 
                                                    " of killing the victim using the " + suggestionData.w + 
                                                    " in the " + suggestionData.r + ". Can you disprove this?", 
                                                    "Suggestion Received", MessageBoxButton.YesNo);
            //If the user says they can disprove the suggestion, enable the disprove button and combobox
            //Added a call to tell the client to stop what it's doing until it receives the disprove info
            //May not be the best idea, but I'm open to suggestions
            if (result == MessageBoxResult.Yes)
             {
                //client.WaitForDisproveInfo();
                DisproveEnabled = true;
                RaisePropertyChangedEvent("DisproveEnabled");
             }
            else
            {
                DisproveEnabled = false;
                RaisePropertyChangedEvent("DisproveEnabled");
            }
        }

        void DisproveSuggestion()
        {
            /*bool success = client.ReceiveDisproval(DisproveCard);

            if (success)
            {
                MessageBox.Show("Suggestion successfully disproven.");
            }
            else
            {
                MessageBox.Show("Failed to disprove suggestion");
            }*/
            DisproveEnabled = false;
            RaisePropertyChangedEvent("DisproveEnabled");
        }

        public void SelectedGameChanged()
        {
            PeopleStrings.Clear();
            if (_SelectedGame != null)
            {
                /*List<string> input = client.GetAvailableCharacters(_SelectedGame);

                foreach (string s in input)
                {
                    PeopleStrings.Add(s);
                }*/
            }
        }

        //Start of the list of commands used to bind to objects in GUI
        public ICommand MoveUpCommand
        {
            get { return new DelegateCommand(MoveUp); }
        }

        public ICommand MoveDownCommand
        {
            get { return new DelegateCommand(MoveDown); }
        }

        public ICommand MoveLeftCommand
        {
            get { return new DelegateCommand(MoveLeft); }
        }

        public ICommand MoveRightCommand
        {
            get { return new DelegateCommand(MoveRight); }
        }

        public ICommand LoungeSecretPassageCommand
        {
            get { return new DelegateCommand(ActivateLoungeSecretPassage); }
        }
        public ICommand ConservatorySecretPassageCommand
        {
            get { return new DelegateCommand(ActivateConservatorySecretPassage); }
        }

        public ICommand KitchenSecretPassageCommand
        {
            get { return new DelegateCommand(ActivateKitchenSecretPassage); }
        }

        public ICommand StudySecretPassageCommand
        {
            get { return new DelegateCommand(ActivateStudySecretPassage); }
        }

        public ICommand MakeSuggestionCommand
        {
            get { return new DelegateCommand(MakeSuggestion); }
        }

        public ICommand MakeAccusationCommand
        {
            get { return new DelegateCommand(MakeAccusation); }
        }

        public ICommand JoinGameCommand
        {
            get { return new DelegateCommand(JoinGame); }
        }

        public ICommand DisproveSuggestionCommand
        {
            get { return new DelegateCommand(DisproveSuggestion); }
        }

        public ICommand CreateGameCommand
        {
            get { return new DelegateCommand(CreateGame); }
        }

        public ICommand GetGamesCommand
        {
            get { return new DelegateCommand(GetGames); }
        }
        
        public ICommand StartGameCommand
        {
            get { return new DelegateCommand(StartGame); }
        }
        //End of command list

        //Switch from string to enum
        //Needed this for some of my GUI -> Enum action
        //Probably not ideal...
        Person ConvertStringToPerson(string personName)
        {
            switch (personName)
            {
                case "Miss Scarlet":
                    return Person.Scarlet;
                case "Col Mustard":
                    return Person.Mustard;
                case "Mrs White":
                    return Person.White;
                case "Mr Green":
                    return Person.Green;
                case "Mrs Peacock":
                    return Person.Peacock;
                case "Prof Plum":
                    return Person.Plum;
                default:
                    return Person.None; //should trigger an error if this comes out...
            }
        }

        //Switch from string to enum
        //Needed this for some of my GUI -> Enum action
        //Probably not ideal...
        Weapon ConvertStringToWeapon(string weaponName)
        {
            switch (weaponName)
            {
                case "Candlestick":
                    return Weapon.Candlestick;
                case "Knife":
                    return Weapon.Knife;
                case "Rope":
                    return Weapon.Rope;
                case "Revolver":
                    return Weapon.Revolver;
                case "Lead Pipe":
                    return Weapon.LeadPipe;
                case "Wrench":
                    return Weapon.Wrench;
                default:
                    return Weapon.None; //should trigger an error if this comes out...
            }
        }

        //Switch from string to enum
        //Needed this for some of my GUI -> Enum action
        //Probably not ideal...
        Room ConvertStringToRoom(string roomName)
        {
            switch (roomName)
            {
                case "Study":
                    return Room.Study;
                case "Hall":
                    return Room.Hall;
                case "Lounge":
                    return Room.Lounge;
                case "Library":
                    return Room.Library;
                case "Billiard Room":
                    return Room.Billiard;
                case "Dining Room":
                    return Room.Dining;
                case "Conservatory":
                    return Room.Conservatory;
                case "Ballroom":
                    return Room.Ballroom;
                case "Kitchen":
                    return Room.Kitchen;
                default:
                    return Room.None;
            }
        }
    }
}
