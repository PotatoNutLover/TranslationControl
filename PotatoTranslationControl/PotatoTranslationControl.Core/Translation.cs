using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PotatoTranslationControl.Core
{
    [DataContract]
    public class Translation
    {
        private const string AddExceptionMessage = "Record adding failed.";
        private const string DeleteExceptionMessage = "Record deleting failed.";
        private const string ChangeExceptionMessage = "Record changing failed.";

        [DataMember] private string _name;
        [DataMember] private string _description;
        [DataMember] private List<TranslationRecord> _translationRecords;

        public string Description
        {
            get => _description;
            set => _description = value.Trim();
        }

        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }

        public int Count
        {
            get => _translationRecords.Count();
        }

        public Translation(string name, string description)
        {
            Name = name;
            Description = description;
            _translationRecords = new List<TranslationRecord>();
        }

        public void AddRecord(string key, string value)
        {
            int index = IndexOf(key);

            if (index == -1)
                _translationRecords.Add(new TranslationRecord(key, value));
            else
                throw new DuplicateKeyException(key, index, _translationRecords.Count, AddExceptionMessage);
        }

        public void AddRecord(TranslationRecord record)
        {
            int index = IndexOf(record.Key);

            if(index == -1)
                _translationRecords.Add(record);
            else
                throw new DuplicateKeyException(record.Key, index, _translationRecords.Count, AddExceptionMessage);
        }

        public void AddRecordsRange(IEnumerable<TranslationRecord> records)
        {
            int index;

            foreach(TranslationRecord record in records)
            {
                index = IndexOf(record.Key);
                if(index == -1)
                    _translationRecords.Add(record);
                else
                    throw new DuplicateKeyException(record.Key, index, _translationRecords.Count, AddExceptionMessage);
            }
        }

        public void DeleteRecord(string key)
        {
            if (HasKey(key))
                _translationRecords.RemoveAll(x => x.Key == key);
            else
                throw new MissingKeyException(key, DeleteExceptionMessage);
        }

        public void DeleteRecord(TranslationRecord record)
        {
            if (HasKey(record.Key))
                _translationRecords.Remove(record);
            else
                throw new MissingKeyException(record.Key, DeleteExceptionMessage);
        }

        public void DeleteRecord(int index) => _translationRecords.RemoveAt(index);

        public void DeleteAllRecords(Predicate<TranslationRecord> predicate) => _translationRecords.RemoveAll(predicate);

        public void DeleteRecordsRange(int index, int count) => _translationRecords.RemoveRange(index, count);

        public void ChangeKeyByKey(string key, string newKey)
        {
            int index = IndexOf(key);
            bool hasNewKey = HasKey(newKey);

            if (index != -1 && hasNewKey == false)
                _translationRecords[index].Key = newKey;
            else if (index == -1)
                throw new MissingKeyException(key, ChangeExceptionMessage);
            else if (hasNewKey == true)
                throw new DuplicateKeyException(newKey, IndexOf(newKey), index, ChangeExceptionMessage);
        }

        public void ChangeKeyByIndex(int index, string newKey)
        {
            bool hasNewKey = HasKey(newKey);

            if (hasNewKey == false)
                _translationRecords[index].Key = newKey;
            else
                throw new DuplicateKeyException(newKey, IndexOf(newKey), index, ChangeExceptionMessage);
        }

        public void ChangeValueByIndex(int index, string newValue)
        {
            _translationRecords[index].Value = newValue;
        }

        public void ChangeValueByKey(string key, string newValue)
        {
            int index = IndexOf(key);

            if (index != -1)
                _translationRecords[index].Value = newValue;
            else
                throw new MissingKeyException(key, ChangeExceptionMessage);
        }

        public void ReplaceRecords(int index, int toIndex)
        {
            TranslationRecord tempRecord = _translationRecords[index];

            _translationRecords[index] = _translationRecords[toIndex];
            _translationRecords[toIndex] = tempRecord;
        }

        public bool HasKey(string key)
        {
            if (_translationRecords.Where(x => x.Key == key).FirstOrDefault() != null)
                return true;
            else
                return false;
        }

        public int IndexOf(string key) => _translationRecords.FindIndex(0, x => x.Key == key);

        public TranslationRecord GetRecordByKey(string key)
        {
            TranslationRecord record = _translationRecords.Where(x => x.Key == key).FirstOrDefault();

            if (record != null)
                return record.Clone();
            else
                return CreateEmptyRecord();
        }

        public TranslationRecord GetRecordByValue(string value)
        {
            TranslationRecord record = _translationRecords.Where(x => x.Value == value).FirstOrDefault();

            if (record != null)
                return record.Clone();
            else
                return CreateEmptyRecord();
        }

        public List<TranslationRecord> GetRecordsWhere(Func<TranslationRecord, bool> predicate) => _translationRecords.Where(predicate).Select(x => x.Clone()).ToList();

        public List<TranslationRecord> GetRecords() => _translationRecords.Select(x => x.Clone()).ToList();

        public List<string> GetKeyList()
        {
            List<string> keys = new List<string>();

            foreach (TranslationRecord record in _translationRecords)
                keys.Add(record.Key);

            return keys;
        } 

        private TranslationRecord CreateEmptyRecord() => new TranslationRecord("", "");
    }
}
