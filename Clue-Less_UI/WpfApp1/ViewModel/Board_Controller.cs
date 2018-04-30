using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    class Board_Controller
    {
        //This should probably be somewhere else
        public enum Person { Scarlet, Mustard, Plum, Peacock, Green, White};
        public enum Room { Study, Hall, Lounge, Library, Billiard, Dining, Conservatory, Ballroom, Kitchen }

        public ObservableCollection<string> StudyOccupants { get; set; }
        public ObservableCollection<string> HallOccupants { get; set; }
        public ObservableCollection<string> LoungeOccupants { get; set; }
        public ObservableCollection<string> LibraryOccupants { get; set; }
        public ObservableCollection<string> BilliardOccupants { get; set; }
        public ObservableCollection<string> DiningOccupants { get; set; }
        public ObservableCollection<string> ConservatoryOccupants { get; set; }
        public ObservableCollection<string> BallroomOccupants { get; set; }
        public ObservableCollection<string> KitchenOccupants { get; set; }

        public Dictionary<Room, ObservableCollection<string>> RoomContents { get; set; }

        //Just using this as a testing ground for now.
        //Eventually the Client should control this class and use MovePerson etc.
        public Board_Controller()
        {
            StudyOccupants = new ObservableCollection<string>
            {
                "Study"
            };
            HallOccupants = new ObservableCollection<string>
            {
                "Hall"
            };
            LoungeOccupants = new ObservableCollection<string>
            {
                "Lounge"
            };
            LibraryOccupants = new ObservableCollection<string>
            {
                "Library"
            };
            BilliardOccupants = new ObservableCollection<string>
            {
                "Billiard"
            };
            DiningOccupants = new ObservableCollection<string>
            {
                "Dining"
            };
            ConservatoryOccupants = new ObservableCollection<string>
            {
                "Conservatory"
            };
            BallroomOccupants = new ObservableCollection<string>
            {
                "Ballroom"
            };
            KitchenOccupants = new ObservableCollection<string>
            {
                "Kitchen"
            };

            RoomContents = new Dictionary<Room, ObservableCollection<string>>()
            {
                {Room.Study, StudyOccupants},
                {Room.Hall, HallOccupants},
                {Room.Lounge, LoungeOccupants},
                {Room.Library, LibraryOccupants},
                {Room.Billiard, BilliardOccupants},
                {Room.Dining, DiningOccupants},
                {Room.Conservatory, ConservatoryOccupants},
                {Room.Ballroom, BallroomOccupants},
                {Room.Kitchen, KitchenOccupants}
            };
            

            AddPersonToRoom(Person.Scarlet, Room.Study);
            
            //MovePerson(Person.Scarlet, Room.Study, Room.Hall);
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
        
    }
}
