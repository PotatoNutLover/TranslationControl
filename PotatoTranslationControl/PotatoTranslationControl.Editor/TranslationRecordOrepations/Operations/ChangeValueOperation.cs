using System;
using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class ChangeValueOperation : IOperation
    {
        private readonly int _id;
        private readonly string _value;

        public ChangeValueOperation(string id, string value)
        {
            Int32.TryParse(id, out _id);
            _value = value;
        }

        public ChangeValueOperation(int id, string value)
        {
            _id = id;
            _value = value;
        }

        public void Execute(Translation translation)
        {
            translation.ChangeValueByIndex(_id, _value);
        }
    }
}
