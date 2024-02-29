using System.IO;
using Yarhl.IO;

namespace DungeonFloorLib
{
    public class DungeonFloorWriter
    {
        /// <summary>
        /// Writes a <see cref="DungeonFloor"/> to a <see cref="DataStream"/>.
        /// </summary>
        /// <param name="dungeonFloor">The <see cref="DungeonFloor"/> to write.</param>
        /// <param name="datastream">The <see cref="DataStream"/> to write to.</param>
        private static void WriteDGRF(DungeonFloor dungeonFloor, DataStream datastream)
        {
            var writer = new DataWriter(datastream)
            {
                Endianness = EndiannessMode.LittleEndian,
                DefaultEncoding = System.Text.Encoding.UTF8,
            };

            writer.Write("DGRF", false);
            writer.Write(0);
            writer.Write(dungeonFloor.Version);
            writer.Write(0);

            foreach(DungeonRoom room in dungeonFloor.Rooms)
            {
                writer.Write((int)room.Type);
                writer.Write(room.ConnectionFlags);
            }
        }


        /// <summary>
        /// Writes a <see cref="DungeonFloor"/> to a file.
        /// </summary>
        /// <param name="dungeonFloor">The <see cref="DungeonFloor"/> to write.</param>
        /// <param name="path">The destination file path.</param>
        public static void WriteDGRFToFile(DungeonFloor dungeonFloor, string path)
        {
            using (var datastream = DataStreamFactory.FromFile(path, FileOpenMode.Write))
            {
                WriteDGRF(dungeonFloor, datastream);
            }
        }


        /// <summary>
        /// Writes a <see cref="DungeonFloor"/> to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="dungeonFloor">The <see cref="DungeonFloor"/> to write.</param>
        public static Stream WriteDGRFToStream(DungeonFloor dungeonFloor)
        {
            MemoryStream stream = new MemoryStream();
            DataStream tempds = DataStreamFactory.FromMemory();
            WriteDGRF(dungeonFloor, tempds);
            tempds.WriteTo(stream);
            return stream;
        }


        /// <summary>
        /// Writes a <see cref="DungeonFloor"/> to a byte array.
        /// </summary>
        /// <param name="dungeonFloor">The <see cref="DungeonFloor"/> to write.</param>
        public static byte[] WriteDGRFToArray(DungeonFloor dungeonFloor)
        {
            DataStream tempds = DataStreamFactory.FromMemory();
            WriteDGRF(dungeonFloor, tempds);
            return tempds.ToArray();
        }
    }
}
