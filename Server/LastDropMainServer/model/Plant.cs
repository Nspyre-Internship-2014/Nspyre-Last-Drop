using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LastDropMainServer
{
    [Serializable]
    public class Plant
    {
        private string name;
        private string status;
        private int waterAmount;
        private TimeSpan coolDown;
        private int dryValue;
        private int humidValue;

        private Plant()
        { }

        public Plant(string name, string status, int waterAmount, TimeSpan coolDown, int dryValue, int humidValue)
        {
            this.Name = name;
            this.Status = status;
            this.WaterAmount = waterAmount;
            this.CoolDown = coolDown;
            this.DryValue = dryValue;
            this.HumidValue = humidValue;

        }


        public override string ToString()
        {
            return this.Name + " " + this.Status + " " + this.CoolDown + " " + this.WaterAmount + " " + this.DryValue + " " + this.HumidValue;
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

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public int WaterAmount
        {
            get { return this.waterAmount; }
            set { this.waterAmount = value; }
        }

        [XmlIgnore]
        public TimeSpan CoolDown
        {
            get { return this.coolDown; }
            set { this.coolDown = value; }
        }

        [XmlElement("CoolDown")]
        public long IToTicks
        {
            get { return coolDown.Ticks; }
            set { coolDown = new TimeSpan(value); }
        }

        public int DryValue
        {
            get { return this.dryValue; }
            set { this.dryValue = value; }
        }

        public int HumidValue
        {
            get { return this.humidValue; }
            set { this.humidValue = value; }
        }
    }
}
