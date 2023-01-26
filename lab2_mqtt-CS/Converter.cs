using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_mqtt_CS
{
    public class Converter
    {

        public static string ConvertHexToAscii(string hex)
        {
            string hexStr = new string((from c in hex
                                        where char.IsLetterOrDigit(c)
                                        select c).ToArray());

            if (hexStr == "" || hexStr == null)
            {
                throw new Exception();
            }

            if (hexStr.Length % 2 == 0)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hexStr.Length; i += 2)
                {
                    string hs = hexStr.Substring(i, 2);
                    sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
                }
                return sb.ToString();
            }
            else
            {
                throw new Exception();
            }

        }
        
        public static float FloatIEE754(byte[] b)
        {
            if (b == null )
            {
                throw new NullReferenceException();
            }

            if (b.Length != 4)
            {
                throw new Exception();
            }
            // converts the 4 bytes in the array to a single integer value by shifting the values of b[3], b[2], b[1],
            //and b[0] left by 24, 16, 8, and 0 bits respectively and then performing a bitwise OR operation on the resulting values.
            int bits = (b[3] << 24) | (b[2] << 16) | (b[1] << 8) | b[0];
            return BitConverter.ToSingle(BitConverter.GetBytes(bits), 0);
        }


        public static byte[] ByteSwapX4(byte[] b)
        {
            if (b == null)
            {
                throw new NullReferenceException();
            }

            if (b.Length != 4 || b == null)
            {
                throw new Exception();
            }

            return new byte[] { b[3], b[2], b[1], b[0] };
        }
    }
}
