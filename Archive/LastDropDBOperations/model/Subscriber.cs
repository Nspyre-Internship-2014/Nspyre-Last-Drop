using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropDBOperations
{
    class Subscriber
    {
        private string mailSubscriber;
        private string plantName;

        public Subscriber(string MailSubscriber, string PlantName)
        {
            this.MailSubscriber = MailSubscriber;
            this.PlantName = PlantName;
        }

        public string ToString()
        {
            return this.MailSubscriber + " " + this.PlantName;
        }

        public string MailSubscriber{ get; set; }

        public string PlantName{ get; set; }    
    }
}
