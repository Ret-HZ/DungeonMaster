namespace DungeonFloorLib.Enum
{
    public enum RoomType
    {
        /// <summary>
        /// Nothing.
        /// </summary>
        None,

        /// <summary>
        /// Random room. Can be empty, enemy encounter, loot or rescue.
        /// </summary>
        Random,

        /// <summary>
        /// Hallway.
        /// </summary>
        Hallway,

        /// <summary>
        /// Start room.
        /// </summary>
        Start,

        /// <summary>
        /// Exit room.
        /// </summary>
        Exit,

        /// <summary>
        /// Enemy encounter room.
        /// </summary>
        Encounter,

        /// <summary>
        /// Loot room.
        /// </summary>
        Loot,

        /// <summary>
        /// Room extension. (The Big Swell)
        /// </summary>
        Extension,
    }
}
