using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationIsycase
{
    public class AlertMissingLifebuoy : AlertClass
    {
        public AlertMissingLifebuoy(string id, double latitude, double longitude) : base("somerandomuid")
        {
            AlertSource = new TextProperty(id);
            Category = new TextProperty("safety");
            SubCategory = new TextProperty("equipmentMissing");
            Description = new TextProperty("Safety equipment has been reported as missing.");
            Severity = new TextProperty("high");
            Location = new GeoProperty(latitude, longitude);
        }
    }
}
