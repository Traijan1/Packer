using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {
    
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            OpenFileDialog open = new OpenFileDialog(); // Schauen wie man beim Encoden nur .tom-Dateien anzeigen kann, mit Filter, aber wie man checkt ob man gerade encoden will | Probably Tabs

            if(open.ShowDialog() == true)
                FilePath.Text = open.FileName;
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {
            Decoder.Decode("result.pdf.tom", "testTEST.pdf");
        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {
            if(FilePath.Text == "Keinen Pfad angegeben") { // Überarbeiten
                MessageBox.Show("Wählen Sie eine Datei aus.");
                return;
            }
            
            if(FileName.Text == "") {
                MessageBox.Show("Geben Sie einen Namen in das Textfeld ein.");
                return;
            }

            Encoder.Encode(FilePath.Text, FileName.Text);
            MessageBox.Show("Fertig");
        }
    }
}
