using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInClasses
{
    class History
    {
        private string PlantName;
        private DateTime WateredOn;

        public History(string PlantName, DateTime WateredOn)
        {
            this.PlantName = PlantName;
            this.WateredOn = WateredOn;
        }

        public string ToString()
        {
            return this.PlantName + " " + this.WateredOn;
        }
    }
}
