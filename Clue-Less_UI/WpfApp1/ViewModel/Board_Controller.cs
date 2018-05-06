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
        public enum Person { Scarlet, Mustard, Plum, Peacock, Green, White};
        public enum Room { Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen,
            StudyHall_Hallway, HallLounge_Hallway, StudyLibrary_Hallway, HallBilliardRoom_Hallway,
            LoungeDiningRoom_Hallway, LibraryBilliardRoom_Hallway, BilliardRoomDiningRoom_Hallway,
            LibraryConservatory_Hallway, BilliardRoomBallroom_Hallway, DiningRoomKitchen_Hallway,
            ConservatoryBallroom_Hallway, BallroomKitchen_Hallway}

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
        //End Test Code

    }
}
