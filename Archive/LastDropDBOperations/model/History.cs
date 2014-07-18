using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropDBOperations
{
    class History
    {
        private string plantName;
        private DateTime wateredOn;

        public History(string PlantName, DateTime WateredOn)
        {
            this.PlantName = PlantName;
            this.WateredOn = WateredOn;
        }

        public string ToString()
        {
            return this.PlantName + " " + this.WateredOn;
        }

        public string PlantName{ get; set; }

        public DateTime WateredOn { get; set; }
    }
}
