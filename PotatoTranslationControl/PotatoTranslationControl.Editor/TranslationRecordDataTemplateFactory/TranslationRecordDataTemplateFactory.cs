using System.Windows;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public class TranslationRecordDataTemplateFactory : ITranslationRecordDataTemplateFactory
    {
        private readonly Thickness _elementMargin = new Thickness(0, 0, 10, 0);
        private readonly Thickness _elementPadding = new Thickness(10, 5, 10, 5);
        private readonly TranslationRecordDataTemplateSettings _templeteSettings;

        public TranslationRecordDataTemplateFactory() => _templeteSettings = new TranslationRecordDataTemplateSettings();

        public TranslationRecordDataTemplateFactory(TranslationRecordDataTemplateSettings templeteSettings) => _templeteSettings = templeteSettings;

        public DataTemplate CreateDataTemplate(TranslationRecordType recordType)
        {
            switch (recordType)
            {
                case TranslationRecordType.FullEditable:
                    return CreateTemplateFullEditable();

                case TranslationRecordType.NonEditable:
                    return CreateTemplateNonEditable();

                case TranslationRecordType.NonEditableKey:
                    return CreateTemplateWithNonEditableKey();

                case TranslationRecordType.NonEditableValue:
                    return CreateTemplateWithNonEditableValue();

                default:
                    return CreateTemplateFullEditable();
            }
        }

        private DataTemplate CreateTemplateFullEditable()
        {
            DataTemplate template = GetBasicTemplateBuilder()
                .AddLabel(new DataTemplateElementSettings() { Binding = _templeteSettings.IdBinding, Margin = _elementMargin })
                .AddTextBox(new DataTemplateElementSettings() { Binding = _templeteSettings.KeyBinding, Column = 1, Margin = _elementMargin, Padding = _elementPadding, ValueChangedHandler = _templeteSettings.KeyChangedHandler })
                .AddTextBox(new DataTemplateElementSettings() { Binding = _templeteSettings.ValueBinding, Column = 2, Margin = _elementMargin, Padding = _elementPadding, ValueChangedHandler = _templeteSettings.ValueChangedHandler })
                .Build();

            return template;
        }

        private DataTemplate CreateTemplateNonEditable()
        {
            DataTemplate template = GetBasicTemplateBuilder()
                .AddLabel(new DataTemplateElementSettings() { Binding = _templeteSettings.IdBinding, Margin = _elementMargin })
                .AddTextBlock(new DataTemplateElementSettings() { Binding = _templeteSettings.KeyBinding, Column = 1, Margin = _elementMargin, Padding = _elementPadding })
                .AddTextBlock(new DataTemplateElementSettings() { Binding = _templeteSettings.ValueBinding, Column = 2, Margin = _elementMargin, Padding = _elementPadding })
                .Build();

            return template;
        }

        private DataTemplate CreateTemplateWithNonEditableKey()
        {
            DataTemplate template = GetBasicTemplateBuilder()
                .AddLabel(new DataTemplateElementSettings() { Binding = _templeteSettings.IdBinding, Margin = _elementMargin })
                .AddTextBlock(new DataTemplateElementSettings() { Binding = _templeteSettings.KeyBinding, Column = 1, Margin = _elementMargin, Padding = _elementPadding })
                .AddTextBox(new DataTemplateElementSettings() { Binding = _templeteSettings.ValueBinding, Column = 2, Margin = _elementMargin, Padding = _elementPadding, ValueChangedHandler = _templeteSettings.ValueChangedHandler })
                .Build();

            return template;
        }

        private DataTemplate CreateTemplateWithNonEditableValue()
        {
            DataTemplate template = GetBasicTemplateBuilder()
                .AddLabel(new DataTemplateElementSettings() { Binding = _templeteSettings.IdBinding, Margin = _elementMargin })
                .AddTextBox(new DataTemplateElementSettings() { Binding = _templeteSettings.KeyBinding, Column = 1, Margin = _elementMargin, Padding = _elementPadding, ValueChangedHandler = _templeteSettings.KeyChangedHandler})
                .AddTextBlock(new DataTemplateElementSettings() { Binding = _templeteSettings.ValueBinding, Column = 2, Margin = _elementMargin, Padding = _elementPadding })
                .Build();

            return template;
        }

        private IDataTempleteBuilder GetBasicTemplateBuilder()
        {
            return new GridDataTemplateBuilder(_templeteSettings.GridGotFocusHandler, _templeteSettings.GridLostFocusHandler)
                .AddColumn(new GridLength(30, GridUnitType.Auto))
                .AddColumn(new GridLength(200, GridUnitType.Auto))
                .AddColumn(new GridLength(370, GridUnitType.Auto));
        }
    }
}
