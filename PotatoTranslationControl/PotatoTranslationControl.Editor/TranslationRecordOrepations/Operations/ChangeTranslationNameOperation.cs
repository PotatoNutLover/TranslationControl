using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class ChangeTranslationNameOperation : IOperation
    {
        private readonly string _name;

        public ChangeTranslationNameOperation(string newName) => _name = newName;

        public void Execute(Translation translation)
        {
            translation.Name = _name;
        }
    }
}
