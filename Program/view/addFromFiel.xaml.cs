using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;


namespace Program.view
{
    public partial class addFromFile : Window
    {
        public addFromFile()
        {
            InitializeComponent();
        }

        private void addFile(string filePath)
        {
            string folderPath = Path.GetFullPath(@"..\..\..\filesFlashCards");
            string fileName = Path.GetFileName(filePath);
            string newFilepath = Path.Combine(folderPath, fileName);
            File.Copy(filePath, newFilepath, overwrite: true);
        }

        private async void pressEnter(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null) {
                
                string path = tb.Text;
                if (e.Key == Key.Enter)
                {
                    if (!File.Exists(path))
                    {
                        tb.BorderBrush = Brushes.Red;
                        MessageBox.Show("subor sa nenasiel");

                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            addFile(path);
                        });
                        this.Close(); 
                    }
                
                }
                else
                {
                    tb.BorderBrush = Brushes.Yellow;
                }

            }

        }

        private async void ExploreWindow(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == true)
            {

                string path = openFileDialog.FileName;
                await Task.Run(() =>
                {
                    addFile(path);
                });
                this.Close();
            }

        }
    }
}
