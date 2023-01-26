namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct Accelerometer : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly sbyte _rawX;

        [FieldOffset(3)]
        private readonly byte _rawX2;

        [FieldOffset(4)]
        private readonly sbyte _rawY;

        [FieldOffset(5)]
        private readonly byte _rawY2;

        [FieldOffset(6)]
        private readonly sbyte _rawZ;

        [FieldOffset(7)]
        private readonly byte _rawZ2;

        private const decimal Resolution = 0.001m;

        public byte Channel => this._channel;

        public byte Type => this._type;

        public decimal X => ((this._rawX << 8) | this._rawX2) * Resolution;
        public decimal Y => ((this._rawY << 8) | this._rawY2) * Resolution;
        public decimal Z => ((this._rawZ << 8) | this._rawZ2) * Resolution;
    }
}