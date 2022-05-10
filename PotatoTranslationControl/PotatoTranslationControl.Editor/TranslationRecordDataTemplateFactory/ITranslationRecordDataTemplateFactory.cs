using System.Windows;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public interface ITranslationRecordDataTemplateFactory
    {
        public DataTemplate CreateDataTemplate(TranslationRecordType recordType);
    }
}
