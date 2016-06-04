using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.Common
{
    public static class BitHelper
    {
        public static bool[] GetBitArray(byte[] source, int outLength = 0)
        {
            if (outLength == 0) outLength = source.Length*8;
            var outArray = new bool[outLength];

            for (int i = 0; i < outLength; i++)
            {
                var bitIndex = i%8;
                var byteIndex = i/8;
                var isSet = (source[byteIndex] & (1 << bitIndex)) != 0;
                outArray[i] = isSet;
            }

            return outArray;
        }

        public static bool GetBit(byte source, int bitIndex)
        {
            var mask = (1 << bitIndex);
            var bit = (source & (byte)mask) == mask;
            return bit;
        }

        public static byte SetBit(byte source, int bitIndex, bool bitValue)
        {
            var mask = bitValue ? (1 << bitIndex) : ~(1 << bitIndex);
            var newByte = source & (byte)mask;
            return (byte)newByte;
        }

        public static byte ToggleBit(byte source, int bitIndex)
        {
            var mask = (1 << bitIndex);
            var newByte = source ^ (byte)mask;
            return (byte)newByte;
        }
    }
}
