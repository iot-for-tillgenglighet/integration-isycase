namespace Fiware
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Open311ServiceRequest
    {
        [JsonProperty("@context")]
        public string[] Context { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public TextProperty Description { get; set; }

        [JsonProperty("service_code")]
        public NumberProperty ServiceCode { get; set; }

        [JsonProperty("service_name")]
        public TextProperty ServiceName { get; set; }

        [JsonProperty("service_request_id")]
        public NumberProperty ServiceRequestID { get; set; }

        [JsonProperty("requested_datetime")]
        public DateTimeProperty RequestedDatetime { get; set; }

        [JsonProperty("location")]
        public GeoProperty Location { get; set; }

        public Open311ServiceRequest(string device, double latitude, double longitude, int requestId) {
            Id = "urn:ngsi-ld:Open311ServiceRequest:" + requestId;
            Type = "Open311ServiceRequest";
            Location = new GeoProperty(latitude, longitude);
            Context = new string[2] { "https://schema.lab.fiware.org/ld/context", "https://uri.etsi.org/ngsi-ld/v1/ngsi-ld-core-context.jsonld" };
            ServiceRequestID = new NumberProperty(requestId);
            RequestedDatetime = new DateTimeProperty(DateTime.UtcNow);
        }
    }

    public class LifebuoyServiceRequest : Open311ServiceRequest {

        public LifebuoyServiceRequest(string device, double latitude, double longitude, int requestId) : base(device, latitude, longitude, requestId) {
            ServiceCode = new NumberProperty(42);
            ServiceName = new TextProperty("Lifebouy");
            Description = new TextProperty("Lifebuoy has been reported as missing.");
        }
    }
}