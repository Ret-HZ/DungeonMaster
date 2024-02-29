using DungeonFloorLib.Enum;
using System.IO;
using Yarhl.IO;

namespace DungeonFloorLib
{
    public static class Util
    {
        internal static bool GetFlag(int flagsValue, int bitPosition)
        {
            return ((1 << bitPosition) & flagsValue) > 0;
        }


        internal static bool GetFlag(int flagsValue, RoomConnection bitPosition)
        {
            return GetFlag(flagsValue, (int)bitPosition);
        }


        internal static void SetFlag(ref int bitmask, int bitPosition, bool boolval)
        {
            if (boolval)
            {
                // Set bit to 1
                bitmask |= 1 << bitPosition;
            }
            else
            {
                // Set bit to 0
                bitmask &= ~(1 << bitPosition);
            }
        }


        internal static void SetFlag(ref int bitmask, RoomConnection bitPosition, bool boolval)
        {
            SetFlag(ref bitmask, (int)bitPosition, boolval);
        }



        ///// EXTENSIONS /////

        /// <summary>
        /// Writes the complete <see cref="DataStream"/> into a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The destination <see cref="Stream"/>.</param>
        internal static void WriteTo(this DataStream ds, Stream stream)
        {
            ds.Seek(0);
            byte[] temp = new byte[ds.Length];
            ds.Read(temp, 0, (int)ds.Length);
            stream.Write(temp, 0, temp.Length);
            stream.Seek(0, SeekOrigin.Begin);
        }


        /// <summary>
        /// Returns the contents of the <see cref="DataStream"/> as a byte array.
        /// </summary>
        internal static byte[] ToArray(this DataStream ds)
        {
            ds.Seek(0);
            byte[] array = new byte[ds.Length];
            ds.Read(array, 0, (int)ds.Length);
            return array;
        }
    }
}
