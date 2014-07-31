using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LastDropMainServer
{
    [Serializable]
    public class UserNotificationOptions
    {
#pragma warning disable 0169
        private string mail;
        private TimeSpan iFrom;
        private TimeSpan iTo;
        private Boolean mailToggle;
        private Boolean desktopToggle;
        private int interval;

        private UserNotificationOptions() { }

        public UserNotificationOptions(string Mail, TimeSpan IFrom, TimeSpan ITo, Boolean MailToggle, Boolean DesktopToggle, int Interval)
        {
            this.mail = Mail;
            this.iFrom = IFrom;
            this.iTo = ITo;
            this.mailToggle = MailToggle;
            this.desktopToggle = DesktopToggle;
            this.interval = Interval;
        }

        public string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }

        [XmlIgnore]
        public TimeSpan IFrom
        {
            get { return this.iFrom; }
            set { this.iFrom = value; }
        }

        [XmlElement("IFrom")]
        public long IFromTicks
        {
            get { return iFrom.Ticks; }
            set
            {
                iFrom = new TimeSpan(value);
            }
        }

        [XmlIgnore]
        public TimeSpan ITo
        {
            get { return this.iTo; }
            set { this.iTo = value; }
        }


        [XmlElement("ITo")]
        public long IToTicks
        {
            get { return iTo.Ticks; }
            set
            {
                iTo = new TimeSpan(value);
            }
        }

        public Boolean MailToggle
        {
            get { return this.mailToggle; }
            set { this.mailToggle = value; }
        }
        public Boolean DesktopToggle
        {
            get { return this.desktopToggle; }
            set { this.desktopToggle = value; }
        }
        public int Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }

        public override string ToString()
        {
            return this.mail + " " + this.iFrom + " " + this.iTo + " " + this.mailToggle + " " + this.desktopToggle + " " + this.interval;
        }

        public bool Equals(UserNotificationOptions opt)
        {
            // If parameter is null return false:
            if ((object)opt == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.mail == opt.Mail) && (this.iFrom == opt.IFrom) && (this.iTo == opt.ITo) && (this.mailToggle == opt.MailToggle) && (this.desktopToggle == opt.DesktopToggle) && (this.interval == opt.Interval);
        }
    }

}