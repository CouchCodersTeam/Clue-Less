
namespace ClueLessClient.Model.Game
{
    /*
     * A class to represent a combination of Room, Suspect, and Weapon.
     * This class can be renamed to include 'CaseFile' usage
     */
    public class Accusation
    {
        public Room room { get; }
        public Suspect suspect { get; }
        public Weapon weapon { get; }

        public Accusation(Room _room, Suspect _suspect, Weapon _weapon)
        {
            this.room = _room;
            this.suspect = _suspect;
            this.weapon = _weapon;
        }
    }
}
