using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropMainServer
{
    class SensorDataCollection
    {
        ReadArduino arduinoReader;
        OperationController controller;
        TimeSpan updateInterval;

        public SensorDataCollection(OperationController controller, TimeSpan updateInterval)
        {
            this.updateInterval = updateInterval;
            this.controller = controller;
            try
            {
                arduinoReader = new ReadArduino(1000, "COM4");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string getSensorData()
        {
            return arduinoReader.getData();
        }
    }
}
