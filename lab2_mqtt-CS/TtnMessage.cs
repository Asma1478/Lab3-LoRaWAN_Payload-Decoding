using MQTTnet;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace lab2_mqtt_CS
{
    public class TtnMessage
    {
        public DateTime? Timestamp { get; set; }
        public string? DeviceID { get; set; }
        public LoRaMessageV3? RawMessageV3 { get; set; }
        public string? Topic { get; set; }
        public byte[]? Payload { get; set; }

        // for virtual sensor
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Altitude { get; set; }

        /// <summary>
        /// Make TTNMessage and decode the base64 payload 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public TtnMessage(LoRaMessageV3 msg, string topic)
        {
            Timestamp = msg.uplink_message!.settings!.time;
            DeviceID = msg.end_device_ids!.device_id;
            RawMessageV3 = msg;
            Topic = topic;

            if (msg.uplink_message.frm_payload != null)
            {
                Payload = Convert.FromBase64String(msg.uplink_message.frm_payload);
            }
        }

        /// <summary>
        /// Deserialise V3 Messages from TTN 
        /// </summary>
        /// <param name="evt"></param>
        /// <returns></returns>
        public static TtnMessage DeserialiseMessageV3(MqttApplicationMessage evt)
        {
            var _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // allows the plusSigns etc.
                // https://learn.microsoft.com/en-us/dotnet/api/system.text.encodings.web.javascriptencoder.unsaferelaxedjsonescaping
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                //WriteIndented = true
            };
            var text = Encoding.ASCII.GetString(evt.Payload);
            var lora = JsonSerializer.Deserialize<LoRaMessageV3>(text, _jsonSerializerOptions);
            var msg = new TtnMessage(lora!, evt.Topic);
            return msg;
        }
    }
}
