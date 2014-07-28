using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace LastDropMainServer
{
    class SensorDataCollection
    {
        ReadArduino arduinoReader;
        OperationController controller;
        TimeSpan updateInterval;
        Dictionary<Plant, Int32> plantData;

        public SensorDataCollection(OperationController controller, TimeSpan updateInterval)
        {
            this.updateInterval = updateInterval;
            this.controller = controller;
            this.plantData = new Dictionary<Plant, int>();


            arduinoReader = new ReadArduino(1000, "COM4");

            Thread oThread = new Thread(new ThreadStart(collectSensorData));
            oThread.Start();

        }

        public string getSensorData()
        {
            return arduinoReader.getData();
        }

        public void collectSensorData()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(this.timer_Tick);
            timer.Interval = this.updateInterval.Seconds * 1000;
            timer.Enabled = true;
            timer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            List<Plant> plantlist = controller.getPlantList();

            Int32 sensorValue = Int32.Parse(getSensorData());
            Debug.WriteLine("Sensor value is: " + sensorValue);

            foreach (Plant p in plantlist)
            {
                CreateNewOrUpdateExisting<Plant, Int32>(plantData, p, sensorValue);

                //test on the plant "Jon" as our main plant
                if (p.Name == "Jon")
                {
                    controller.updateDatabasePlantState(p, sensorValue);
                }
            }

        }

        public Dictionary<Plant, Int32> getPlantSensorData()
        {
            return new Dictionary<Plant, Int32>(this.plantData);
        }

        private void CreateNewOrUpdateExisting<TKey, TValue>(IDictionary<TKey, TValue> map,
            TKey key, TValue value)
        {
            if (map.ContainsKey(key))
            {
                map[key] = value;
            }
            else
            {
                map.Add(key, value);
            }
        }

    }
}
