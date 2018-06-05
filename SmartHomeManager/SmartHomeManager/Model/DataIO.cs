using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SmartHomeManager.Model
{
    public class DataIO
    {
        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }

            return objectOut;
        }

        public void logService(string fileName, string item, double input)
        {
            if (!File.Exists(fileName))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement($"{item}s");

                    xmlWriter.WriteStartElement($"{item}");
                    xmlWriter.WriteElementString("value", input.ToString());
                    xmlWriter.WriteElementString("date", DateTime.Now.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            else
            {
                FileStream s2 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XDocument doc = XDocument.Load(s2);
                XElement school = doc.Element($"{item}s");
                school.Add(new XElement($"{item}",
                           new XElement("value", $"{input}"),
                           new XElement("date", $"{DateTime.Now}")));
                doc.Save(fileName);
            }
        }

        public List<double> ReadLog(string fileName, string item)
        {
            double help = 0;
            DateTime helpdate;
            List<double> retVal = new List<double>();

            if (File.Exists(fileName))
            {
                FileStream s2 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                XDocument document = XDocument.Load(s2);

                var values = from i in document.Descendants(item)
                             select new
                             {
                                 Value = i.Element("value").Value,
                                 Date = i.Element("date").Value
                             };

                foreach (var value in values)
                {
                    helpdate = DateTime.Parse(value.Date);
                    if (helpdate.Minute % 3 == 0 || helpdate.Minute == 7 || helpdate.Minute == 16 || helpdate.Minute == 22)
                    {
                        Double.TryParse(value.Value, out help);
                        retVal.Add(help);
                    }
                }
            }
            return retVal;
        }
    }
}
