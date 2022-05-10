using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoTranslationControl.Core;

namespace PotatoTranslationControl.ProjectManager
{
    public class TranslationFileCreator
    {
        public string CreateNewTranslationAndGetPath(string translationName, string translationDescription)
        {
            Translation translation = new Translation(translationName, translationDescription);
            TranslationSerializer translationSerializer = new TranslationSerializer();
            TranslationFile translationFile = new TranslationFile();
            string serializedTranslation;
            string path = $"TestTranslations\\{translationName}.trsl";

            serializedTranslation = translationSerializer.Serialize(translation);
            translationFile.WriteFile(path, serializedTranslation);

            return path;
        }
    }
}
