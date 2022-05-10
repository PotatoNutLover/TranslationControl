using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PotatoTranslationControl.Editor;
using PotatoTranslationControl.Editor.DialogueWindows;

namespace PotatoTranslationControl.ProjectManager.Controls
{
    /// <summary>
    /// Логика взаимодействия для TranslationFileElement.xaml
    /// </summary>
    public partial class TranslationFileElement : UserControl
    {
        private readonly string _translationFilePath;
        private TranslationEditorWindow window;

        public TranslationFileElement(string toOpenPath, string translationName, BitmapImage icon)
        {
            InitializeComponent();
            _translationFilePath = toOpenPath;
            TranslationName.Text = translationName;
            TranslationImage.Source = icon;
        }

        private void TranslationButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        public void OpenFile()
        {
            try
            {
                new TranslationEditorWindow(_translationFilePath, TranslationRecordType.FullEditable).Show();
            }
            catch
            {
                new TranslationMessageBox("Cant open file").Show();
            }
        }

        private void Closed(object sender, EventArgs eventArgs)
        {
            window = null;
        }
    }
}
