using System;

namespace PotatoTranslationControl.Core
{
    public class MissingKeyException: Exception
    {
        private string _key;
        
        public string Key
        {
            get => _key;
        }

        public MissingKeyException(string key, string message)
            :base(message)
        {
            _key = key.Trim();
        }

        public override string ToString()
        {
            return $"{Message} The key ({Key}) is missing";
        }
    }

}
