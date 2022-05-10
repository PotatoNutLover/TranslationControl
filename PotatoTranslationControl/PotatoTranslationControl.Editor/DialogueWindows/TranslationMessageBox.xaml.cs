using System.Windows;

namespace PotatoTranslationControl.Editor.DialogueWindows
{
    public partial class TranslationMessageBox : Window
    {
        public TranslationMessageBox(string title, string message)
        {
            InitializeComponent();
            Title = title;
            MessageTextBlock.Text = message;
        }

        public TranslationMessageBox(string message)
        {
            InitializeComponent();
            Title = "";
            MessageTextBlock.Text = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
