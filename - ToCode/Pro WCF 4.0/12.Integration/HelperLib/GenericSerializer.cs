using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Runtime.Serialization;

namespace HelperLib
{
    public static class GenericSerializer<T>
    {
        public static string Serialize(T p)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, p);

            return sw.ToString();
        }

        public static T Deserialize(string xml)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(xml);

            return (T)ser.Deserialize(sr);
        }

        static public string SerializeDC(T o)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            StringWriter stringWriter = new StringWriter();

            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            dataContractSerializer.WriteObject(xmlWriter, o);
            xmlWriter.Close();

            return (stringWriter.ToString());
        }

        static public T DeserializeDC(string Xml)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            StringReader stringReader = new StringReader(Xml);
            XmlReader xmlReader = XmlReader.Create(stringReader);

            T obj = (T)dataContractSerializer.ReadObject(xmlReader);

            return obj;
        }


    }
}

