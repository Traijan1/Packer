using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {
    public static class UnitTest {
        public static bool TestWriteHeader(String name, String expectedName) {
            String outputFile = "testwriteheader.bin";
            FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            Decoder.WriteHeader(bw, name);

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
            Decoder.WriteHeader(bw, outputFile);
            fs.Flush();
            bw.Close();
            fs.Close();

            fs = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            return expected == Encoder.GetMarker(fs, br);
        }

        public static bool TestDecoderDecode(string fileName, string newFileName, string expected) {
            Decoder.Decode(fileName, newFileName);

            FileStream fs = new FileStream(newFileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            String result = "";

            //for(int i = 0; i < fs.Length - 1; i++)
            //    result += br.ReadChar();

            String s = "";

            try {
                while(true)
                    s += br.ReadChar();
            }
            catch(Exception e) { }

            int test = s[22];
            int t = s[28];
            int t1 = s[34];

            fs.Flush();
            br.Close();
            fs.Close();

            return result == expected;
        }
    }
}
