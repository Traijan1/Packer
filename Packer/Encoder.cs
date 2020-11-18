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

        /// <summary>
        /// Enkodiert die .tom-Datei zurück in ihren Ursprung
        /// </summary>
        /// <param name="fileName">Die Packer-Datei</param>
        public static void Encode(String fileName) {

        }

        /// <summary>
        /// Holt sich den Marker für die Datei aus dem Header
        /// </summary>
        /// /// <param name="fs">Der aktuelle Stream auf die Datei</param>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Der Marker für die Datei</returns>
        public static char GetMarker(FileStream fs, BinaryReader br) {
            fs.Position = Generals.MagicNumber.Length;
            return br.ReadChar(); 
        }

        /// <summary>
        /// Gibt den Namen der Originaldatei zurück
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Den Namen der Originaldatei</returns>
        public static string GetOldName(BinaryReader br) {
            return "";
        }
    }
}
