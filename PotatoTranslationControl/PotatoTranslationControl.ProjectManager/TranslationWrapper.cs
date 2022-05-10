using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.ProjectManager
{
    [DataContract]
    public class TranslationWrapper
    {
        private const string _defaultIconPath = "\\Controls\\SaveImg.png";
        [DataMember] private string _translationPath;
        [DataMember] private Translation _translation;
        [DataMember] private BitmapImage _translationIcon;

        public string TranslationPath
        {
            get => _translationPath;
        }

        public Translation Translation
        {
            get => _translation;
        }

        public BitmapImage TranslationIcon
        {
            get => _translationIcon;
            set => _translationIcon = value;
        }

        public TranslationWrapper(string translationPath)
        {
            OpenTranslationFile(translationPath);
            InitializeIcon(_defaultIconPath);
        }

        public TranslationWrapper(string translationPath, string iconPath)
        {
            OpenTranslationFile(translationPath);
            InitializeIcon(iconPath);
        }

        public void OpenTranslationFile(string path)
        {
            _translationPath = path;
            TranslationFile translationFile = new TranslationFile();
            TranslationSerializer translationSerializer = new TranslationSerializer();
            _translation = translationSerializer.Deserialize(translationFile.ReadFile(path));
        }

        private void InitializeIcon(string path)
        {
            _translationIcon = new BitmapImage();
            _translationIcon.BeginInit();
            _translationIcon.UriSource = new Uri(_defaultIconPath, UriKind.RelativeOrAbsolute);
            _translationIcon.EndInit();
        }
    }
}
