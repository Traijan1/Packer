using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {
    
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            //String longName = "test123455678.txt";
            //String name = "1234567.txt";

            //MessageBox.Show(UnitTest.TestWriteHeader(longName, "test1234.txt").ToString() + " " + UnitTest.TestWriteHeader(name, "1234567.txt").ToString());

            //MessageBox.Show("Test Marker: " + UnitTest.TestGetCharOfHeader(Generals.Marker).ToString());

            string ex = Generals.MagicNumber + Generals.Marker + "ErsterDe.txt" + "\r\n" + $"{Generals.Marker}{((char)6) - 48}A" + $"{Generals.Marker}{((char)5) - 48}B" + $"{Generals.Marker}{((char)5) - 48}C";
            MessageBox.Show($"Decode: {UnitTest.TestDecoderDecode("black.bmp", "ErsterDecodeTest.bin", ex).ToString()}");
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {

        }
    }
}
