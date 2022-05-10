using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PotatoTranslationControl.Core
{
    [DataContract]
    public class TranslationRecord : IEquatable<TranslationRecord>
    {
        [DataMember] private string _key;
        [DataMember] private string _value;

        public string Key
        {
            get => _key;
            set => _key = value.Trim();
        }

        public string Value
        {
            get => _value;
            set => _value = value.Trim();
        }

        public TranslationRecord(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public TranslationRecord Clone() => new TranslationRecord(Key, Value);

        public override string ToString()
        {
            return $"Key: {Key}, Value: {Value}";
        }

        public bool Equals(TranslationRecord other)
        {
            if (Key == other.Key && Value == other.Value)
                return true;
            else
                return false;
        }
    }
}
