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
            FileStream fsRead = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            FileStream fsWrite = new FileStream(newFileName + Generals.FileExt, FileMode.Create, FileAccess.Write);

            // BinaryWriter und BinaryReader öffnen
            BinaryReader br = new BinaryReader(fsRead);
            BinaryWriter bw = new BinaryWriter(fsWrite);

            // Header einfügen
            WriteHeader(bw, fileName);

            // Muss noch weiter getestet werden, und am besten mal nochmal ein Vergleich aufbauen (in Tabellen Form) wie die sich die Größe der Dateien sich dann unterscheidet
            while(fsRead.Position < fsRead.Length) {
                char c = (char)br.ReadByte();
                int count = GetCountOfChar(fsRead, br, c);

                // Wenn die der Char nicht marked werden soll, dann die Bytes normal reinschreiben und nächsten Schleifenintervall erzwingen
                if(count <= 3) {
                    fsRead.Position -= count; // oder count - 1 | TESTEN
                    
                    for(int i = 0; i < count; i++) 
                        bw.Write(br.ReadByte());

                    continue;
                }

                //string cache = Generals.Marker + GetCountOfChar(fsRead, br, c).ToString() + c;

                // Schauen welcher der beiden Methoden benutzt werden soll => Die for ist kleiner (146 Bytes), aber die Zahlen sind eben sichtbar in einem Texteditor
                //for(int i = 0; i < cache.Length; i++)
                //    bw.Write(cache[i]);

                // 226 Bytes | Beim testen an black.bmp
                bw.Write(Generals.Marker);
                bw.Write(GetCountOfChar(fsRead, br, c));
                bw.Write(c);
            }

            // FileStreams flushen
            fsRead.Flush();
            fsWrite.Flush();

            // FileStreams und BinaryWriter/BinaryReader closen
            br.Close();
            bw.Close();
            fsRead.Close();
            fsWrite.Close();
        }

        /// <summary>
        /// Zählt wie oft der übergebene Char hintereinander folgt.
        /// </summary>
        /// <param name="fs">Der aktuelle FileStream auf die zu lesende Datei</param>
        /// <param name="br">Der aktuelle BinaryReader auf die zu lesende Datei</param>
        /// <param name="val">Der zu zählende Char</param>
        /// <returns>Die Anzahl wie oft der char vorkommt</returns>
        public static int GetCountOfChar(FileStream fs, BinaryReader br, char val) {
            int count = 1;
            
            while(true) {
                if(fs.Position == fs.Length)
                    break;

                if((char)br.ReadByte() == val)
                    count++;
                else {
                    fs.Position -= 1;
                    break;
                }   
            }

            return count;
        }

        /// <summary>
        /// Erstellt und schreibt den Header in die ersten Bytes
        /// </summary>
        /// <param name="bw">Der BinaryWriter der auf die Datei zeigt</param>
        /// <param name="fullFileName">Der ganze Pfad zur Datei</param>
        public static void WriteHeader(BinaryWriter bw, string fullFileName) {
            String header = Generals.MagicNumber + Generals.Marker;
            
            // FileInfos bekommen
            FileInfo info = new FileInfo(fullFileName);
            String fileName = info.Name.Replace(info.Extension, ""); // info.Name gibt den Dateinamen mit Extension; Da nur die Länge des Namens relevant ist wird die Extension abgehakt


            // Den einzufügenden Namen ermitteln
            if(fileName.Length > Generals.MaxLengthFileName)
                header += fileName.Substring(0, 8) + info.Extension;
            else
                header += info.Name;

            // Header beenden mit einem \r\n (Neue Zeile)
            header += "\r\n";

            // Header reinschreiben
            for(int i = 0; i < header.Length; i++)
                bw.Write(header[i]);
        }

        /// <summary>
        /// Findet den am geeignetesten Marker für die Datei
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        public static void GetMarker(BinaryReader br) {
            // Später dann Generals.Marker überschreiben, ist static statt const.
        }
    }
}
