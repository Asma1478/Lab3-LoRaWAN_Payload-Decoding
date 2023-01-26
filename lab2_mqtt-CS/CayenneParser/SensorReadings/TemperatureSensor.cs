namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct TemperatureSensor : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly sbyte _rawTemperature;

        [FieldOffset(3)]
        private readonly byte _rawTemperature2;

        private const decimal Resolution = .1m;

        public byte Channel => this._channel;

        public byte Type => this._type;
        public decimal Temperature => (this._rawTemperature << 8 | this._rawTemperature2) * Resolution;
    }
}