namespace Fiware

{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

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

        public class NumberProperty {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set;}

        public NumberProperty(int value) {
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

    public class DateTimeProperty {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public DateTimePropertyValue Value { get; set; }

        public DateTimeProperty(DateTime dateTime) {
            Type = "Property";
            Value = new DateTimePropertyValue(dateTime);
        }
    }

    public class DateTimePropertyValue {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("@value")]
        public string Value { get; set; }

        public DateTimePropertyValue(DateTime dateTime) {
            Type = "DateTime";
            Value = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}