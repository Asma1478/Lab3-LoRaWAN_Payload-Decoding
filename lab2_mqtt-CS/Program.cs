using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using System.Configuration;
using System.Text;
using lab2_mqtt_CS;
using Parser.SensorReadings;
using Parser;

namespace lab2_mqtt_CS
{
    public class Program
    {
        private static readonly bool CONTAINER = false;

        /// <summary>
        /// MQTTnet usage: https://blog.behroozbc.ir/mqtt-client-with-mqttnet-4-and-c
        /// </summary>
        static async Task Main(string[] args)
        {
            Console.WriteLine($"{Environment.NewLine}" +
                $"MQTTnet ConsoleApp - A The Things Network V3 C# App {Environment.NewLine}");

            var TTN_APP_ID = "demo-ap-la";
            var TTN_API_KEY = "NNSXS.YRCUBFYUK35J2MB6KBRVFCT3ST4IZLDUWGA6UII.6372L5SZNTYYYDQCOPQ777KRMIYS73YO2GQR5JCSKPYLMZDP5R6Q";
            var TTN_REGION = "eu1";
            var TTN_BROKER = $"{TTN_REGION}.cloud.thethings.network";
            var TOPIC = "v3/+/devices/+/up";

            IManagedMqttClient _mqttClient = new MqttFactory().CreateManagedMqttClient();

            // Create client options object with keep alive 1 sec
            var builder = new MqttClientOptionsBuilder()
                            .WithTcpServer(TTN_BROKER, 1883)
                            .WithCredentials(TTN_APP_ID, TTN_API_KEY)
                            .WithCleanSession(true)
                            .WithKeepAlivePeriod(TimeSpan.FromSeconds(1));

            // auto reconnect after 5 sec if disconnected
            var options = new ManagedMqttClientOptionsBuilder()
                   .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                   .WithClientOptions(builder.Build())
                   .Build();

            // Set up handlers
            _mqttClient.ApplicationMessageReceivedAsync += MqttOnNewMessageAsync3;
            _mqttClient.ConnectedAsync += MqttOnConnectedAsync;
            _mqttClient.DisconnectedAsync += MqttOnDisconnectedAsync;
            _mqttClient.ConnectingFailedAsync += MqttConnectingFailedAsync;

            var topics = new List<MqttTopicFilter>();
            var opts = new MqttTopicFilterBuilder()
                .WithTopic(TOPIC)
                .Build();
            topics.Add(opts);
            await _mqttClient.SubscribeAsync(topics);
            await _mqttClient.StartAsync(options);

            if (CONTAINER)
            {
                // use for testing when running as container
                Thread.Sleep(Timeout.Infinite);
            } 
            else
            {
                Console.WriteLine("Press return to exit!");
                Console.ReadLine();
                Console.WriteLine("\nAloha, Goodbye, Vaarwel!");
                Thread.Sleep(1000);
            }
        }
        public static Task MqttOnNewMessageAsync3(MqttApplicationMessageReceivedEventArgs eArg)
        {
            var obj = eArg.ApplicationMessage;
            var ttn = TtnMessage.DeserialiseMessageV3(obj);
            var data = ttn.Payload != null ? BitConverter.ToString(ttn.Payload) : string.Empty;

            Console.WriteLine($"\nTimestamp: {ttn.Timestamp} \nDevice: {ttn.DeviceID} \nTopic: {ttn.Topic} \nPayload: {data}");
            //Console.WriteLine($"Payload decoded: {HexToByte.ConvertToByteArray(data)}\n");
            try
            {
                byte[] clpp = obj.Payload != null ? obj.Payload : null;
                if (clpp != null)
                {
                    data = data.Replace("-", "");
                    IReadOnlyList<ISensorReading> list = SensorReadingParser.Parse(data);
                    var item0 = (TemperatureSensor)list[0];
                    var item1 = (HumiditySensor)list[1];
                    var item2 = (AnalogOutput)list[2];
                    var item3 = (GpsLocation)list[3];
                    Console.WriteLine($"CayenneLpp payload decoded into Temperature: {item0.Temperature} Humidity: {item1.Humidity}, Led: {item2.Value}");
                    Console.WriteLine($"Device loaction is Latitude: {item3.Latitude}, Longitude: {item3.Longitude}, Altitude: {item3.Altitude}");

                }
                else
                {
                    Console.WriteLine($"CayenneLpp is not possible to Decode!");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public static Task MqttOnNewMessageAsync2(MqttApplicationMessageReceivedEventArgs eArg)
        {
            var obj = eArg.ApplicationMessage;
            var ttn = TtnMessage.DeserialiseMessageV3(obj);
            var data = ttn.Payload != null ? BitConverter.ToString(ttn.Payload) : string.Empty;

            Console.WriteLine($"\nTimestamp: {ttn.Timestamp} \nDevice: {ttn.DeviceID} \nTopic: {ttn.Topic} \nPayload: {data}");
            //Console.WriteLine($"Payload decoded: {ConvertHexToAscii(data)}\n");

            try
            {
                var btemp = ttn.Payload != null ? ttn.Payload[0..4] : null;
                var bhum = ttn.Payload != null ? ttn.Payload[4..8] : null;
                var bled = ttn.Payload != null ? ttn.Payload[8..12] : null;

                if (btemp != null && bhum != null && bled != null)
                {
                    float temp = Converter.FloatIEE754(Converter.ByteSwapX4(btemp));
                    float hum = Converter.FloatIEE754(Converter.ByteSwapX4(bhum));
                    float led = Converter.FloatIEE754(Converter.ByteSwapX4(bled));
                    Console.WriteLine("-- Using Float IEE 754 --");
                    Console.WriteLine($"Payload decoded: Temp: {temp}, Hum: {hum} , LED: {led}\n");

                }
                else
                {
                    Console.WriteLine($"Payload is not possible to convert!");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return Task.CompletedTask;
        }

        public static Task MqttOnNewMessageAsync1(MqttApplicationMessageReceivedEventArgs eArg)
        {
            var obj = eArg.ApplicationMessage;
            var ttn = TtnMessage.DeserialiseMessageV3(obj);
            var data = ttn.Payload != null ? BitConverter.ToString(ttn.Payload) : string.Empty;

            Console.WriteLine($"\nTimestamp: {ttn.Timestamp} \nDevice: {ttn.DeviceID} \nTopic: {ttn.Topic} \nPayload: {data}");
            Console.WriteLine($"Payload decoded: {Converter.ConvertHexToAscii(data)}\n");

            return Task.CompletedTask;
        }

        private static Task MqttOnConnectedAsync(MqttClientConnectedEventArgs eArg)
        {
            Console.WriteLine($"MQTTnet Client -> Connected with result: {eArg.ConnectResult.ResultCode}");
            return Task.CompletedTask;
        }
        private static Task MqttOnDisconnectedAsync(MqttClientDisconnectedEventArgs eArg)
        {
            Console.WriteLine($"MQTTnet Client -> Connection lost! Reason: {eArg.Reason}");
            return Task.CompletedTask;
        }
        private static Task MqttConnectingFailedAsync(ConnectingFailedEventArgs eArg)
        {
            Console.WriteLine($"MQTTnet Client -> Connection failed! Reason: {eArg.Exception}");
            return Task.CompletedTask;
        }
     

      


    }



}
