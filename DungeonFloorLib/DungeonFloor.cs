using DungeonFloorLib.Enum;
using System.Collections.Generic;

namespace DungeonFloorLib
{
    public class DungeonFloor
    {
        /// <summary>
        /// Format version.
        /// </summary>
        public int Version { get; internal set; }

        /// <summary>
        /// List of rooms that comprise a floor.
        /// </summary>
        public List<DungeonRoom> Rooms = new List<DungeonRoom>();



        /// <summary>
        /// Returns de amount of rooms of the specified type that are present in the floor.
        /// </summary>
        /// <param name="roomType">The type to check for.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public int GetRoomTypeAmount(RoomType roomType)
        {
            int amount = 0;
            foreach (DungeonRoom room in Rooms)
            {
                if (room.Type == roomType) amount++;
            }
            return amount;
        }
    }
}
