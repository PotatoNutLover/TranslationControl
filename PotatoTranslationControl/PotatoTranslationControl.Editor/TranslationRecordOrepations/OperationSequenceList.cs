using System.Collections.Generic;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class OperationSequenceList
    {
        private List<List<IOperation>> _list;

        private int _undoCount;

        public int CountOfSequences
        {
            get => _list.Count;
        }

        public List<IOperation> OperationSequence
        {
            get
            {
                if (CountOfSequences > 0)
                    return CloneList(_list[CountOfSequences - 1]);
                else
                    return new List<IOperation>();
            }
        }

        public OperationSequenceList()
        {
            _list = new List<List<IOperation>>();
            _undoCount = 0;
        }

        public void AddOperation(IOperation operation)
        {
            if (CountOfSequences > 0)
            {
                List<IOperation> newSequence = CloneList(_list[CountOfSequences - 1]);
                newSequence.Add(operation);
                _list.Add(newSequence);
                _undoCount = 0;
            }
            else
            {
                _list.Add(new List<IOperation>() { operation });
                _undoCount = 0;
            }
        }

        public void Undo()
        {
            if (CountOfSequences > 0 && _list[CountOfSequences - 1].Count > 0)
            {
                List<IOperation> newSequence = CloneList(_list[CountOfSequences - 1]);
                newSequence.RemoveAt(newSequence.Count - 1);
                _list.Add(newSequence);
                _undoCount++;
            }
        }

        public void Redo()
        {
            if (CountOfSequences > 0 && _undoCount > 0)
            {
                _list.RemoveAt(CountOfSequences - 1);
                _undoCount--;
            }
        }

        private List<IOperation> CloneList(List<IOperation> list)
        {
            List<IOperation> newList = new List<IOperation>();

            foreach (IOperation operation in list)
                newList.Add(operation);

            return newList;
        }
    }
}
