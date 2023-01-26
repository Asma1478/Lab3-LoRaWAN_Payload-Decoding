namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct AnalogInput : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly sbyte _rawValue;

        [FieldOffset(3)]
        private readonly byte _rawValue2;

        private const decimal Resolution = .01m;

        public byte Channel => this._channel;

        public byte Type => this._type;
        public decimal Value => ((this._rawValue << 8) | this._rawValue2) * Resolution;
    }
}