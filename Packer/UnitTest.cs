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
    }
}
