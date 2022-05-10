using System.Windows;

namespace PotatoTranslationControl.Editor
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string argumentsMask = $"{StartupArguments.PathArgument}|" +
                $"{StartupArguments.FullEditableRecordsArgument}," +
                $"{StartupArguments.NonEditableRecordsArgument}," +
                $"{StartupArguments.NonEditableKeyRecordsArgument}," +
                $"{StartupArguments.NonEditableValueRecordsArgument}";

            StartupArgumentsParser.Instance.ParseArguments(e.Args, argumentsMask);
        }
    }
}
