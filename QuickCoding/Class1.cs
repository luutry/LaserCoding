using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuickCoding
{
    class Class1
    {
        public static string StringFromUShortArray(ushort[] reg)
        {
            if (reg == null || reg.Length == 0)
                return null;
            else
            {
                List<ushort> list = reg.TakeWhile<ushort>(x => x != 0).ToList<ushort>();
                byte[] bytes = new byte[list.Count];
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = (byte)list[i];
                return Encoding.GetEncoding("GBK").GetString(bytes);
            }
        }
    }
}
