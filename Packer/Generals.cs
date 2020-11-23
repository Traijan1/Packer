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
        public const string MagicNumber = "e=mc^2";
        public static char Marker = '^';
        public const int MaxLengthFileName = 8;
        public const string EndOfHeader = "\r\n";
    }
}
