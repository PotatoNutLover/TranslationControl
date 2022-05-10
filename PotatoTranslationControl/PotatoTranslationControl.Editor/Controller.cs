using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using PotatoTranslationControl.Core;
using PotatoTranslationControl.Editor.DialogueWindows;
using PotatoTranslationControl.Editor.DataTemplates;
using PotatoTranslationControl.Editor.TranslationOperations;

namespace PotatoTranslationControl.Editor
{
    public class Controller
    {
        private bool _translationSaved = true;
        private const string _translationFileExtension = ".trsl";
        private string _filePath;
        private string _translationName;
        private string _translationDescprition;
        private SystemMemento _systemMemento;
        private TranslationRecordType _recordsType;

        public Controller(string path, TranslationRecordType recordsType)
        {
            OpenFileByPath(path, recordsType);
        }

        public Controller(string path)
        {
            OpenFileByPath(path, TranslationRecordType.FullEditable);
        }

        public Controller(TranslationRecordType recordsType)
        {
            CreateNewFile(recordsType);
        }

        public Controller()
        {
            CreateNewFile(TranslationRecordType.FullEditable);
        }

        public string FilePath
        {
            get => _filePath;
        }

        public string TranslationName
        {
            get => _systemMemento.CurrentState.Name;        
        }

        public string TranslationDescription
        {
            get => _systemMemento.CurrentState.Description;
        }

        public bool TranslationSaved
        {
            get => _translationSaved;
        }

        public void OpenFile(TranslationRecordType recordsType)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = $"TranslationFiles(*.trls)|*{_translationFileExtension}";
            if(openFileDialog.ShowDialog() == true)
            {
                _filePath = openFileDialog.FileName;
                Translation translation = new TranslationSerializer().Deserialize(new TranslationFile().ReadFile(_filePath));
                _translationName = translation.Name;
                _translationDescprition = translation.Description;
                _systemMemento = new SystemMemento(translation);
                _recordsType = recordsType;
            }
        }

        public void OpenFileByPath(string path, TranslationRecordType recordsType)
        {
            _filePath = path;
            Translation translation = new TranslationSerializer().Deserialize(new TranslationFile().ReadFile(path));
            _translationName = translation.Name;
            _translationDescprition = translation.Description;
            _systemMemento = new SystemMemento(translation);
            _recordsType = recordsType;
        }

        public void CreateNewFile(TranslationRecordType recordsType)
        {
            _filePath = "";
            _translationName = "New translation file";
            _translationDescprition = "Description";
            _recordsType = recordsType;
            _systemMemento = new SystemMemento(new Translation(_translationName, _translationDescprition));
        }

        public async Task SaveFile()
        {
            if (_filePath != "")
            {
                TranslationFile translationFile = new TranslationFile();
                _systemMemento.CurrentState.Name = TranslationName;
                _systemMemento.CurrentState.Description = TranslationDescription;
                await translationFile.WriteFileAsync(_filePath, new TranslationSerializer().Serialize(_systemMemento.CurrentState));
                _translationSaved = true;
            }
            else
                await SaveFileAs();
        }

        public async Task SaveFileAs()
        {
            string filePath;
            TranslationFile translationFile = new TranslationFile();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"TranslationFiles(*.trls)|*{_translationFileExtension}";
            if (saveFileDialog.ShowDialog() == true)
            {
                _systemMemento.CurrentState.Name = TranslationName;
                _systemMemento.CurrentState.Description = TranslationDescription;
                filePath = saveFileDialog.FileName;
                _filePath = filePath;
                await translationFile.WriteFileAsync(filePath, new TranslationSerializer().Serialize(_systemMemento.CurrentState));
                _translationSaved = true;
            }
        }

        public DataTemplate CreateDataTemplate(TranslationRecordDataTemplateSettings settings)
        {
            return new TranslationRecordDataTemplateFactory(settings).CreateDataTemplate(_recordsType);
        }

