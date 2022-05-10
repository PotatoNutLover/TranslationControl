using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.Editor.TranslationOperations
{
    public interface IOperation
    {
        public void Execute(Translation translation);
    }
}
