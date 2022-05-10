using System.Windows;

namespace PotatoTranslationControl.Editor.DialogueWindows
{
    public partial class FileNotSavedWindow : Window
    {
        private bool _exitWithoutSaving;

        public bool ExitWithoutSaving
        {
            get => _exitWithoutSaving;
        }

        public FileNotSavedWindow()
        {
            InitializeComponent();
            _exitWithoutSaving = true;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            _exitWithoutSaving = true;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            _exitWithoutSaving = false;
            Close();
        }
    }
}
