using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {

    /// <summary>
    /// Kodiert Dateien zu .tom-Dateien
    /// </summary>
    public static class Decoder {

        /// <summary>
        /// Dekodiert die Datei und schreibt sie in die neue Datei
        /// </summary>
        /// <param name="fileName">Der Dateiname der ausgelesen werden soll</param>
        /// <param name="newFileName">Der neue Dateiname</param>
        public static void Decode(String fileName, String newFileName) {
            // 2 FileStreams erstellen


            // BinaryWriter und BinaryReader öffnen




            // FileStream flushen


            // FileStream und BinaryWriter closen
        }

        /// <summary>
        /// Findet den am geeignetesten Marker für die Datei
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Der Marker für die Datei</returns>
        public static char GetMarker(BinaryReader br) {
            return ' ';
        }

        /// <summary>
        /// Erstellt und schreibt den Header in die ersten Bytes
        /// </summary>
        /// <param name="bw">Der BinaryWriter der auf die Datei zeigt</param>
        public static void WriteHeader(BinaryWriter bw) {
            // Magic Number in die ersten Bytes schreiben



            // Marker in den Header einfügen



            // Originalnamen einfügen (Name max. 8 und Extension max. 4 lang)



            // Header beenden mit einem \r\n
        }
    }
}
