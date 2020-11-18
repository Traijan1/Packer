using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packer {
    public static class UnitTest {
        public static bool TestWriteHeader(String name, String expectedName) {
            FileStream fs = new FileStream("testwriteheader.bin", FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            Decoder.WriteHeader(bw, name);

            fs.Flush();
            bw.Close();
            fs.Close();

            fs = new FileStream("testwriteheader.bin", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            String header = "";

            for(int i = 0; i < fs.Length - 1; i++)
                header += br.ReadChar();

            fs.Flush();
            br.Close();
            fs.Close();

            return Generals.MagicNumber + Generals.Marker + expectedName + "\r\n" == header;
        }
    }
}
