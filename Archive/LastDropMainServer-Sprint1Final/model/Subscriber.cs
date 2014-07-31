using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastDropMainServer
{
    [Serializable]
    public class Subscriber
    {
        #pragma warning disable 0169
        private string mailSubscriber;
        private string plantName;

         private Subscriber()
        { }

        public Subscriber(string MailSubscriber, string PlantName)
        {
            this.MailSubscriber = MailSubscriber;
            this.PlantName = PlantName;
        }

        public override string ToString()
        {
            return this.MailSubscriber + " " + this.PlantName;
        }

        public bool Equals(Subscriber s)
        {
            // If parameter is null return false:
            if ((object)s == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.MailSubscriber == s.MailSubscriber) && (this.PlantName == s.PlantName);
        }

        public string MailSubscriber{ get; set; }

        public string PlantName{ get; set; }    
    }
}
