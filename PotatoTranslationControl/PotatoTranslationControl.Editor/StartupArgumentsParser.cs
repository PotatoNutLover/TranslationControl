using System.Collections.Generic;

namespace PotatoTranslationControl.Editor
{
    public class StartupArgumentsParser
    {
        private static StartupArgumentsParser _instance;
        private string[] _arguments;
        private readonly char _firstLevelSeparator;
        private readonly char _secondLevelSeparator;

        public static StartupArgumentsParser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StartupArgumentsParser();
                return _instance;
            }
        }

        private StartupArgumentsParser()
        {
            _arguments = new string[0];
            _firstLevelSeparator = '|';
            _secondLevelSeparator = ',';
        }

        public void ParseArguments(string[] args, string mask)
        {
            List<string> parsedArguments = new List<string>();
            string[] maskElements = ParseMask(mask, _firstLevelSeparator);

            for(int i = 0; i < args.Length; i++)
            {
                if (i < maskElements.Length && FilterHasArgument(args[i], maskElements[i]))
                    parsedArguments.Add(args[i]);
            }

            _arguments = parsedArguments.ToArray();
        }

        private bool FilterHasArgument(string argument, string maskElement)
        {
            string[] secondLevelMaskElements = ParseMask(maskElement, _secondLevelSeparator);
            foreach (string secondLevelMaskElement in secondLevelMaskElements)
                if (argument == secondLevelMaskElement || secondLevelMaskElement == "*")
                    return true;
            return false;
        }

        private string[] ParseMask(string mask, char separator)
        {
            string[] maskElements = mask.Split(separator);

            for (int i = 0; i < maskElements.Length; i++)
                maskElements[i].Trim();

            return maskElements;
        }

        public string[] GetArguments()
        {
            return _arguments.Clone() as string[];
        }
    }
}
