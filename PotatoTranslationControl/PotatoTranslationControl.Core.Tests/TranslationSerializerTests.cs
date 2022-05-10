using NUnit.Framework;
using System.Runtime.Serialization;

namespace PotatoTranslationControl.Core.Tests
{
    public class TranslationSerializerTests
    {
        private TranslationSerializer _serializer;
        private Translation _translation;
        private string _serializedTranslationString;
        private string _badSerializedTranslationString;

        [SetUp]
        public void SetUp()
        {
            _translation = new Translation("test_TEST", "testDescription");
            _serializer = new TranslationSerializer();
            _serializedTranslationString = "<Translation xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/PotatoTranslationControl.Core\"><_description>testDescription</_description><_name>test_TEST</_name><_translationRecords><TranslationRecord><_key>key1</_key><_value>value1</_value></TranslationRecord></_translationRecords></Translation>";
            _badSerializedTranslationString = "gdfgidfgiertgmdifgnw;dkiirmrtwewret";
        }

        [Test]
        public void SerializeTranslation()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            string text = _serializer.Serialize(_translation);

            StringAssert.Contains(_serializedTranslationString, text);
        }

        [Test]
        public void DeserializeTranslation()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            string text = _serializer.Serialize(_translation);

            Translation actual = _serializer.Deserialize(text);

            if (_translation.Name == actual.Name && _translation.Description == actual.Description)
                Assert.AreEqual(_translation.GetRecords(), actual.GetRecords());
            else
                Assert.Fail($"Actual. Name: {actual.Name}, Description: {actual.Description}, Records: {actual.GetRecords()}");
        }
        
        [Test]
        public void DeserializeBadStringException()
        {
            Assert.Throws<SerializationException>(() => _serializer.Deserialize(_badSerializedTranslationString));
        }




    }
}
