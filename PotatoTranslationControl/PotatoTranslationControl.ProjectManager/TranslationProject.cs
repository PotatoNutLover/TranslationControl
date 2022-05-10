using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoTranslationControl.Core;
using System.Runtime.Serialization;

namespace PotatoTranslationControl.ProjectManager
{
    [DataContract]
    public class TranslationProject
    {
        private const string _defaultProjectName = "New translation project";
        [DataMember] private string _translationProjectName;
        [DataMember] private TranslationWrapper _basicTranslation;
        [DataMember] private List<TranslationWrapper> _additionalTranslations;

        public TranslationProject()
        {
            _translationProjectName = _defaultProjectName;
            string basicPath = new TranslationFileCreator().CreateNewTranslationAndGetPath("en_EN", "Basic Translation");
            _basicTranslation = new TranslationWrapper(basicPath);
            _additionalTranslations = new List<TranslationWrapper>();
        }

        public TranslationProject(string projectName)
        {
            _translationProjectName = projectName;
            string basicPath = new TranslationFileCreator().CreateNewTranslationAndGetPath("en_EN", "Basic Translation");
            _basicTranslation = new TranslationWrapper(basicPath);
            _additionalTranslations = new List<TranslationWrapper>();
        }

        public string TranslationProjectName
        {
            get => _translationProjectName;
            set => _translationProjectName = value.Trim();
        }

        public TranslationWrapper BasicTranslation
        {
            get => _basicTranslation;
        }

        public List<TranslationWrapper> AdditionalTranslations
        {
            get => CloneWrappersList();
        }

        public int AdditionalTranslationCount
        {
            get => _additionalTranslations.Count;
        }

        private List<TranslationWrapper> CloneWrappersList()
        {
            List<TranslationWrapper> listClone = new List<TranslationWrapper>();

            foreach (TranslationWrapper wrapper in _additionalTranslations)
                listClone.Add(wrapper);

            return listClone;
        }

        public void AddAdditionalTranslation(TranslationWrapper translationWrapper) => _additionalTranslations.Add(translationWrapper);

        public void DeleteAdditionalTranslation(int index) => _additionalTranslations.RemoveAt(index);

        public void ReplaceAdditionalTranslatonToBasic(int index)
        {
            TranslationWrapper basicPath = _basicTranslation;
            _basicTranslation = _additionalTranslations[index];
            _additionalTranslations[index] = basicPath;
        }

        public void UpdateTranslations()
        {
            _basicTranslation.OpenTranslationFile(_basicTranslation.TranslationPath);
            foreach (TranslationWrapper wrapper in AdditionalTranslations)
                wrapper.OpenTranslationFile(wrapper.TranslationPath);
        }
    }
}
