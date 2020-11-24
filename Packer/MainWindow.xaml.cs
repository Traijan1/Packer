using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {
    // TestVisua
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
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            OpenFileDialog open = new OpenFileDialog(); // Schauen wie man beim Encoden nur .tom-Dateien anzeigen kann, mit Filter, aber wie man checkt ob man gerade encoden will | Probably Tabs

            if(open.ShowDialog() == true)
                FilePath.Text = open.FileName;
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {
            Decoder.Decode("asdasdadsadsad.tom.tom", "result.txt");
        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {
            if(FilePath.Text == "Keinen Pfad angegeben") { // Überarbeiten
                MessageBox.Show("Wählen Sie eine Datei aus.");
                return;
            }

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = Generals.DialogFilter;
            
            if(sf.ShowDialog() == true) {
                Encoder.Encode(FilePath.Text, sf.FileName);
                MessageBox.Show("Fertig");

                return;
            }

            MessageBox.Show("Unbekannter Fehler: Button_Encode(object, MouseButtonEventArgs)");
        }
    }
}
