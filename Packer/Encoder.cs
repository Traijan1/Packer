using System.IO;

namespace Packer
{

    /// <summary>
    /// Kodiert Dateien zu .tom-Dateien
    /// </summary>
    public static class Encoder
    {

        /// <summary>
        /// Dekodiert die Datei und schreibt sie in die neue Datei
        /// </summary>
        /// <param name="fileName">Der Dateiname der ausgelesen werden soll</param>
        /// <param name="newFileName">Der neue Dateiname</param>
        public static void Encode(String fileName, String newFileName) {
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

                // Schauen welcher der beiden Methoden benutzt werden soll => Die for ist kleiner (146 Bytes), aber die Zahlen sind eben sichtbar in einem Texteditor
                //for(int i = 0; i < cache.Length; i++)
                //    bw.Write((byte)cache[i]);

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

        }

        /// <summary>
        /// Erstellt und schreibt den Header in die ersten Bytes
        /// </summary>
        /// /// <param name="fs">Der aktuelle Stream auf die Datei</param>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Der Marker für die Datei</returns>
        public static char GetMarker(FileStream fs, BinaryReader br) {
            fs.Position = Generals.MagicNumber.Length;
            return br.ReadChar(); 
        }

        /// <summary>
        /// Findet den am geeignetesten Marker für die Datei
        /// </summary>
        /// <param name="br">Der BinaryReader, der die Datei aktuell offen hat</param>
        /// <returns>Den Namen der Originaldatei</returns>
        public static string GetOldName(BinaryReader br) {
            return "";
        }
    }
}
