using System.IO;

namespace Packer {

    /// <summary>
    /// Enkodiert die .tom-Datei zurück in ihren Ursprung
    /// </summary>
    public static class Decoder {

        /// <summary>
        /// Enkodiert die .tom-Datei zurück in ihren Ursprung
        /// </summary>
        /// <param name="fileName">Die Packer-Datei</param>
        public static bool Decode(string filename, string newFilename) {
            //Streams erstellen und Reader/Writer öffnen
            FileStream fsR = new FileStream(filename, FileMode.Open, FileAccess.Read);
            FileStream fsW = new FileStream(newFilename, FileMode.Create, FileAccess.Write);
            BinaryReader br = new BinaryReader(fsR);
            BinaryWriter bw = new BinaryWriter(fsW);

            if(!CheckMagic(br))
                return false;

            // Nur drinnen bis wir Dateinamen auslesen
            char testC = ' ';

            while(testC != '\r')
                testC = (char)br.ReadByte();

            fsR.Position++; // Erforderlich da wir die fsR.Position auf das Ende unseres Headers brauchen >\n<

            while(fsR.Position < fsR.Length) //durch alle einträge von  file durchgehen
            {
                byte c = br.ReadByte();
                if(c == Generals.Marker) //Marker fest -> wird noch geändert  sobald GetMarker fertig
                {
                    byte count = br.ReadByte();//wert wieoft das folgende zeichen vorkommt
                    byte sign = br.ReadByte();//zeichen
                    for(int i = 0; i < count; i++) // zeichen wird *count geschrieben
                        bw.Write(sign);
                }
                else {
                    bw.Write(c);
                }
            }

            //Streams flushen und alles schließen
            fsR.Flush();
            fsW.Flush();

            br.Close();
            bw.Close();
            fsR.Close();
            fsW.Close();

            return true;
        }

        public static bool CheckMagic(BinaryReader br) {
            string mNumber = "";
            for(int i = 0; i < Generals.MagicNumber.Length; i++)
                mNumber += (char)br.ReadByte();
            if(mNumber != Generals.MagicNumber)
                return false;
            else
                return true;
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