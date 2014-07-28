using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropMainServer
{
    [Serializable]
    public class Plant
    {
        private string name;
        private string status;
        private int waterAmount;
        private TimeSpan coolDown;
        private int  dryValue;
        private int  humidValue;




         private Plant()
        { }

         public Plant(string Name, string Status, int WaterAmount, TimeSpan coolDown, int dryValue, int humidValue)
        {
            this.Name = Name;
            this.Status = Status;
            this.WaterAmount = WaterAmount;
            this.coolDown = CoolDown;
            this.dryValue = DryValue;
            this.humidValue = HumidValue;
            
        }


        public override string ToString()
        {
            return this.Name + " " + this.Status +  " " + this.CoolDown +" " + this.WaterAmount + " " + this.DryValue + " " + this.HumidValue;
        }

        public bool Equals(Plant p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Name == p.Name) && (this.Status == p.Status) && (this.WaterAmount == p.WaterAmount)
                    && (this.CoolDown == p.CoolDown) && (this.DryValue == p.DryValue) && (this.HumidValue == p.HumidValue);
        }

        public string Name { get; set; }

        public string Status { get; set; }

        public int WaterAmount { get; set; }
        
        public TimeSpan CoolDown { get; set; }

        public int DryValue { get; set; }

        public int HumidValue { get; set; }
    }
}
