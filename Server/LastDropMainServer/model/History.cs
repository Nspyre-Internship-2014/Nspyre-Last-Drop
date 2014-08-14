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
            this.plantName = PlantName;
            this.wateredOn = WateredOn;
        }

        public string ToString()
        {
            return this.plantName + " " + this.wateredOn;
        }

        public bool Equals(History h)
        {
            // If parameter is null return false:
            if ((object)h == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.plantName == h.PlantName) && (this.wateredOn == h.WateredOn);
        }

        public string PlantName
        {
            get { return this.plantName; }
            set { this.plantName = value; }
        }

        public DateTime WateredOn
        {
            get { return this.wateredOn; }
            set { this.wateredOn = value; }
        }
    }
}