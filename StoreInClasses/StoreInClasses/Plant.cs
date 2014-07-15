using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInClasses
{
    class Plant
    {
        private string Name;
        private string Status;
        private int WaterAmount;

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
    }
}
