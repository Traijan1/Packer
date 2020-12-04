using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            // ######### TESTS WERDEN IRGENDWO BEHALTEN UM SIE VORZUZEIGEN BEI SCHWIERIGKEITEN

            //String longName = "test123455678.txt";
            //String name = "1234567.txt";

            //MessageBox.Show(UnitTest.TestWriteHeader(longName, "test1234.txt").ToString() + " " + UnitTest.TestWriteHeader(name, "1234567.txt").ToString());

            //MessageBox.Show("Test Marker: " + UnitTest.TestGetCharOfHeader(Generals.Marker).ToString());

            //MessageBox.Show($"Bei einer richtigen Datei: {UnitTest.TestCheckMagic("result.jpg.tom").ToString()}\r\n" +
            //                $"Bei einer Datei ohne MagicNumber: {UnitTest.TestCheckMagic("FileWithoutMagic.tom").ToString()}\r\n" +
            //                $"Bei einer Datei mit halber MagicNumber: {UnitTest.TestCheckMagic("HalfMagicNumber.tom").ToString()}");

            MessageBox.Show($"Test Least Char: {UnitTest.TestLeastChar()} | {Generals.Marker}");
            Clipboard.SetText("" + (byte)Generals.Marker);
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            OpenFileDialog open = new OpenFileDialog(); // Schauen wie man beim Encoden nur .tom-Dateien anzeigen kann, mit Filter, aber wie man checkt ob man gerade encoden will | Probably Tabs

            if(open.ShowDialog() == true)
                FilePath.Text = open.FileName;
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {
            OpenFileDialog of = new OpenFileDialog();
            //of.Filter = Generals.DialogFilter;

            if(of.ShowDialog() == true) {
                SaveFileDialog sf = new SaveFileDialog();
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                sf.FileName = Decoder.GetOldName(fs, br); // Besser machen

                fs.Flush();
                br.Close();
                fs.Close();

                if(sf.ShowDialog() == true) {
                    Decoder.Decode(of.FileName, sf.FileName);
                    MessageBox.Show("Fertig");
                }
            }
        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {
            if(FilePath.Text == "Keinen Pfad angegeben") { // Überarbeiten
                MessageBox.Show("Wählen Sie eine Datei aus.");
                return;
            }

            FileInfo info = new FileInfo(FilePath.Text);

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = Generals.DialogFilter;
            sf.InitialDirectory = info.DirectoryName; // Ändert das SaveFileDialog Verzeichnis auf das der Originaldatei
            sf.FileName = info.Name.Replace(info.Extension, "") + Generals.FileExt; // Standardname ist der Name der Originaldatei

            if(sf.ShowDialog() == true) {
                Encoder.Encode(FilePath.Text, sf.FileName);
                MessageBox.Show("Fertig");

                return;
            }

            MessageBox.Show("Unbekannter Fehler: Button_Encode(object, MouseButtonEventArgs)");
        }
    }
}
