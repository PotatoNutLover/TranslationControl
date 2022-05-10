using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class OperationManager
    {
        private static OperationManager _instance;
        private readonly OperationSequenceList _sequenceList;

        public static OperationManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OperationManager();
                return _instance;
            }
        }

        private OperationManager()
        {
            _sequenceList = new OperationSequenceList();
        }

        public void AddOperation(IOperation operation) => _sequenceList.AddOperation(operation);

        public void Undo() => _sequenceList.Undo();

        public void Redo() => _sequenceList.Redo();

        public void Process(SystemMemento systemMemento)
        {
            Translation translation = systemMemento.InitialState;

            foreach (IOperation operation in _sequenceList.OperationSequence)
                operation.Execute(translation);

            systemMemento.CurrentState = translation;
        }
    }
}
