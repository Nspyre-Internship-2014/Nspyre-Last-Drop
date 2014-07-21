using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LastDropMainServer
{
    [Serializable]
    class SerializableTimeSpan : IXmlSerializable
    {
        TimeSpan value;

        public SerializableTimeSpan(TimeSpan timeSpan)
        {
            this.value = timeSpan;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.value = TimeSpan.Parse(reader.ReadElementContentAsString());
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteValue(this.value.ToString());
        }
    }
}