﻿using System;
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

            GetMarker(br, fsRead);

            // Header einfügen
            WriteHeader(bw, fileName);

            fsRead.Position = 0;

            while(fsRead.Position < fsRead.Length) {
                byte c = br.ReadByte();

                byte count = GetCountOfChar(fsRead, br, c);

                // Wenn die der Char nicht marked werden soll, dann die Bytes normal reinschreiben und nächsten Schleifenintervall erzwingen, und Marker soll dabei nicht beachtet werden
                if(count <= 3 && c != Generals.Marker) {
                    fsRead.Position -= count; // oder count - 1 | TESTEN

                    for(int i = 0; i < count; i++)
                        bw.Write(br.ReadByte());

                    continue;
                }

                bw.Write((byte)Generals.Marker);
                bw.Write(count);
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
        public static byte GetCountOfChar(FileStream fs, BinaryReader br, byte val) {
            byte count = 1;

            while(true) {
                if(fs.Position == fs.Length)
                    break;

                if(br.ReadByte() == val)
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
            string fileName = info.Name.Replace(info.Extension, ""); // info.Name gibt den Dateinamen mit Extension; Da nur die Länge des Namens relevant ist wird die Extension abgehakt
            string extension = info.Extension.Length <= Generals.MaxLengthExtName ? info.Extension : info.Extension.Substring(0, Generals.MaxLengthExtName); // Zählt zu der Extension der "." ?

            // Den einzufügenden Namen ermitteln
            if(fileName.Length > Generals.MaxLengthFileName)
                header += fileName.Substring(0, Generals.MaxLengthFileName) + extension;
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
        public static void GetMarker(BinaryReader br, FileStream fsRead) //geringst vorkommenden marker ermitteln
        {
            int[] array = new int[256]; //array für ascii zeichen
            
            while (fsRead.Position < fsRead.Length)
             {
                char c = (char)br.ReadByte();
                for (int i = 0; i < array.Length; i++)
                {
                    if (i == c) //wenn zeichen aus file ascii position im array entspricht
                    {
                        array[i] += 1; //wert an der position des ascii wertes erhöhen
                        break;
                    }    
                }
            }
            Generals.Marker = LeastUsed(array);
        }

        /// <summary>
        /// Sucht nach dem am geringsten genutzen Char
        /// </summary>
        /// <param name="array">Das Array mit den ASCII-Werten</param>
        /// <returns></returns>
        static char LeastUsed(int[] array)
        {
            char marker = ' ';
            for (int i = 0; i < array.Length; i++)
            {
                for (int k = 0; k < array.Length - 1 - i; k++)
                {
                    if (array[k] == 0)
                        return (char)(k);
                    else if (array[k] < array[k + 1])
                    {
                        marker = (char)(k);
                    }
                    else
                        marker = (char)(k + 1);
                }
            }
            return marker;
        }
    }
}
