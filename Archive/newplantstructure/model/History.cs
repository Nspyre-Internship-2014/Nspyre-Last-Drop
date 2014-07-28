using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropMainServer
{
    [Serializable]
    public class History
    {
        private string plantName;
        private DateTime wateredOn;

        private History()
        { }

        public History(string PlantName, DateTime WateredOn)
        {
            this.PlantName = PlantName;
            this.WateredOn = WateredOn;
        }

        public string ToString()
        {
            return this.PlantName + " " + this.WateredOn;
        }

        public bool Equals(History h)
        {
            // If parameter is null return false:
            if ((object)h == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.PlantName == h.PlantName) && (this.WateredOn == h.WateredOn);
        }

        public string PlantName{ get; set; }

        public DateTime WateredOn { get; set; }
    }
}
