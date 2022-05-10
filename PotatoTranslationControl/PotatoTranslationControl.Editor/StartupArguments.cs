using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoTranslationControl.Editor
{
    public static class StartupArguments
    {
        public static string PathArgument { get; } = "*";
        public static string FullEditableRecordsArgument { get; } = "-trf";
        public static string NonEditableRecordsArgument { get; } = "-trn";
        public static string NonEditableKeyRecordsArgument { get; } = "-trnk";
        public static string NonEditableValueRecordsArgument { get; } = "-trnv";
    }
}
