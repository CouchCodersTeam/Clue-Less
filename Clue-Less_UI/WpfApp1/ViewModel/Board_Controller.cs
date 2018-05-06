using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    class Board_Controller : ObservableObject
    {
        //This should probably be somewhere else
        public enum Person { Scarlet, Mustard, Plum, Peacock, Green, White, None};
        public enum Room { Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen,
            StudyHall_Hallway, HallLounge_Hallway, StudyLibrary_Hallway, HallBilliardRoom_Hallway,
            LoungeDiningRoom_Hallway, LibraryBilliardRoom_Hallway, BilliardRoomDiningRoom_Hallway,
            LibraryConservatory_Hallway, BilliardRoomBallroom_Hallway, DiningRoomKitchen_Hallway,
            ConservatoryBallroom_Hallway, BallroomKitchen_Hallway, None}

        public enum Weapon { Candlestick, Knife, Rope, Revolver, LeadPipe, Wrench, None};

        public ObservableCollection<string> StudyOccupants { get; set; }
        public ObservableCollection<string> HallOccupants { get; set; }
        public ObservableCollection<string> LoungeOccupants { get; set; }
        public ObservableCollection<string> LibraryOccupants { get; set; }
        public ObservableCollection<string> BilliardOccupants { get; set; }
        public ObservableCollection<string> DiningOccupants { get; set; }
        public ObservableCollection<string> ConservatoryOccupants { get; set; }
        public ObservableCollection<string> BallroomOccupants { get; set; }
        public ObservableCollection<string> KitchenOccupants { get; set; }

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

        public Dictionary<Room, ObservableCollection<string>> RoomContents { get; set; }

        //Just using this as a testing ground for now.
        //Eventually the Client should control this class and use MovePerson etc.
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
            

            AddPersonToRoom(Person.Scarlet, Room.Billiard);
            AddPersonToRoom(Person.Mustard, Room.Billiard);
            AddPersonToRoom(Person.Peacock, Room.Billiard);
            AddPersonToRoom(Person.Plum, Room.Billiard);
            AddPersonToRoom(Person.White, Room.Study);
            AddPersonToRoom(Person.Green, Room.Study);
            AddPersonToRoom(Person.Peacock, Room.Conservatory);
            AddPersonToRoom(Person.Mustard, Room.Kitchen);
            AddPersonToRoom(Person.White, Room.Lounge);
            AddPersonToRoom(Person.Green, Room.Lounge);
         
            MovePerson(Person.Scarlet, Room.Study, Room.Hall);
        }

        public void MovePerson(Person person, Room fromHere, Room toHere)
        {
            RemovePersonFromRoom(person, fromHere);
            AddPersonToRoom(person, toHere);
        }

        void RemovePersonFromRoom(Person person, Room room)
        {
            if (RoomContents[room].Contains(person.ToString()))
                RoomContents[room].Remove(person.ToString());
        }

        void AddPersonToRoom(Person person, Room room)
        {
            if(!RoomContents[room].Contains(person.ToString()))
                RoomContents[room].Add(person.ToString());
        }

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

        Room ConvertStringToRoom(string roomName)
        {
            switch(roomName)
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

        //Test Client Functions
        TestClient client = new TestClient();

        public ICommand MoveUpCommand
        {
            get { return new DelegateCommand(MoveUp); }
        }

        private void MoveUp()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Scarlet, Room.Billiard, Room.HallBilliardRoom_Hallway);
        }

        public ICommand MoveDownCommand
        {
            get { return new DelegateCommand(MoveDown); }
        }

        private void MoveDown()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Plum, Room.Billiard, Room.BilliardRoomBallroom_Hallway);
        }

        public ICommand MoveLeftCommand
        {
            get { return new DelegateCommand(MoveLeft); }
        }

        private void MoveLeft()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Mustard, Room.Billiard, Room.LibraryBilliardRoom_Hallway);
        }

        public ICommand MoveRightCommand
        {
            get { return new DelegateCommand(MoveRight); }
        }

        private void MoveRight()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Peacock, Room.Billiard, Room.BilliardRoomDiningRoom_Hallway);
        }

        public ICommand LoungeSecretPassageCommand
        {
            get { return new DelegateCommand(ActivateLoungeSecretPassage); }
        }

        private void ActivateLoungeSecretPassage()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.White, Room.Lounge, Room.Conservatory);
        }

        public ICommand ConservatorySecretPassageCommand
        {
            get { return new DelegateCommand(ActivateConservatorySecretPassage); }
        }

        private void ActivateConservatorySecretPassage()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Peacock, Room.Conservatory, Room.Lounge);
        }

        public ICommand KitchenSecretPassageCommand
        {
            get { return new DelegateCommand(ActivateKitchenSecretPassage); }
        }

        private void ActivateKitchenSecretPassage()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.Mustard, Room.Kitchen, Room.Study);
        }

        public ICommand StudySecretPassageCommand
        {
            get { return new DelegateCommand(ActivateStudySecretPassage); }
        }

        private void ActivateStudySecretPassage()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well
            MovePerson(Person.White, Room.Study, Room.Kitchen);
        }

        public ICommand MakeSuggestionCommand
        {
            get { return new DelegateCommand(MakeSuggestion); }
        }
       
        private void MakeSuggestion()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well

            bool success = client.MakeSuggestion(ConvertStringToPerson(SuggestionPerson), 
                                                ConvertStringToRoom(SuggestionRoom), 
                                                ConvertStringToWeapon(SuggestionWeapon));
        }

        public ICommand MakeAccusationCommand
        {
            get { return new DelegateCommand(MakeAccusation); }
        }

        private void MakeAccusation()
        {
            //insert game logic to determine how to use this function
            //MovePerson(Person person, Room fromHere, Room toHere)
            //pass call to client as well

            bool success = client.MakeAccusation(ConvertStringToPerson(AccusePerson),
                                                ConvertStringToRoom(AccuseRoom),
                                                ConvertStringToWeapon(AccuseWeapon));
        }
        //End Test Code

    }
}
