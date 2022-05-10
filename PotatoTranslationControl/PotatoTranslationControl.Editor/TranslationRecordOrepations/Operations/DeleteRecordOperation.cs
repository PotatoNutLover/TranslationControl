using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class DeleteRecordOperation : IOperation
    {
        private readonly int _index;

        public DeleteRecordOperation(int index) => _index = index;

        public void Execute(Translation translation)
        {
            translation.DeleteRecord(_index);
        }
    }
}
