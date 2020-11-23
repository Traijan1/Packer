using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {

    /// <summary>
    /// Dekodiert die .tom-Datei zurück in ihren Ursprung
    /// </summary>
    public static class Decoder {

        /// <summary>
        /// Enkodiert die .tom-Datei zurück in ihren Ursprung
        /// </summary>
        /// <param name="fileName">Die Packer-Datei</param>
        public static void Decode(String fileName) {

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
