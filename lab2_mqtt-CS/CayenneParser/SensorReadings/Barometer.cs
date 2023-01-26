namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct Barometer : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly byte _rawhPa;

        [FieldOffset(3)]
        private readonly byte _rawhPa2;

        private const decimal Resolution = 0.1m;

        public byte Channel => this._channel;

        public byte Type => this._type;
        public decimal HPa => ((this._rawhPa << 8) | this._rawhPa2) * Resolution;
    }
}