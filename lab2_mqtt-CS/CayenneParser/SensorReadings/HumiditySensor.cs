namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct HumiditySensor : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly byte _rawHumidity;

        private const decimal Resolution = .5m * (1 / 100m); // Written as .5 % in the docs

        public byte Channel => this._channel;

        public byte Type => this._type;
        public decimal Humidity => this._rawHumidity * Resolution;
    }
}