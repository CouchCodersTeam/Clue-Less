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
        public enum Room { None, Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen,
            StudyHall_Hallway, HallLounge_Hallway, StudyLibrary_Hallway, HallBilliardRoom_Hallway,
            LoungeDiningRoom_Hallway, LibraryBilliardRoom_Hallway, BilliardRoomDiningRoom_Hallway,
            LibraryConservatory_Hallway, BilliardRoomBallroom_Hallway, DiningRoomKitchen_Hallway,
            ConservatoryBallroom_Hallway, BallroomKitchen_Hallway }

        public enum Weapon { Candlestick, Knife, Rope, Revolver, LeadPipe, Wrench, None };

        public Room[,] Board = new Room[6,6];

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
                { Room.BallroomKitchen_Hallway, BallroomKitchen_HallwayOccupant },
                { Room.None, new ObservableCollection<string>() }
            };

            //Setup board
            Board[0, 0] = Room.Study;
            Board[1, 0] = Room.StudyHall_Hallway;
            Board[2, 0] = Room.Hall;
            Board[3, 0] = Room.HallLounge_Hallway;
            Board[4, 0] = Room.Lounge;

            Board[0, 1] = Room.StudyLibrary_Hallway;
            Board[2, 1] = Room.HallBilliardRoom_Hallway;
            Board[4, 1] = Room.LoungeDiningRoom_Hallway;

            Board[0, 2] = Room.Library;
            Board[1, 2] = Room.LibraryBilliardRoom_Hallway;
            Board[2, 2] = Room.Billiard;
            Board[3, 2] = Room.BilliardRoomDiningRoom_Hallway; 
            Board[4, 2] = Room.Dining;

            Board[0, 3] = Room.LibraryConservatory_Hallway;
            Board[2, 3] = Room.BilliardRoomBallroom_Hallway;
            Board[4, 3] = Room.DiningRoomKitchen_Hallway;

            Board[0, 4] = Room.Conservatory;
            Board[1, 4] = Room.ConservatoryBallroom_Hallway;
            Board[2, 4] = Room.Ballroom;
            Board[3, 4] = Room.BallroomKitchen_Hallway;
            Board[4, 4] = Room.Kitchen;
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
        void MovePerson(string person, Room fromHere, Room toHere)
        {
            if(RemovePersonFromRoom(person, fromHere))
            {
                AddPersonToRoom(person, toHere);
            }
            else
            {
                //Can't move someone from one room to another room if they aren't in the room they're coming from!
                MessageBox.Show($"Move Command Failed: " + person + " was not in the "+ fromHere);
            }
        }

        //Remove a person from a room
        bool RemovePersonFromRoom(string person, Room room)
        {
            bool success = false;
            if (RoomContents[room].Contains(person))
            {
                success = true;
                RoomContents[room].Remove(person);
            }
            return success;
        }

        //Adds a person to a room if they weren't already in that roo
        bool AddPersonToRoom(string person, Room room)
        {
            bool success = false;
            if (room != Room.None)
            {
                if (!RoomContents[room].Contains(person))
                {
                    success = true;
                    RoomContents[room].Add(person);
                }
                else
                {
                    MessageBox.Show($"Add Person to Room Command Failed: " + person.ToString() + " was already in the " + room);
                }
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
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach(Model.Game.Player player in players)
            {
                if(player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);
                    Model.Game.Location newLoc = board.UpFrom(loc);
                    if (loc != newLoc)
                    {
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.MoveUp();
            
        }

        private void MoveDown()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);
                    Model.Game.Location newLoc = board.DownFrom(loc);
                    if (loc != newLoc)
                    {
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.MoveDown();
        }

        private void MoveLeft()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);
                    Model.Game.Location newLoc = board.LeftFrom(loc);
                    if (loc != newLoc)
                    {
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.MoveLeft();
        }

        private void MoveRight()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);
                    Model.Game.Location newLoc = board.RightFrom(loc);
                    if (loc != newLoc)
                    {
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.MoveRight();
        }

        private void ActivateLoungeSecretPassage()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);

                    if (loc.isSecretPassage() && loc.xCoordinate == 4 && loc.yCoordinate == 0)
                    {
                        Model.Game.Location newLoc = board.GetLocation(0, 4);
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.ActivateLoungeSecretPassage();
        }

        private void ActivateConservatorySecretPassage()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);

                    if (loc.isSecretPassage() && loc.xCoordinate == 0 && loc.yCoordinate == 4)
                    {
                        Model.Game.Location newLoc = board.GetLocation(4, 0);
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.ActivateConservatorySecretPassage();
        }

        private void ActivateKitchenSecretPassage()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);

                    if (loc.isSecretPassage() && loc.xCoordinate == 4 && loc.yCoordinate == 4)
                    {
                        Model.Game.Location newLoc = board.GetLocation(0, 0);
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.ActivateKitchenSecretPassage();
        }


        private void ActivateStudySecretPassage()
        {
            List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
            Model.Game.Board board = connect.Gameplay.GetState().getBoard();

            foreach (Model.Game.Player player in players)
            {
                if (player.name == UserName)
                {
                    Model.Game.Location loc = board.GetLocation(player.location.xCoordinate, player.location.yCoordinate);

                    if (loc.isSecretPassage() && loc.xCoordinate == 0 && loc.yCoordinate == 0)
                    {
                        Model.Game.Location newLoc = board.GetLocation(4, 4);
                        if (connect.Gameplay.MovePlayerTo(newLoc))
                        {
                            MovePerson(player.character.ToString(), Board[loc.xCoordinate, loc.yCoordinate], Board[newLoc.xCoordinate, newLoc.yCoordinate]);
                            WaitForCommand();
                        }
                    }
                }
            }
            //client.ActivateStudySecretPassage();
        }

        private void MakeSuggestion()
        {
            MessageBox.Show($"You have suggested that " + SuggestionPerson + " killed the victim using the " + SuggestionWeapon + " in the " + SuggestionRoom);
            Model.Game.Accusation suggestion = new Model.Game.Accusation(ConvertStringToRoom(SuggestionRoom), ConvertStringToPerson(SuggestionPerson), ConvertStringToWeapon(SuggestionWeapon));

            connect.Gameplay.MakeSuggestion(suggestion);
        }

        private void MakeAccusation()
        {
            /*bool success = client.MakeAccusation(ConvertStringToPerson(AccusePerson),
                                                ConvertStringToRoom(AccuseRoom),
                                                ConvertStringToWeapon(AccuseWeapon));*/
            MessageBox.Show($"You have accused " + AccusePerson + " of killing the victim using the " + AccuseWeapon + " in the " + AccuseRoom);
            Model.Game.Accusation accusation = new Model.Game.Accusation(ConvertStringToRoom(AccuseRoom), ConvertStringToPerson(AccusePerson), ConvertStringToWeapon(AccuseWeapon));

            connect.Gameplay.MakeAccusation(accusation);
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
            var lobby = connect.Lobbies.CreateLobby();
            connect.registerToGame(lobby);
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

                ClueLessClient.Model.Game.Game game = connect.Gameplay.GetState();

                List<Model.Game.Player> players = game.getPlayers();
                foreach(Model.Game.Player player in players)
                {
                    AddPersonToRoom(player.character.ToString(), Board[player.location.xCoordinate, player.location.yCoordinate]);
                }

            }
            WaitForCommand();
        }
        // End of the functions used to interact

        //Start of the list of event handlers that take data from the client


        private void WaitForCommand()
        {
            while (true)
            {
                var incCommand = connect.Gameplay.WaitForCommand();
                if (incCommand.command == CommandType.MovePlayer)
                {
                    MoveData data = incCommand.data.moveData;
                    List<Model.Game.Player> players = connect.Gameplay.GetState().getPlayers();
                    
                    foreach (Model.Game.Player player in players)
                    {
                        //this is stupid and I don't care
                        foreach (Room room in Board)
                        {
                            RemovePersonFromRoom(player.character.ToString(), room);
                        }

                        if (player.name == data.playerName)
                        {
                            AddPersonToRoom(player.character.ToString(), Board[player.location.xCoordinate, player.location.yCoordinate]);
                        }
                    }
                }
                else if (incCommand.command == CommandType.TakeTurn)
                {
                    MessageBox.Show("It's your turn");
                    break;
                }
                else if (incCommand.command == CommandType.AccusationMade)
                {
                    AccusationData data = incCommand.data.accusationData;
                    //TODO: Add logic for when this is received
                    MessageBoxResult result = MessageBox.Show(data.playerName + "Has accused " + data.accusation.suspect +
                                                        " of killing the victim using the " + data.accusation.weapon +
                                                        " in the " + data.accusation.room + ". Can you disprove this?",
                                                        "Accusation Received", MessageBoxButton.YesNo);
                }
                else if (incCommand.command == CommandType.DisproveResult)
                {
                    //TODO: Add logic for when this is received
                    DisproveData data = incCommand.data.disproveData;

                    MessageBox.Show("The suggestion was disproven by " + data.disprovingPlayer + " by revealing " + data.card.cardValue);
                }
                else if (incCommand.command == CommandType.SuggestionMade)
                {
                    SuggestionData data = incCommand.data.suggestData;
                    //TODO: Add logic for when this is received
                    MessageBoxResult result = MessageBox.Show(data.playerName + "Has suggested that " + data.accusation.suspect +
                                                        " killed the victim using the " + data.accusation.weapon +
                                                        " in the " + data.accusation.room + ". Can you disprove this?",
                                                        "Suggestion Received", MessageBoxButton.YesNo);
                    //If the user says they can disprove the suggestion, enable the disprove button and combobox
                    //Added a call to tell the client to stop what it's doing until it receives the disprove info
                    //May not be the best idea, but I'm open to suggestions
                    if (result == MessageBoxResult.Yes)
                    {
                        //If the user says they can disprove the suggestion, enable the disprove button and combobox
                        //Added a call to tell the client to stop what it's doing until it receives the disprove info
                        //May not be the best idea, but I'm open to suggestions
                        if (result == MessageBoxResult.Yes)
                        {
                            DisproveEnabled = true;
                            RaisePropertyChangedEvent("DisproveEnabled");
                            break;
                        }
                        else
                        {
                            DisproveEnabled = false;
                            RaisePropertyChangedEvent("DisproveEnabled");
                        }
                    }
                    else if (incCommand.command == CommandType.TurnEnd)
                    {
                        //TODO: Add logic for when this is received
                    }
                    else if (incCommand.command == CommandType.Wait)
                    {
                        //TODO: Add logic for when this is received
                    }
                }
            }
        }
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

            MovePerson(moveData.p, Board[moveData.fromX, moveData.fromY], Board[moveData.toX, moveData.toY]);
        }

        void HandleAddPlayerEvent(object sender, EventArgs m)
        {
            EventArgStructures.AddPlayerEventCommand addData = (EventArgStructures.AddPlayerEventCommand)m;

            AddPersonToRoom(addData.person, Board[addData.x, addData.y]);
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
        Model.Game.Suspect ConvertStringToPerson(string personName)
        {
            switch (personName)
            {
                case "Miss Scarlet":
                    return Model.Game.Suspect.Scarlet;
                case "Col Mustard":
                    return Model.Game.Suspect.Mustard;
                case "Mrs White":
                    return Model.Game.Suspect.White;
                case "Mr Green":
                    return Model.Game.Suspect.Green;
                case "Mrs Peacock":
                    return Model.Game.Suspect.Peacock;
                case "Prof Plum":
                    return Model.Game.Suspect.Plum;
                default:
                    return Model.Game.Suspect.Scarlet; //should trigger an error if this comes out...
            }
        }

        //Switch from string to enum
        //Needed this for some of my GUI -> Enum action
        //Probably not ideal...
        Model.Game.Weapon ConvertStringToWeapon(string weaponName)
        {
            switch (weaponName)
            {
                case "Candlestick":
                    return Model.Game.Weapon.Candlestick;
                case "Knife":
                    return Model.Game.Weapon.Knife;
                case "Rope":
                    return Model.Game.Weapon.Rope;
                case "Revolver":
                    return Model.Game.Weapon.Revolver;
                case "Lead Pipe":
                    return Model.Game.Weapon.Pipe;
                case "Wrench":
                    return Model.Game.Weapon.Wrench;
                default:
                    return Model.Game.Weapon.Candlestick; //should trigger an error if this comes out...
            }
        }

        //Switch from string to enum
        //Needed this for some of my GUI -> Enum action
        //Probably not ideal...
        Model.Game.Room ConvertStringToRoom(string roomName)
        {
            switch (roomName)
            {
                case "Study":
                    return Model.Game.Room.Study;
                case "Hall":
                    return Model.Game.Room.Hall;
                case "Lounge":
                    return Model.Game.Room.Lounge;
                case "Library":
                    return Model.Game.Room.Library;
                case "Billiard Room":
                    return Model.Game.Room.Billiard;
                case "Dining Room":
                    return Model.Game.Room.Dining;
                case "Conservatory":
                    return Model.Game.Room.Conservatory;
                case "Ballroom":
                    return Model.Game.Room.Ballroom;
                case "Kitchen":
                    return Model.Game.Room.Kitchen;
                default:
                    return Model.Game.Room.Study;
            }
        }
    }
}
