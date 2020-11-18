using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {
    
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            FileInfo info = new FileInfo(@"C:\Users\weber\OneDrive\Schule\TAI12\group_rating.txt");

            MessageBox.Show($"Pfad: {info.FullName}\r\n Name: {info.Name.Substring(0, 8)}\r\n Extension: {info.Extension}");
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {

        }
    }
}
