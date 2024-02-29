using DungeonFloorLib.Enum;

namespace DungeonMaster
{
    static class Settings
    {
        /// <summary>
        /// Gets the maximum amount of a room type the format allows.
        /// </summary>
        /// <param name="roomType">The room type to check for.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public static int GetMaxRoomAmount(RoomType roomType) => roomType switch
        {
            RoomType.Random => 9,
            RoomType.Start => 1,
            RoomType.Exit => 1,
            RoomType.Encounter => 8,
            RoomType.Loot => 8,
            _ => -1
        };
    }
}
