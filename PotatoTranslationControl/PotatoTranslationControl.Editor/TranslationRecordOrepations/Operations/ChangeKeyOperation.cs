using System;
using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class ChangeKeyOperation : IOperation
    {
        private readonly int _id;
        private readonly string _key;

        public ChangeKeyOperation(int id, string key)
        {
            _id = id;
            _key = key;
        }

        public ChangeKeyOperation(string id, string key)
        {
            Int32.TryParse(id, out _id);
            _key = key;
        }

        public void Execute(Translation translation)
        {
            if(translation.IndexOf(_key) != _id)
                translation.ChangeKeyByIndex(_id, _key);
        }
    }
}
