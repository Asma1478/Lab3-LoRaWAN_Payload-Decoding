using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_mqtt_CS
{
    public class ApplicationIds
    {
        public string? application_id { get; set; }
    }

    public class EndDeviceIds
    {
        public string? device_id { get; set; }
        public ApplicationIds? application_ids { get; set; }
        public string? dev_eui { get; set; }
        public string? join_eui { get; set; }
        public string? dev_addr { get; set; }
    }

    public class DecodedPayload
    {
        // common
        public int humidity { get; set; }
        public double temperature { get; set; }
        public int vdd { get; set; }
        // ELT specific
        public int accMotion { get; set; }
        public double externalTemperature { get; set; }
        public double pressure { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        // ERS specific
        public int co2 { get; set; }
        public int light { get; set; }
        public int motion { get; set; }
    }

    public class GatewayIds
    {
        public string? gateway_id { get; set; }
        public string? eui { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int altitude { get; set; }
        public string? source { get; set; }
    }

    public class RxMetadata
    {
        public GatewayIds? gateway_ids { get; set; }
        public DateTime? time { get; set; }
        public long timestamp { get; set; }
        public int rssi { get; set; }
        public int channel_rssi { get; set; }
        public float snr { get; set; }
        public Location? location { get; set; }
        public string? uplink_token { get; set; }
        public int channel_index { get; set; }
    }

    public class Lora
    {
        public int bandwidth { get; set; }
        public int spreading_factor { get; set; }
    }

    public class DataRate
    {
        public Lora? lora { get; set; }
    }

    public class Settings
    {
        public DataRate? data_rate { get; set; }
        public int data_rate_index { get; set; }
        public string? coding_rate { get; set; }
        public string? frequency { get; set; }
        public long timestamp { get; set; }
        public DateTime? time { get; set; }
    }

    public class VersionIds
    {
        public string? brand_id { get; set; }
        public string? model_id { get; set; }
        public string? hardware_version { get; set; }
        public string? firmware_version { get; set; }
        public string? band_id { get; set; }
    }

    public class NetworkIds
    {
        public string? net_id { get; set; }
        public string? tenant_id { get; set; }
        public string? cluster_id { get; set; }
    }

    public class UplinkMessage
    {
        public string? session_key_id { get; set; }
        public int f_port { get; set; }
        public int f_cnt { get; set; }
        public string? frm_payload { get; set; }
        public DecodedPayload? decoded_payload { get; set; }
        public List<RxMetadata>? rx_metadata { get; set; }
        public Settings? settings { get; set; }
        public string? received_at { get; set; }
        public string? consumed_airtime { get; set; }
        public VersionIds? version_ids { get; set; }
        public NetworkIds? network_ids { get; set; }
    }

    public class LoRaMessageV3
    {
        public EndDeviceIds? end_device_ids { get; set; }
        public List<string>? correlation_ids { get; set; }
        public string? received_at { get; set; }
        public UplinkMessage? uplink_message { get; set; }
    }
}
