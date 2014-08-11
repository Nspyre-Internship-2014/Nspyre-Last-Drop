using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayApplicationTest
{
    class Subscriber
    {
        private string mailSubscriber;
        private string plantName;

         private Subscriber()
        { }

        public Subscriber(string MailSubscriber, string PlantName)
        {
            this.mailSubscriber = MailSubscriber;
            this.plantName = PlantName;
        }

        public string ToString()
        {
            return this.mailSubscriber + " " + this.plantName;
        }

        public bool Equals(Subscriber s)
        {
            // If parameter is null return false:
            if ((object)s == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.mailSubscriber == s.MailSubscriber) && (this.plantName == s.PlantName);
        }

        public string MailSubscriber
        {
            get { return this.mailSubscriber; }
            set { this.mailSubscriber = value; }
        }

        public string PlantName
        {
            get { return this.plantName; }
            set { this.plantName = value; }
        }    
    }
}
