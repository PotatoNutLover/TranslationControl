using System.Windows;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public interface IDataTempleteBuilder
    {
        public IDataTempleteBuilder AddColumn(GridLength width);

        public IDataTempleteBuilder AddRow(GridLength height);

        public IDataTempleteBuilder AddLabel(DataTemplateElementSettings settings);

        public IDataTempleteBuilder AddTextBox(DataTemplateElementSettings settings);

        public IDataTempleteBuilder AddTextBlock(DataTemplateElementSettings settings);

        public DataTemplate Build();
    }
}
