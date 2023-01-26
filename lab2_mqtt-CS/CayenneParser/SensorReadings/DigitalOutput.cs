namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct DigitalOutput : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly byte _rawValue;

        public byte Channel => this._channel;

        public byte Type => this._type;
        public byte Value => this._rawValue;
    }
}