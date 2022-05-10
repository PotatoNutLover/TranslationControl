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
using Microsoft.Win32;
using PotatoTranslationControl.ProjectManager.Controls;

namespace PotatoTranslationControl.ProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProjectEditor : Window
    {
        TranslationProject translationProject;
        public ProjectEditor()
        {
            InitializeComponent();
            translationProject = new TranslationProject();

            translationProject.AddAdditionalTranslation(new TranslationWrapper("C:\\Users\\potatoo-pc\\Desktop\\translation.trsl"));
            UpdateView();
        }

        private void UpdateView()
        {
            ProjectName.Text = translationProject.TranslationProjectName;
            BasicTranslationPlace.Children.Clear();

            BasicTranslationPlace.Children.Add(new TranslationFileElement(translationProject.BasicTranslation.TranslationPath, translationProject.BasicTranslation.Translation.Name, translationProject.BasicTranslation.TranslationIcon));

            List<TranslationFileElement> translationFiles = new List<TranslationFileElement>();
            foreach (TranslationWrapper wrapper in translationProject.AdditionalTranslations)
                translationFiles.Add(new TranslationFileElement(wrapper.TranslationPath, wrapper.Translation.Name, wrapper.TranslationIcon));

            AdditionalTranslationsListBox.ItemsSource = translationFiles;
        }

        private void ButtonAddNewTranslation_Click(object sender, RoutedEventArgs e)
        {
            string translationName = "en_EN";
            TranslationFileCreator translationFileCreator = new TranslationFileCreator();
            TranslationWrapper translationWrapper = new TranslationWrapper(translationFileCreator.CreateNewTranslationAndGetPath(translationName, "New AdditionalTranslation"));
            translationProject.AddAdditionalTranslation(translationWrapper);
            translationProject.UpdateTranslations();
            UpdateView();
        }

        private void ButtonAddTranslation_Click(object sender, RoutedEventArgs e)
        {
            string translationPath = "C:\\Users\\potatoo-pc\\Desktop\\translation.trsl";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                translationPath = openFileDialog.FileName;
                TranslationWrapper translationWrapper = new TranslationWrapper(translationPath);
                translationProject.AddAdditionalTranslation(translationWrapper);
                translationProject.UpdateTranslations();
                UpdateView();
            } 
            
        }

        private void ButtonDeleteTranslation_Click(object sender, RoutedEventArgs e)
        {
            int index = AdditionalTranslationsListBox.SelectedIndex;
            if (index > -1)
                translationProject.DeleteAdditionalTranslation(index);
            translationProject.UpdateTranslations();
            UpdateView();
        }

        private void ButtonOpenTranslation_Click(object sender, RoutedEventArgs e)
        {
            if (AdditionalTranslationsListBox.SelectedItems.Count > -1)
                (AdditionalTranslationsListBox.SelectedItems[0] as TranslationFileElement).OpenFile();
            translationProject.UpdateTranslations();
        }

        private void ButtonSetAsBasicTranslation_Click(object sender, RoutedEventArgs e)
        {
            int index = AdditionalTranslationsListBox.SelectedIndex;
            if (index > -1)
                translationProject.ReplaceAdditionalTranslatonToBasic(index);
            UpdateView();
            translationProject.UpdateTranslations();
        }

        private void ButtonOpenProjectInExplorer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
