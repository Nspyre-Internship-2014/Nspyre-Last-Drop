using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreInClasses
{
    class Subscriber
    {
        private string MailSubscriber;
        private string PlantName;

        public Subscriber(string MailSubscriber, string PlantName)
        {
            this.MailSubscriber = MailSubscriber;
            this.PlantName = PlantName;
        }

        public string ToString()
        {
            return this.MailSubscriber + " " + this.PlantName;
        }
    }
}
