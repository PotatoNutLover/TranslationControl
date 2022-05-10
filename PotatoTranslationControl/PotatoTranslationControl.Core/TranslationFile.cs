using System.Threading.Tasks;
using System.IO;

namespace PotatoTranslationControl.Core
{
    public class TranslationFile
    {
        public void WriteFile(string path, string text)
        {
            using(StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine(text);
            }
        }

        public async Task WriteFileAsync(string path, string text)
        {
            await Task.Run(() => WriteFile(path, text));
        }

        public string ReadFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd();
        }

    }
}
