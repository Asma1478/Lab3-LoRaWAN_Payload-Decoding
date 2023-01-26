namespace Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using SensorReadings;

    public static class SensorReadingParser
    {
        public static IReadOnlyList<ISensorReading> Parse(string hex)
        {
            byte[] bytes = HexToByte.ConvertToByteArray(hex);
            var list = Parse(bytes);
            return list;
        }

        public static IReadOnlyList<ISensorReading> Parse(byte[] bytes)
        {
            
            var list = new List<ISensorReading>();

            for (var i = 0; i < bytes.Length;)
            {
                int toSkip;
                byte type = bytes[i + 1];
                switch (type)
                {
                    case 0x00:
                         
                        list.Add(ByteArrayToStructure<AnalogInput>(bytes, i, out toSkip));

                        break;
                    case 0x01:
                         
                        list.Add(ByteArrayToStructure<DigitalOutput>(bytes, i, out toSkip));

                        break;
                    case 0x02:
                         
                        list.Add(ByteArrayToStructure<AnalogInput>(bytes, i, out toSkip));

                        break;
                    case 0x03: 
                        list.Add(ByteArrayToStructure<AnalogOutput>(bytes, i, out toSkip));

                        break;
                    case 0x65:
                         
                        list.Add(ByteArrayToStructure<IlluminanceSensor>(bytes, i, out toSkip));

                        break;
                    case 0x66:
                         
                        list.Add(ByteArrayToStructure<PresenceSensor>(bytes, i, out toSkip));

                        break;

                    case 0x67:
                         
                        list.Add(ByteArrayToStructure<TemperatureSensor>(bytes, i, out toSkip));

                        break;

                    case 0x68:
                         
                        list.Add(ByteArrayToStructure<HumiditySensor>(bytes, i, out toSkip));

                        break;
                    case 0x71:
                         
                        list.Add(ByteArrayToStructure<Accelerometer>(bytes, i, out toSkip));

                        break;
                    case 0x73:
                         
                        list.Add(ByteArrayToStructure<Barometer>(bytes, i, out toSkip));

                        break;

                    case 0x86:
                         
                        list.Add(ByteArrayToStructure<Gyrometer>(bytes, i, out toSkip));

                        break;
                    case 0x88:
                         
                        list.Add(ByteArrayToStructure<GpsLocation>(bytes, i, out toSkip));

                        break;

                    default:
                        throw new Exception();
                }

                i += toSkip;
            }

            return list;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes, int start, out int length)
            where T : struct
        {

            length = Marshal.SizeOf<T>();

            GCHandle handle = GCHandle.Alloc(new ArraySegment<byte>(bytes, start, length).ToArray(), GCHandleType.Pinned);
            try
            {
                return (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}