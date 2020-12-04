using System;
using System.IO;

namespace Packer {
    public static class UnitTest {
        public static bool TestWriteHeader(String name, String expectedName) {
            String outputFile = "testwriteheader.bin";
            FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            Encoder.WriteHeader(bw, name);

            fs.Flush();
            bw.Close();
            fs.Close();

            fs = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            String header = "";

            for(int i = 0; i < fs.Length - 1; i++)
                header += br.ReadChar();

            fs.Flush();
            br.Close();
            fs.Close();

            return Generals.MagicNumber + Generals.Marker + expectedName + "\r\n" == header;
        }

        public static bool TestGetCharOfHeader(char expected) {
            String outputFile = "TestGetCharOfHeader.bin";
            FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            Encoder.WriteHeader(bw, outputFile);
            fs.Flush();
            bw.Close();
            fs.Close();

            fs = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            return expected == Decoder.GetMarker(fs, br);
        }

        public static bool TestCheckMagic(string fileName) {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            bool checkMagic = Decoder.CheckMagic(br);

            fs.Flush();
            br.Close();
            fs.Close();

            return checkMagic;
        }

        public static bool CheckFiles(string originalFileName, string newFileName) {
            return false;
        }

        public static bool TestLeastChar() {
            string txt = "TestLeastChar.txt";
            FileStream fs = new FileStream(txt, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            for(byte i = 0; i < byte.MaxValue - 1; i++) {
                for(int j = 0; j < i; j++)
                    bw.Write(i);
            }

            fs.Flush();
            bw.Close();
            fs.Close();

            fs = new FileStream(txt, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Encoder.GetMarker(br, fs);

            return Generals.Marker == (char)byte.MaxValue;
        }
    }
}
