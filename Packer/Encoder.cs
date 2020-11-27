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
    public static class Encoder {

        /// <summary>
        /// Dekodiert die Datei und schreibt sie in die neue Datei
        /// </summary>
        /// <param name="fileName">Der Dateiname der ausgelesen werden soll</param>
        /// <param name="newFileName">Der neue Dateiname</param>
        public static void Encode(String fileName, String newFileName) {
            // 2 FileStreams erstellen
            FileStream fsRead = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            FileStream fsWrite = new FileStream(newFileName, FileMode.Create, FileAccess.Write);

            // BinaryWriter und BinaryReader öffnen
            BinaryReader br = new BinaryReader(fsRead);
            BinaryWriter bw = new BinaryWriter(fsWrite);

            //GetMarker(br, fsRead); //Scheint noch Probleme zu verursachen

            // Header einfügen
            WriteHeader(bw, fileName);

            fsRead.Position = 0;

            // Muss noch weiter getestet werden, und am besten mal nochmal ein Vergleich aufbauen (in Tabellen Form) wie die sich die Größe der Dateien sich dann unterscheidet
            while(fsRead.Position < fsRead.Length) {
                char c = (char)br.ReadByte();
                byte count = GetCountOfChar(fsRead, br, c);

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
                bw.Write((byte)Generals.Marker);
                bw.Write((byte)count);
                bw.Write((byte)c);
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
        public static byte GetCountOfChar(FileStream fs, BinaryReader br, char val) {
            byte count = 1;

            while(true) {
                if(fs.Position == fs.Length)
                    break;

                if((char)br.ReadByte() == val)
                    count++;
                else {
                    fs.Position -= 1;
                    break;
                }

                if(count == byte.MaxValue)
                    return count; // Wenn der Wert überschritten ist returnt er den maximalen Bytewert
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
                header += fileName.Substring(0, Generals.MaxLengthFileName) + info.Extension; // 4 Zeichen lange Extension only
            else
                header += info.Name;

            // Header beenden mit einem \r\n
            header += Generals.EndOfHeader;

            // Header reinschreiben
            for(int i = 0; i < header.Length; i++)
                bw.Write((byte)header[i]); // Geändert: Zu Byte gecastet, sollten Fehler passieren: Hier schauen
        }

        /// <summary>
        /// Findet den am geeignetesten Marker für die Datei
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        public static void GetMarker(BinaryReader br, FileStream fsRead) 
        {
            char[] c_array = new char[fsRead.Length];
            int[] i_array = new int[fsRead.Length];
            while (fsRead.Position < fsRead.Length)
             {
                char c = (char)br.ReadByte();
                CheckArray(c_array, i_array, c);
             }
             char marker = ' ';
             for (int i = 0; i < c_array.Length -1; i++)
             {
                if (i_array[i] < i_array[i] + 1)
                    marker = c_array[i];
                else
                    marker = c_array[i + 1];
             }

             Generals.Marker = marker;
        }

        public static void CheckArray(char[] c_array, int[] i_array, char c)
        {
            for (int i = 0; i < c_array.Length; i++)
            {
                if (c_array[i] == c)
                    i_array[i] +=1;
                else
                {
                    c_array[i] = c;
                    i_array[i] = 1;
                }
            }
        }
    }
}
