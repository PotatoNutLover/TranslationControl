using System;

namespace PotatoTranslationControl.Core
{
    public class DuplicateKeyException: Exception
    {
        private string _key;
        private int _firstPosition;
        private int _secondPosition;

        public string Key
        {
            get => _key;
        }
        public int FirstPosition
        {
            get => _firstPosition;
        }
        public int SecondPosition
        {
            get => _secondPosition;
        }

        public DuplicateKeyException(string key, int firstPosition, int secondPosition, string message)
            :base(message)
        {
            _key = key.Trim();
            _firstPosition = firstPosition;
            _secondPosition = secondPosition;
        }

        public override string ToString()
        {
            return $"{Message} Key: {Key}, FirstPosition: {FirstPosition}, SecondPosition: {SecondPosition}";
        }
    }
}
