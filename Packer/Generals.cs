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
        public const string OpenFileDialogFilter = "tom Datei(." + FileExt + ")|." + FileExt;
        // Maybe die MagicNumber kleiner machen weil ist schon echt huge (Verfehlt dem das komprimieren dann teilweise)
        public const string MagicNumber = "e=mc^2";
        public static char Marker = '`';
        public const int MaxLengthFileName = 8;
        // Ende des Headers maybe ein anderes Zeichen geben, da 2 Zeichen auch too much sind
        public const string EndOfHeader = "\r\n";
    }
}
