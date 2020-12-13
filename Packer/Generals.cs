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
        // Maybe die MagicNumber kleiner machen weil ist schon echt huge (Verfehlt dem das komprimieren dann teilweise)
        public const string MagicNumber = ":)";
        public static char Marker = '`';
        public const byte MaxLengthFileName = 8;
        public const byte MaxLengthExtName = 4;
        // Ende des Headers maybe ein anderes Zeichen geben, da 2 Zeichen auch too much sind
        public const char EndOfHeader = '\0';
    }
}
