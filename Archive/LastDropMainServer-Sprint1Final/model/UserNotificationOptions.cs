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
            this.Mail = Mail;
            this.iFrom = IFrom;
            this.iTo = ITo;
            this.MailToggle = MailToggle;
            this.DesktopToggle = DesktopToggle;
            this.Interval = Interval;
        }

        public string Mail
        {
            get;
            set;
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
            get;
            set;
        }
        public Boolean DesktopToggle
        {
            get;
            set;
        }
        public int Interval
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Mail + " " + this.iFrom + " " + this.iTo + " " + this.MailToggle + " " + this.DesktopToggle + " " + this.Interval;
        }

        public bool Equals(UserNotificationOptions opt)
        {
            // If parameter is null return false:
            if ((object)opt == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (this.Mail == opt.Mail) && (this.IFrom == opt.IFrom) && (this.ITo == opt.ITo) && (this.MailToggle == opt.MailToggle) && (this.DesktopToggle == opt.DesktopToggle) && (this.Interval == opt.Interval);
        }
    }

}