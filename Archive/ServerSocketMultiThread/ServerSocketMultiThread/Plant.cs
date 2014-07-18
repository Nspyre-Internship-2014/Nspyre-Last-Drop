using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketMultiThread
{
    class Plant
    {
        private string name;
        private string plantState;
        private int waterAmount;

        public Plant(string name, string plantState, int waterAmount) 
        {
            this.name = name;
            this.plantState = plantState;
            this.waterAmount = waterAmount;
        }

        public string getName()
        {
            return this.name;
        }
        public string getPlantState()
        {
            return this.plantState;
        }
        public int getWaterAmount()
        {
            return this.waterAmount;
        }

        public string toString()
        {
            return this.name + " " + this.plantState + " " + this.waterAmount;
        }
        }


    }
