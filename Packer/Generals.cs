using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {

    /// <summary>
    /// Allgemeine feste Werte
    /// </summary>
    public static class Generals {
        public const string FileExt = ".tom";
        public const string DialogFilter = "TOM Datei|.tom";
        public const string MagicNumber = ":)";
        public static char Marker = '`';
        public const byte MaxLengthFileName = 8;
        public const byte MaxLengthExtName = 4;
        public const char EndOfHeader = '\0';
    }
}
