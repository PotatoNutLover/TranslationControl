using System.Windows;
using System.Windows.Controls;

namespace PotatoTranslationControl.Editor.DataTemplates
{
    public class TranslationRecordDataTemplateSettings
    {
        private string _idBinding;
        private string _keyBinding;
        private string _valueBinding;

        public TextChangedEventHandler KeyChangedHandler { get; set; }
        public TextChangedEventHandler ValueChangedHandler { get; set; }
        public RoutedEventHandler GridGotFocusHandler { get; set; }
        public RoutedEventHandler GridLostFocusHandler { get; set; }
        public string IdBinding
        {
            get => _idBinding;
            set => _idBinding = value.Trim();
        }
        public string KeyBinding
        {
            get => _keyBinding;
            set => _keyBinding = value.Trim();
        }
        public string ValueBinding
        {
            get => _valueBinding;
            set => _valueBinding = value.Trim();
        }

        public TranslationRecordDataTemplateSettings()
        {
            IdBinding = "Id";
            KeyBinding = "Key";
            ValueBinding = "Value";
            KeyChangedHandler = (object sender, TextChangedEventArgs e) => { };
            ValueChangedHandler = (object sender, TextChangedEventArgs e) => { };
            GridGotFocusHandler = (object sender, RoutedEventArgs e) => { };
            GridLostFocusHandler = (object sender, RoutedEventArgs e) => { };
        }

        public override string ToString()
        {
            return $"IdBinding: {IdBinding}, KeyBinding: {KeyBinding}, ValueBinding: {ValueBinding}";
        }
    }
}
