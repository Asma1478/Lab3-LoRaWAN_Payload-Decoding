namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct GpsLocation : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly sbyte _rawLatitude;

        [FieldOffset(3)]
        private readonly byte _rawLatitude2;

        [FieldOffset(4)]
        private readonly byte _rawLatitude3;

        [FieldOffset(5)]
        private readonly sbyte _rawLongitude;

        [FieldOffset(6)]
        private readonly byte _rawLongitude2;

        [FieldOffset(7)]
        private readonly byte _rawLongitude3;

        [FieldOffset(8)]
        private readonly sbyte _rawAltitude;

        [FieldOffset(9)]
        private readonly byte _rawAltitude2;

        [FieldOffset(10)]
        private readonly byte _rawAltitude3;

        private const decimal ResolutionLatitudeAndLongitude = 0.0001m;
        private const decimal ResolutionAltitude = 0.01m;

        public byte Channel => this._channel;

        public byte Type => this._type;

        public decimal Latitude => ((this._rawLatitude << 16) | (this._rawLatitude2 << 8) | this._rawLatitude3) * ResolutionLatitudeAndLongitude;
        public decimal Longitude => ((this._rawLongitude << 16) | (this._rawLongitude2 << 8) | this._rawLongitude3) * ResolutionLatitudeAndLongitude;
        public decimal Altitude => ((this._rawAltitude << 16) | (this._rawAltitude2 << 8) | this._rawAltitude3) * ResolutionAltitude;
    }
}