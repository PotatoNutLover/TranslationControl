using NUnit.Framework;
using System.Collections.Generic;

namespace PotatoTranslationControl.Core.Tests
{
    public class TranslationTests
    {
        private Translation _translation; 

        [SetUp]
        public void Setup()
        {
            _translation = new Translation("test_TEST", "TestTranslation");
        }

        [Test]
        public void AddRecordByKeyAndValue()
        {
            _translation.AddRecord("key1", "value1");
            string[] excepted = new string[] { "key1", "value1" };

            string[] actual = new string[] { _translation.GetRecords()[0].Key, _translation.GetRecords()[0].Value };

            Assert.AreEqual(excepted, actual);
        }

        [Test]
        public void AddRecordByTranslationRecord()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");
            _translation.AddRecord(record);
 
            TranslationRecord actualRecord = _translation.GetRecords()[0];

            Assert.AreEqual(record, actualRecord);
        }

        [Test]
        public void AddTranslationRecordsRange()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.AreEqual(translationRecords, _translation.GetRecords());
        }

        [Test]
        public void AddTranslationRecordsByTranslationRecordWithEqualKeysException()
        {
            TranslationRecord first = new TranslationRecord("key1", "value1");
            TranslationRecord second = new TranslationRecord("key1", "value1");

            _translation.AddRecord(first);

            Assert.Throws<DuplicateKeyException>(() => _translation.AddRecord(second));
        }

        [Test]
        public void AddTranslationRecordsRengeWithEqualKeysException()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key2","value3")
            };

            Assert.Throws<DuplicateKeyException>(() => _translation.AddRecordsRange(translationRecords));
        }

        [Test]
        public void DeleteTranslationRecordByKey()
        {
            _translation.AddRecord(new TranslationRecord("key1", "value1"));

            _translation.DeleteRecord("key1");

            Assert.AreEqual(0, _translation.GetRecords().Count);
        }

        [Test]
        public void DeleteTranslationRecordByTranslationRecord()
        {
            _translation.AddRecord(new TranslationRecord("key1", "value1"));

            _translation.DeleteRecord(new TranslationRecord("key1", "value1"));

            Assert.AreEqual(0, _translation.GetRecords().Count);
        }

        [Test]
        public void DeleteTranslationRecordByIndex()
        {
            _translation.AddRecord(new TranslationRecord("key1", "value1"));

            _translation.DeleteRecord(0);

            Assert.AreEqual(0, _translation.GetRecords().Count);
        }

        [Test]
        public void DeleteAllRecords()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value2")
            };

            List<TranslationRecord> expectedRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1")
            };

            _translation.AddRecordsRange(translationRecords);

            _translation.DeleteAllRecords(x => x.Value == "value2");

            Assert.AreEqual(expectedRecords, _translation.GetRecords());
        }

        [Test]
        public void DeleteRecordsRange()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value2")
            };

            List<TranslationRecord> expectedRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1")
            };

            _translation.AddRecordsRange(translationRecords);

            _translation.DeleteRecordsRange(1, 2);

            Assert.AreEqual(expectedRecords, _translation.GetRecords());
        }

        [Test]
        public void DeleteTranslationRecordByMissingKeyException()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value2")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<MissingKeyException>(() => _translation.DeleteRecord("key4"));
        }

        [Test]
        public void DeleteTranslationRecordByTranslationRecordWithMissingKeyException()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value2")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<MissingKeyException>(() => _translation.DeleteRecord(new TranslationRecord("key4", "value5")));
        }

        [Test]
        public void HasAvailableKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(true, _translation.HasKey("key1"));
        }

        [Test]
        public void HasMissingKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(false, _translation.HasKey("key2"));
        }

        [Test]
        public void IndexOfAvailableKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(0, _translation.IndexOf("key1"));
        }

        [Test]
        public void IndexOfMissingKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(-1, _translation.IndexOf("key2"));
        }

        [Test]
        public void GetRecordByAvailableKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(new TranslationRecord("key1", "value1"), _translation.GetRecordByKey("key1"));
        }

        [Test]
        public void GetRecordByMissingKey()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(new TranslationRecord("", ""), _translation.GetRecordByKey("key2"));
        }

        [Test]
        public void GetRecordByAvailableValue()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(new TranslationRecord("key1", "value1"), _translation.GetRecordByValue("value1"));
        }

        [Test]
        public void GetRecordByMissingValue()
        {
            _translation.AddRecord("key1", "value1");

            Assert.AreEqual(new TranslationRecord("", ""), _translation.GetRecordByValue("value2"));
        }

        [Test]
        public void GetRecordsWhere()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.AreEqual(new List<TranslationRecord>() { new TranslationRecord("key2", "value2") }, _translation.GetRecordsWhere(x => x.Key == "key2"));
        }

        [Test]
        public void GetKeysList()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<string> expectedKeys = new List<string>() { "key1", "key2", "key3" };

            _translation.AddRecordsRange(translationRecords);

            Assert.AreEqual(expectedKeys, _translation.GetKeyList());
        }

        [Test]
        public void InabilityToChangeRecievedRecordByKey()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            TranslationRecord expectedRecord = new TranslationRecord("key1", "value1");
            TranslationRecord recievedRecord = _translation.GetRecordByKey("key1");
            recievedRecord.Value = "changedValue";

            Assert.AreEqual(expectedRecord, _translation.GetRecords()[0]);
        }

        [Test]
        public void InabilityToChangeRecievedRecordByValue()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            TranslationRecord expectedRecord = new TranslationRecord("key1", "value1");
            TranslationRecord recievedRecord = _translation.GetRecordByValue("value1");
            recievedRecord.Value = "changedValue";

            Assert.AreEqual(expectedRecord, _translation.GetRecords()[0]);
        }

        [Test]
        public void InabilityToChangeRecievedRecordsWhere()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            TranslationRecord expectedRecord = new TranslationRecord("key1", "value1");
            List<TranslationRecord> recievedRecords = _translation.GetRecordsWhere(x => x.Key == "key1");
            recievedRecords[0].Value = "changedValue";

            Assert.AreEqual(expectedRecord, _translation.GetRecords()[0]);
        }

        [Test]
        public void InabilityToChangeRecievedRecords()
        {
            TranslationRecord record = new TranslationRecord("key1", "value1");

            _translation.AddRecord(record);

            TranslationRecord expectedRecord = new TranslationRecord("key1", "value1");
            List<TranslationRecord> recievedRecords = _translation.GetRecords();
            recievedRecords[0].Value = "changedValue";

            Assert.AreEqual(expectedRecord, _translation.GetRecords()[0]);
        }

        [Test]
        public void ChangeKeyByAvailableKey()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<TranslationRecord> expectedTranslationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key4","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            _translation.ChangeKeyByKey("key3", "key4");

            Assert.AreEqual(expectedTranslationRecords, _translation.GetRecords());
        }

        [Test]
        public void ChangeMissingKeyByKeyExpections()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<MissingKeyException>(() => _translation.ChangeKeyByKey("key4", "key6"));
        }

        [Test]
        public void ChangeKeyByDuplicateKeyExceptions()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<DuplicateKeyException>(() => _translation.ChangeKeyByKey("key3", "key2"));
        }

        [Test]
        public void ChangeAvailableKeyByIndex()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<TranslationRecord> expectedTranslationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key4","value3")
            };

            _translation.AddRecordsRange(translationRecords);
            _translation.ChangeKeyByIndex(2, "key4");

            Assert.AreEqual(expectedTranslationRecords, _translation.GetRecords());
        }

        [Test]
        public void ChangeDuplicateKeyByIndex()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<DuplicateKeyException>(() => _translation.ChangeKeyByIndex(2, "key2"));
        }

        [Test]
        public void ChangeValueByIndex()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<TranslationRecord> expectedTranslationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value4")
            };

            _translation.AddRecordsRange(translationRecords);
            _translation.ChangeValueByIndex(2, "value4");

            Assert.AreEqual(expectedTranslationRecords, _translation.GetRecords());
        }

        [Test]
        public void ChangeValueByAvailableKey()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<TranslationRecord> expectedTranslationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value4")
            };

            _translation.AddRecordsRange(translationRecords);
            _translation.ChangeValueByKey("key3", "value4");

            Assert.AreEqual(expectedTranslationRecords, _translation.GetRecords());
        }

        [Test]
        public void ChangeValueByMissingKey()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.Throws<MissingKeyException>(() => _translation.ChangeValueByKey("key4", "value4"));
        }

        [Test]
        public void ReplaceRecords()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            List<TranslationRecord> expectedRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key3","value3"),
                new TranslationRecord("key2","value2")
            };

            _translation.AddRecordsRange(translationRecords);
            _translation.ReplaceRecords(2, 1);

            Assert.AreEqual(expectedRecords, _translation.GetRecords());
        }

        [Test]
        public void GetTranslationRecordsCount()
        {
            List<TranslationRecord> translationRecords = new List<TranslationRecord>()
            {
                new TranslationRecord("key1","value1"),
                new TranslationRecord("key2","value2"),
                new TranslationRecord("key3","value3")
            };

            _translation.AddRecordsRange(translationRecords);

            Assert.AreEqual(3, _translation.Count);
        }
    }
}