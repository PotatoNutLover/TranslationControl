using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class DeleteListOfRecordsOperation : IOperation
    {
        private readonly int[] _idArray;

        public DeleteListOfRecordsOperation(int[] toDeleteIdArray) => _idArray = toDeleteIdArray;

        public void Execute(Translation translation)
        {
            foreach(int id in _idArray)
                translation.DeleteRecord(id);
        }
    }
}
