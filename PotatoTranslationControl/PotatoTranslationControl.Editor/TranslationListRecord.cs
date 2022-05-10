namespace PotatoTranslationControl.Editor
{
    public class TranslationListRecord
    {
        private string _id;
        private string _key;
        private string _value;

        public string Id
        {
            get => _id;
            set => _id = value.Trim();
        }
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

        public TranslationListRecord()
        {
            Id = "";
            Key = "";
            Value = "";
        }

        public TranslationListRecord(string id, string key, string value)
        {
            Id = id;
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Key: {Key}, Value: {Value}";
        }
    }
}