        public List<TranslationListRecord> GetRecords()
        {
            List<TranslationListRecord> records;

            if(_recordsType == TranslationRecordType.FullEditable)
                records = new TranslationRecordsConverter().ConvertRecordsWithAddingEmptyRecord(_systemMemento.CurrentState.GetRecords());
            else
                records = new TranslationRecordsConverter().ConvertRecords(_systemMemento.CurrentState.GetRecords());

            return records;
        }

        public void AddOperation(IOperation operation)
        {
            OperationManager.Instance.AddOperation(operation);
            OperationManager.Instance.Process(_systemMemento);
            _translationSaved = false;
        }

        public void Redo()
        {
            OperationManager.Instance.Redo();
            OperationManager.Instance.Process(_systemMemento);
            _translationSaved = false;
        }

        public void Undo()
        {
            OperationManager.Instance.Undo();
            OperationManager.Instance.Process(_systemMemento);
            _translationSaved = false;
        }

        public void DeleteRecords(int[] idList)
        {
            AddOperation(new DeleteListOfRecordsOperation(idList));
        }

        public void ExecuteAddOrChangeOperation(int id, string key, string value)
        {
            if (_systemMemento.CurrentState.HasKey(key) == false && id == _systemMemento.CurrentState.Count)
            {
                AddOperation(new AddRecordOperation(key, value));
            }
            else if (_systemMemento.CurrentState.HasKey(key) == true && id < _systemMemento.CurrentState.Count)
            {
                AddOperation(new ChangeValueOperation(id, value));
            }
            else if (_systemMemento.CurrentState.HasKey(key) == false && id < _systemMemento.CurrentState.Count)
            {
                AddOperation(new ChangeKeyOperation(id, key));
            }
            else
                new TranslationMessageBox($"Adding record failed. Key({key})").ShowDialog();
        }

        public void Update()
        {
            OperationManager.Instance.Process(_systemMemento);
        }

        public void OpenExplorer()
        {
            if (_filePath != "")
            {
                string[] path = _filePath.Split('\\');
                string explorerParh = "";
                for (int i = 0; i < path.Length - 1; i++)
                    explorerParh += $"{path[i]}\\";
                Process.Start("explorer.exe", explorerParh);
            }
            else
                new TranslationMessageBox("Alert", "File not saved.").ShowDialog();
        }

        public int FindRecordId()
        {
            int id = -1;
            string request = "";
            FindRecordWindow findRecordWindow = new FindRecordWindow();
            if(findRecordWindow.ShowDialog() == false)
            {
                request = findRecordWindow.Request.Trim();
            }

            id = _systemMemento.CurrentState.IndexOf(request);
            if (id == -1)
                id = _systemMemento.CurrentState.GetRecords().FindIndex(0, x => x.Value == request);

            if (id == -1 && request != "")
            {
                new TranslationMessageBox("Record not found", $"Record not found by request ({request})").ShowDialog();
                id = 0;
            }
            else if (id == -1)
                id = 0;

            return id;
        }

        public void ChangeTranslationName(string toName)
        {
            if (toName != _systemMemento.CurrentState.Name)
                AddOperation(new ChangeTranslationNameOperation(toName));
        }

        public void ChangeTranslationDescriotion(string toDescripton)
        {
            if (toDescripton != _systemMemento.CurrentState.Description)
                AddOperation(new ChangeTranslationDescriptionOperation(toDescripton));
        }

        public async Task ShowExitWithoutSavingWindow()
        {
            bool exitWithoutSaving = false;
            FileNotSavedWindow fileNotSavedWindow = new FileNotSavedWindow();

            if (fileNotSavedWindow.ShowDialog() == false)
                exitWithoutSaving = fileNotSavedWindow.ExitWithoutSaving;

            if (exitWithoutSaving == false)
                await SaveFile();
        }
    }
}
