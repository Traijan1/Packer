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
        /// /// <param name="fullFileName">Der ganze Pfad zur Datei</param>
        public static void WriteHeader(BinaryWriter bw, string fullFileName) {
            // FileInfos bekommen
            FileInfo info = new FileInfo(fullFileName);
            String fileName = info.Name.Replace(info.Extension, ""); // info.Name gibt den Dateinamen mit Extension; Da nur die Länge des Namens relevant ist wird die Extension abgehakt

            // Magic Number in die ersten Bytes schreiben
            for(int i = 0; i < Generals.MagicNumber.Length; i++)
                bw.Write(Generals.MagicNumber[i]);

            // Marker in den Header einfügen
            bw.Write(Generals.Marker);

            // Den einzufügenden Namen ermitteln
            if(fileName.Length > Generals.MaxLengthFileName)
                fileName = fileName.Substring(0, 8) + info.Extension;
            else
                fileName = info.Name;

            // Namen in den Header einfügen
            for(int i = 0; i < fileName.Length; i++)
                bw.Write(fileName[i]);

            // Header beenden mit einem \r\n (Neue Zeile)
            bw.Write('\r');
            bw.Write('\n');
        }

        
    }
}
