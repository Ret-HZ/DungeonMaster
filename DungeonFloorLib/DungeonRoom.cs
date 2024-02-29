using DungeonFloorLib.Enum;

namespace DungeonFloorLib
{
    public class DungeonRoom
    {
        /// <summary>
        /// The type of room. It will determine the contents.
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// Bitmask for room connections.
        /// </summary>
        internal int ConnectionFlags;

        /// <summary>
        /// Does the room have a connection hallway on the South?
        /// </summary>
        public bool IsConnectSouth
        {
            get { return Util.GetFlag(ConnectionFlags, RoomConnection.South); }
            set { Util.SetFlag(ref ConnectionFlags, RoomConnection.South, value); }
        }

        /// <summary>
        /// Does the room have a connection hallway on the West?
        /// </summary>
        public bool IsConnectWest
        {
            get { return Util.GetFlag(ConnectionFlags, RoomConnection.West); }
            set { Util.SetFlag(ref ConnectionFlags, RoomConnection.West, value); }
        }

        /// <summary>
        /// Does the room have a connection hallway on the North?
        /// </summary>
        public bool IsConnectNorth
        {
            get { return Util.GetFlag(ConnectionFlags, RoomConnection.North); }
            set { Util.SetFlag(ref ConnectionFlags, RoomConnection.North, value); }
        }

        /// <summary>
        /// Does the room have a connection hallway on the East?
        /// </summary>
        public bool IsConnectEast
        {
            get { return Util.GetFlag(ConnectionFlags, RoomConnection.East); }
            set { Util.SetFlag(ref ConnectionFlags, RoomConnection.East, value); }
        }



        /// <summary>
        /// Sets the room connection flag for the specified cardinal.
        /// </summary>
        /// <param name="connection">The cardinal to set.</param>
        /// <param name="value">The value to set the flag to.</param>
        public void SetConnection(RoomConnection connection, bool value)
        {
            Util.SetFlag(ref ConnectionFlags, connection, value);
        }


        /// <summary>
        /// Gets the room connection flag for the specified cardinal.
        /// </summary>
        /// <param name="connection">The cardinal flag to get.</param>
        /// <returns>A <see cref="bool"/>.</returns>
        public bool GetConnection(RoomConnection connection)
        {
            return Util.GetFlag(ConnectionFlags, connection);
        }
    }
}
