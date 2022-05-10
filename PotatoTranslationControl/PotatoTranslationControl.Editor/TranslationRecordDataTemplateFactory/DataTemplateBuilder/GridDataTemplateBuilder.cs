using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public class GridDataTemplateBuilder : IDataTempleteBuilder
    {
        private readonly DataTemplate _dataTemplate;
        private readonly FrameworkElementFactory _mainGrid;

        public GridDataTemplateBuilder()
        {
            _dataTemplate = new DataTemplate();
            _mainGrid = new FrameworkElementFactory(typeof(Grid));
        }

        public GridDataTemplateBuilder(RoutedEventHandler gotFocusHandler, RoutedEventHandler lostFocusHandler)
        {
            _dataTemplate = new DataTemplate();
            _mainGrid = new FrameworkElementFactory(typeof(Grid));
            _mainGrid.AddHandler(Grid.GotFocusEvent, gotFocusHandler);
            _mainGrid.AddHandler(Grid.LostFocusEvent, lostFocusHandler);
        }

        public IDataTempleteBuilder AddColumn(GridLength width)
        {
            FrameworkElementFactory column = new FrameworkElementFactory(typeof(ColumnDefinition));
            column.SetValue(ColumnDefinition.WidthProperty, width);

            _mainGrid.AppendChild(column);

            return this;
        }

        public IDataTempleteBuilder AddRow(GridLength height)
        {
            FrameworkElementFactory row = new FrameworkElementFactory(typeof(RowDefinition));
            row.SetValue(RowDefinition.HeightProperty, height);

            _mainGrid.AppendChild(row);

            return this;
        }

        public IDataTempleteBuilder AddLabel(DataTemplateElementSettings settings)
        {
            FrameworkElementFactory label = new FrameworkElementFactory(typeof(Label));
            label.SetValue(Label.NameProperty, settings.Name);
            label.SetValue(Label.ContentProperty, settings.Content);
            label.SetValue(Label.MarginProperty, settings.Margin);
            label.SetValue(Label.PaddingProperty, settings.Padding);
            label.SetValue(Grid.ColumnProperty, settings.Column);
            label.SetValue(Grid.RowProperty, settings.Row);
            label.SetBinding(Label.ContentProperty, new Binding(settings.Binding));

            _mainGrid.AppendChild(label);

            return this;
        }

        public IDataTempleteBuilder AddTextBlock(DataTemplateElementSettings settings)
        {
            FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetValue(TextBlock.NameProperty, settings.Name);
            textBlock.SetValue(TextBlock.TextProperty, settings.Content);
            textBlock.SetValue(TextBlock.MarginProperty, settings.Margin);
            textBlock.SetValue(TextBlock.PaddingProperty, settings.Padding);
            textBlock.SetValue(Grid.ColumnProperty, settings.Column);
            textBlock.SetValue(Grid.RowProperty, settings.Row);
            textBlock.SetBinding(TextBlock.TextProperty, new Binding(settings.Binding));

            _mainGrid.AppendChild(textBlock);

            return this;
        }

        public IDataTempleteBuilder AddTextBox(DataTemplateElementSettings settings)
        {
            FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
            textBox.SetValue(TextBox.NameProperty, settings.Name);
            textBox.SetValue(TextBox.TextProperty, settings.Content);
            textBox.SetValue(TextBox.MarginProperty, settings.Margin);
            textBox.SetValue(TextBox.PaddingProperty, settings.Padding);
            textBox.SetValue(Grid.ColumnProperty, settings.Column);
            textBox.SetValue(Grid.RowProperty, settings.Row);
            textBox.SetBinding(TextBox.TextProperty, new Binding(settings.Binding));
            textBox.AddHandler(TextBox.TextChangedEvent, settings.ValueChangedHandler);

            _mainGrid.AppendChild(textBox);

            return this;
        }

        public DataTemplate Build()
        {
            _dataTemplate.VisualTree = _mainGrid;

            return _dataTemplate;
        }
    }
}
