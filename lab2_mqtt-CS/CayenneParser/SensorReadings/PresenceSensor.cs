namespace Parser.SensorReadings
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    public struct PresenceSensor : ISensorReading
    {
        [FieldOffset(0)]
        private readonly byte _channel;

        [FieldOffset(1)]
        private readonly byte _type;

        [FieldOffset(2)]
        private readonly byte _present;

        public byte Channel => this._channel;

        public byte Type => this._type;
        public bool Present => this._present != 0x00;
    }
}