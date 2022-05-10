using System.Windows;
using System.Windows.Media;

namespace PotatoTranslationControl.Editor.DialogueWindows
{
    public partial class FindRecordWindow : Window
    {
        private bool _findForTextBoxFirstChange;
        private string _request;

        public string Request
        {
            get => _request;
        }

        public FindRecordWindow()
        {
            InitializeComponent();
            _findForTextBoxFirstChange = true;
            _request = "";
        }

        private void FindForTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_findForTextBoxFirstChange == true)
            {
                FindForTextBox.Text = "";
                FindForTextBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                _findForTextBoxFirstChange = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _request = FindForTextBox.Text;
            Close();
        }
    }
}
