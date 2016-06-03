using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Farplane.Common
{
    public static class DataTextSerializer
    {
        public static string Serialize(byte[] dataToSerialize)
        {
            var base64 = Convert.ToBase64String(dataToSerialize);
            return base64;
        }

        public static byte[] Deserialize(string stringToDeserialize)
        {
            var data = Convert.FromBase64String(stringToDeserialize);
            return data;
        }
    }
}
