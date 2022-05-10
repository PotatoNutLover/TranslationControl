using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public class ChangeTranslationDescriptionOperation : IOperation
    {
        private readonly string _description;

        public ChangeTranslationDescriptionOperation(string newDescription) => _description = newDescription;

        public void Execute(Translation translation)
        {
            translation.Description = _description;
        }
    }
}
