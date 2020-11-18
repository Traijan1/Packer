using System;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;

namespace Packer {
    
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            String longName = "test123455678.txt";
            String name = "1234567.txt";

            MessageBox.Show(UnitTest.TestWriteHeader(longName, "test1234.txt").ToString() + " " + UnitTest.TestWriteHeader(name, "1234567.txt").ToString());
        }

        private void Button_ChoosePath(object sender, MouseButtonEventArgs e) {
            
        }

        private void Button_Decode(object sender, MouseButtonEventArgs e) {

        }

        private void Button_Encode(object sender, MouseButtonEventArgs e) {

        }
    }
}
