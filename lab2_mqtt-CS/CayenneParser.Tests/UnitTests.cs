namespace Parser.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SensorReadings;

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestTemperatureHumidityAnalog()
        {
            /*
            from Python 
            frame.add_temperature(15, 27.2)
            frame.add_humitidy(15, 64.5)
            frame.add_analog_output(15, 0.4)
            b'0f670110 0f6881 0f030028'
            */
            // Data Channel (1b)	Type(1b)	Value(in bytes)
            // manual https://github.com/myDevicesIoT/cayenne-docs/blob/master/docs/LORA.md

            byte[] clpp = { 0x0f, 0x67, 0x01, 0x10, 
                            0x0f, 0x68, 0x81,
                            0x0f, 0x03, 0x00, 0x28};

            IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(clpp);

            Assert.IsInstanceOfType(list[0], typeof(TemperatureSensor));
            Assert.IsInstanceOfType(list[1], typeof(HumiditySensor));
            Assert.IsInstanceOfType(list[2], typeof(AnalogOutput));

            var item0 = (TemperatureSensor)list[0];
            var item1 = (HumiditySensor)list[1];
            var item2 = (AnalogOutput)list[2];

            Assert.AreEqual(15, item0.Channel);
            Assert.AreEqual(0x67, item0.Type);
            Assert.AreEqual(27.2m, item0.Temperature);

            // humidity needs to be divieded with 100 to get %
            Assert.AreEqual(15, item1.Channel);
            Assert.AreEqual(0x68, item1.Type);
            Assert.AreEqual(64.5m/100, item1.Humidity);

            Assert.AreEqual(15, item2.Channel);
            Assert.AreEqual(0x03, item2.Type);
            Assert.AreEqual(0.4m, item2.Value);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string hex = "03 67 01 10 05 67 00 FF".Replace(" ", "");

            IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(hex);

            Assert.IsInstanceOfType(list[0], typeof(TemperatureSensor));
            Assert.IsInstanceOfType(list[1], typeof(TemperatureSensor));

            var item0 = (TemperatureSensor) list[0];
            var item1 = (TemperatureSensor) list[1];

            Assert.AreEqual(3, item0.Channel);
            Assert.AreEqual(0x67, item0.Type);
            Assert.AreEqual(27.2m, item0.Temperature);

            Assert.AreEqual(5, item1.Channel);
            Assert.AreEqual(0x67, item1.Type);
            Assert.AreEqual(25.5m, item1.Temperature);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string hex = "01 67 FF D7".Replace(" ", "");

            IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(hex);

            Assert.IsInstanceOfType(list[0], typeof(TemperatureSensor));

            var item0 = (TemperatureSensor) list[0];
            Assert.AreEqual(1, item0.Channel);
            Assert.AreEqual(0x67, item0.Type);
            Assert.AreEqual(-4.1m, item0.Temperature);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string hex = "06 71 04 D2 FB 2E 00 00".Replace(" ", "");

            IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(hex);

            Assert.IsInstanceOfType(list[0], typeof(Accelerometer));
            var item0 = (Accelerometer) list[0];

            Assert.AreEqual(6, item0.Channel);
            Assert.AreEqual(0x71, item0.Type);
            Assert.AreEqual(1.234m, item0.X);
            Assert.AreEqual(-1.234m, item0.Y);
            Assert.AreEqual(0m, item0.Z);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string hex = "01 88 06 76 5f f2 96 0a 00 03 e8".Replace(" ", "");

            IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(hex);

            Assert.IsInstanceOfType(list[0], typeof(GpsLocation));

            var item0 = (GpsLocation) list[0];

            Assert.AreEqual(1, item0.Channel);
            Assert.AreEqual(0x88, item0.Type);
            Assert.AreEqual(42.3519m, item0.Latitude);
            Assert.AreEqual(-87.9094m, item0.Longitude);
            Assert.AreEqual(10m, item0.Altitude);
        }
    }
}