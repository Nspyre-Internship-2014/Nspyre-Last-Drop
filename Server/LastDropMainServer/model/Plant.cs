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
        private int plantID;

        private Plant()
        { }

        public Plant(string name, string status, int waterAmount, TimeSpan coolDown, int dryValue, int humidValue, int id)
        {
            this.name = name;
            this.status = status;
            this.waterAmount = waterAmount;
            this.coolDown = coolDown;
            this.dryValue = dryValue;
            this.humidValue = humidValue;
            this.plantID = id;
        }


        public override string ToString()
        {
            return this.name + " " + this.status + " " + this.coolDown + " " + this.waterAmount + " " + this.dryValue + " " + this.humidValue + " " + this.plantID;
        }

        public bool Equals(Plant p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.name == p.Name) && (this.status == p.Status) && (this.waterAmount == p.WaterAmount)
                    && (this.coolDown == p.CoolDown) && (this.dryValue == p.DryValue) && (this.humidValue == p.HumidValue) && (this.plantID == p.PlantID);
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

        public int PlantID
        {
            get { return this.plantID; }
            set { this.plantID = value; }
        }
    }
}
