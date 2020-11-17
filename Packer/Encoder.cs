using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {

    /// <summary>
    /// Enkodiert die .tom-Datei zurück in ihren Ursprung
    /// </summary>
    public static class Encoder {

        public static void Encode(String fileName) {

        }

        /// <summary>
        /// Holt sich den Marker für die Datei aus dem Header
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Der Marker für die Datei</returns>
        public static char GetMarker(BinaryReader br) {
            return ' ';
        }
    }
}
