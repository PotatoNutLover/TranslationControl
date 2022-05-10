using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor
{
    public class SystemMemento
    {
        private readonly string _rawInitialState;
        private Translation _initialState;
        private Translation _currentSystemState;

        public Translation InitialState
        {
            get
            {
                _initialState = new TranslationSerializer().Deserialize(_rawInitialState);
                return _initialState;
            }
        }
        public Translation CurrentState
        {
            get
            {
                if (_currentSystemState == null)
                    _currentSystemState = new TranslationSerializer().Deserialize(_rawInitialState);
                return _currentSystemState;
            }
            set => _currentSystemState = value;
        }
        
        public SystemMemento(Translation initialState)
        {
            _rawInitialState = new TranslationSerializer().Serialize(initialState);
        }
    }
}
