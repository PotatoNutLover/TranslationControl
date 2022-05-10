using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PotatoTranslationControl.Editor.DataTemplates;
using PotatoTranslationControl.Editor.TranslationOperations;

namespace PotatoTranslationControl.Editor
{
    public partial class TranslationEditorWindow : Window
    {
        private Controller _controller;
        public TranslationEditorWindow()
        {
            InitializeComponent();
            
            TranslationRecordDataTemplateSettings templateSettings = new TranslationRecordDataTemplateSettings()
            {
                GridLostFocusHandler = TranslationRecordElementGrid_LostFocus,
            };

            InitializeController();
            UpdateNameAndDescription();
            TranslationRecordsListBox.ItemTemplate = _controller.CreateDataTemplate(templateSettings);
            UpdateView();
        }

        public TranslationEditorWindow(string path, TranslationRecordType recordsType)
        {
            InitializeComponent();

            TranslationRecordDataTemplateSettings templateSettings = new TranslationRecordDataTemplateSettings()
            {
                GridLostFocusHandler = TranslationRecordElementGrid_LostFocus,
            };

            _controller = new Controller(path, recordsType);
            UpdateNameAndDescription();
            TranslationRecordsListBox.ItemTemplate = _controller.CreateDataTemplate(templateSettings);
            UpdateView();
        }

        private void InitializeController()
        {
            string[] startupArguments = StartupArgumentsParser.Instance.GetArguments();

            if (startupArguments.Length == 0)
                _controller = new Controller(TranslationRecordType.FullEditable);
            else if (startupArguments.Length == 1)
                _controller = new Controller(startupArguments[0], TranslationRecordType.FullEditable);
            else if (startupArguments.Length == 2)
                _controller = new Controller(startupArguments[0], IdentifyRecordsType(startupArguments[1]));
            else
                _controller = new Controller();
        }

        private TranslationRecordType IdentifyRecordsType(string argument)
        {
            if (argument == StartupArguments.FullEditableRecordsArgument)
                return TranslationRecordType.FullEditable;
            else if (argument == StartupArguments.NonEditableRecordsArgument)
                return TranslationRecordType.NonEditable;
            else if (argument == StartupArguments.NonEditableKeyRecordsArgument)
                return TranslationRecordType.NonEditableKey;
            else if (argument == StartupArguments.NonEditableValueRecordsArgument)
                return TranslationRecordType.NonEditableValue;
            else
                return TranslationRecordType.FullEditable;
        }

        private void TranslationRecordElementGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(((sender as Grid).Children[0] as Label).Content.ToString(), out int id);
            string key = ((sender as Grid).Children[1] as TextBox).Text;
            string value = ((sender as Grid).Children[2] as TextBox).Text;
            _controller.ExecuteAddOrChangeOperation(id, key, value);
            UpdateView();
        }

        private void ButtonRedo_Click(object sender, RoutedEventArgs e)
        {
            _controller.Redo();
            UpdateView();
        }

        private void ButtonUndo_Click(object sender, RoutedEventArgs e)
        {
            _controller.Undo();
            UpdateView();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateNameAndDescription();
            _controller.Update();
            UpdateView();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(TranslationRecordsListBox.SelectedItems.Count > 0)
            {
                List<int> idList = new List<int>();
                foreach (TranslationListRecord record in TranslationRecordsListBox.SelectedItems)
                {
                    Int32.TryParse(record.Id, out int id);
                    idList.Add(id);
                }
                _controller.DeleteRecords(idList.ToArray());
                UpdateView();
            }
        }

        private void ButtonOpenExplorer_Click(object sender, RoutedEventArgs e)
        {
            _controller.OpenExplorer();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            await _controller.SaveFile();
        }

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            TranslationRecordsListBox.ScrollIntoView(TranslationRecordsListBox.Items[_controller.FindRecordId()]);
        }

        private async void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            await _controller.SaveFileAs();
        }

        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateNameAndDescription()
        {
            TranslationName.Text = _controller.TranslationName;
            TranslationDescription.Text = _controller.TranslationDescription;
        }

        private void UpdateView()
        {
            TranslationRecordsListBox.ItemsSource = _controller.GetRecords();
            UpdateNameAndDescription();
        }

        private void MenuItemOpenFile_Click(object sender, RoutedEventArgs e)
        {
            _controller.OpenFile(TranslationRecordType.FullEditable);
            UpdateNameAndDescription();
            UpdateView();
        }

        private void MenuItemNewFile_Click(object sender, RoutedEventArgs e)
        {
            _controller.CreateNewFile(TranslationRecordType.FullEditable);
            UpdateNameAndDescription();
            UpdateView();
        }

        private void MenuItemPasteKeyToValue_Click(object sender, RoutedEventArgs e)
        {
            if (TranslationRecordsListBox.SelectedItems.Count > 0)
            {
                int id;
                int.TryParse((TranslationRecordsListBox.SelectedItems[0] as TranslationListRecord).Id, out id);
                _controller.AddOperation(new ChangeValueOperation(id, (TranslationRecordsListBox.SelectedItems[0] as TranslationListRecord).Key));
                UpdateView();
            }
        }

        private void TranslationName_LostFocus(object sender, RoutedEventArgs e)
        {
            _controller.ChangeTranslationName(TranslationName.Text);
        }

        private void TranslationDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            _controller.ChangeTranslationDescriotion(TranslationDescription.Text);
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_controller.TranslationSaved == false)
                await _controller.ShowExitWithoutSavingWindow();
        }
    }
}
