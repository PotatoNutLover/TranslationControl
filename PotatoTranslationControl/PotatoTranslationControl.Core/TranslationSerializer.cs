using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

namespace PotatoTranslationControl.Core
{
    public class TranslationSerializer
    {
        public string Serialize(Translation translation)
        {
            DataContractSerializer serializer = new DataContractSerializer(translation.GetType());

            StringBuilder stringBuilder = new StringBuilder();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment
            };

            using(XmlWriter writer = XmlWriter.Create(stringBuilder, xmlWriterSettings))
            {
                serializer.WriteObject(writer, translation);
            }

            return stringBuilder.ToString();
        }

        public Translation Deserialize(string text)
        {
            using(var stream = GenerateStreamFromString(text))
            {
                Translation translation;
                DataContractSerializer serializer = new DataContractSerializer(typeof(Translation));

                using (XmlReader xmlReader = XmlReader.Create(stream))
                {
                    translation = (Translation)serializer.ReadObject(xmlReader);
                }

                return translation;
            }
        }

        private Stream GenerateStreamFromString(string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
