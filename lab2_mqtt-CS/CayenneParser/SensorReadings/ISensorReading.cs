namespace Parser.SensorReadings
{
    public interface ISensorReading
    {
        byte Channel { get; }
        byte Type { get; }
    }
}