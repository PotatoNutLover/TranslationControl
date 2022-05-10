using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class AddRecordOperation : IOperation
    {
        private readonly TranslationRecord _record;

        public AddRecordOperation(string key, string value) => _record = new TranslationRecord(key, value);

        public AddRecordOperation(TranslationRecord record) => _record = record;

        public void Execute(Translation translation)
        {
            translation.AddRecord(_record.Clone());
        }
    }
}
