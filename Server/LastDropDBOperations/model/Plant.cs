using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropDBOperations
{
    class Plant
    {
        private string name;
        private string status;
        private int waterAmount;

        public Plant(string Name, string Status, int WaterAmount)
        {
            this.Name = Name;
            this.Status = Status;
            this.WaterAmount = WaterAmount;
        }

        public string ToString()
        {
            return this.Name + " " + this.Status + " " + this.WaterAmount;
        }

        public string Name { get; set; }

        public string Status { get; set; }

        public int WaterAmount { get; set; }
        
    }
}
