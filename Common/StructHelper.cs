using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Farplane.FFX.Data;

namespace Farplane.Common
{
    public static class StructHelper
    {
        public static int GetSize<T>()
        {
            return Marshal.SizeOf<T>();
        }

        public static int GetFieldOffset<T>(string fieldName, int baseOffset = 0)
        {
            int structSize = Marshal.SizeOf(typeof(PartyMember));
            int writeOffset = 0;

            // Iterate fields in type, attempt to locate requested field
            foreach (var field in typeof(T).GetFields())
            {
                if (field.Name.ToLower() == fieldName.ToLower())
                    break;

                int fieldSize = 0;

                foreach (var attribute in field.CustomAttributes)
                {
                    try
                    {
                        var sizeConst = attribute.NamedArguments.FirstOrDefault(arg => arg.MemberName == "SizeConst");
                        fieldSize = (int)sizeConst.TypedValue.Value;
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }

                if (fieldSize == 0) fieldSize = Marshal.SizeOf(field.FieldType);

                writeOffset += fieldSize;
            }

            // If reached end of struct, throw exception
            if (writeOffset == structSize) throw new Exception($"Unable to find field: {fieldName}");

            return baseOffset + writeOffset;
        }
    }
}
