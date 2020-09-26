using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IntegrationIsycase
{
    public class AlertClass
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("alertSource")]
        public TextProperty AlertSource { get; set; }

        [JsonProperty("category")]
        public TextProperty Category { get; set; }

        [JsonProperty("subCategory")]
        public TextProperty SubCategory { get; set; }

        [JsonProperty("description")]
        public TextProperty Description { get; set; }

        [JsonProperty("location")]
        public GeoProperty Location { get; set; }

        [JsonProperty("severity")]
        public TextProperty Severity { get; set; }

        [JsonProperty("@context")]
        public string[] Context { get; set; }

        public AlertClass(string id)
        {
            Id = "urn:ngsi-ld:Alert:Alert:" + id;
            Type = "Alert";
            Context = new string[2] { "https://schema.lab.fiware.org/ld/context", "https://uri.etsi.org/ngsi-ld/v1/ngsi-ld-core-context.jsonld" };
        }

    }

    public class TextProperty
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }

        public TextProperty(string value)
        {
            Type = "Property";
            Value = value;
        }
    }

    public class GeoProperty
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public GeoPropertyValue Value { get; set; }

        public GeoProperty(double latitude, double longitude)
        {
            Type = "GeoProperty";
            Value = new GeoPropertyValue(latitude, longitude);
        }

    }

    public class GeoPropertyValue
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }

        public GeoPropertyValue(double latitude, double longitude)
        {
            Type = "Point";
            Coordinates = new double[2] { latitude, longitude };
        }

    }

}
