using System;
using System.Windows;
using System.Windows.Controls;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public class DataTemplateElementSettings
    {
        private string _name;
        private string _binding;
        private string _content;
        private int _row;
        private int _column;
        private Thickness _margin;
        private Thickness _padding;

        public TextChangedEventHandler ValueChangedHandler { get; set; }
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }
        public string Binding
        {
            get => _binding;
            set => _binding = value.Trim();
        }
        public string Content
        {
            get => _content;
            set => _content = value.Trim();
        }
        public int Row
        {
            get => _row;
            set => _row = Math.Clamp(value, 0, Int32.MaxValue);
        }
        public int Column
        {
            get => _column;
            set => _column = Math.Clamp(value, 0, Int32.MaxValue);
        }
        public Thickness Margin
        {
            get => _margin;
            set => _margin = value;
        }
        public Thickness Padding
        {
            get => _padding;
            set => _padding = value;
        }

        public DataTemplateElementSettings()
        {
            Name = "";
            Binding = "";
            Content = "";
            Row = 0;
            Column = 0;
            Margin = new Thickness();
            Padding = new Thickness();
            ValueChangedHandler = (object sender, TextChangedEventArgs e) => { };
        }

        public override string ToString()
        {
            return $"Name: {Name}, Binding: {Binding}, Content: {Content}, Row: {Row}, Column: {Column}, Margin: {Margin.ToString()}, Padding: {Padding.ToString()}";
        }
    }
}
