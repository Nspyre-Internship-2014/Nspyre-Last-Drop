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

         private Plant()
        { }

        public Plant(string Name, string Status, int WaterAmount)
        {
            this.Name = Name;
            this.Status = Status;
            this.WaterAmount = WaterAmount;
        }

        public override string ToString()
        {
            return this.Name + " " + this.Status + " " + this.WaterAmount;
        }

        public bool Equals(Plant p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == p.Name) && (this.Status == p.Status);
        }

        public string Name { get; set; }

        public string Status { get; set; }

        public int WaterAmount { get; set; }
        
    }
}
