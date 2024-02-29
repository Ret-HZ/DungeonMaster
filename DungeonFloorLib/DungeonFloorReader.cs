using DungeonFloorLib.Enum;
using System;
using System.IO;
using System.Text;
using Yarhl.IO;

namespace DungeonFloorLib
{
    public class DungeonFloorReader
    {
        /// <summary>
        /// Reads a DGRF file.
        /// </summary>
        /// <param name="datastream">The DGRF file as <see cref="DataStream"/>.</param>
        /// <returns>A <see cref="DungeonFloor"/> object.</returns>
        public static DungeonFloor ReadDGRF(DataStream datastream)
        {
            var reader = new DataReader(datastream)
            {
                Endianness = EndiannessMode.LittleEndian,
                DefaultEncoding = Encoding.UTF8,
            };

            DungeonFloor dgrf = new DungeonFloor();

            if (reader.ReadString(4) != "DGRF")
                throw new Exception("Wrong magic. Expected DGRF");

            reader.Stream.Seek(0x8);

            dgrf.Version = reader.ReadInt32();

            reader.Stream.Seek(0x10);

            // 31x31 grid
            for(int i = 0; i < 961; i++)
            {
                DungeonRoom room = new DungeonRoom();
                room.Type = (RoomType)reader.ReadInt32();
                room.ConnectionFlags = reader.ReadInt32();

                dgrf.Rooms.Add(room);
            }

            return dgrf;
        }


        /// <summary>
        /// Reads a DGRF file.
        /// </summary>
        /// <param name="fileBytes">The DGRF file as byte array.</param>
        /// <param name="offset">The location in the array to start reading data from.</param>
        /// <param name="length">The number of bytes to read from the array.</param>
        /// <returns>A <see cref="DungeonFloor"/> object.</returns>
        public static DungeonFloor ReadDGRF(byte[] fileBytes, int offset = 0, int length = 0)
        {
            if (length == 0) length = fileBytes.Length;
            using (var datastream = DataStreamFactory.FromArray(fileBytes, offset, length))
            {
                return ReadDGRF(datastream);
            }
        }


        /// <summary>
        /// Reads a DGRF file.
        /// </summary>
        /// <param name="stream">The DGRF file as <see cref="Stream"/>.</param>
        /// <returns>A <see cref="DungeonFloor"/> object.</returns>
        public static DungeonFloor ReadDGRF(Stream stream)
        {
            using (var datastream = DataStreamFactory.FromStream(stream))
            {
                return ReadDGRF(datastream);
            }
        }


        /// <summary>
        /// Reads a DGRF file.
        /// </summary>
        /// <param name="path">The path to the DGRF file.</param>
        /// <returns>A <see cref="DungeonFloor"/> object.</returns>
        public static DungeonFloor ReadDGRF(string path)
        {
            using (var datastream = DataStreamFactory.FromFile(path, FileOpenMode.Read))
            {
                return ReadDGRF(datastream);
            }
        }
    }
}
