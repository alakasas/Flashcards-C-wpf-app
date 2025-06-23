using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace Program.view
{
    public partial class addFromFile : Window
    {
        public addFromFile()
        {
            InitializeComponent();
        }

        private void AddFile(string filePath)
        {
            string folderPath = Path.GetFullPath(@"..\..\..\filesFlashCards");
            string fileName = Path.GetFileName(filePath);
            string newFilepath = Path.Combine(folderPath, fileName);
            File.Copy(filePath, newFilepath, overwrite: true);
        }

        private async void PressEnter(object sender, KeyEventArgs e)
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
                            AddFile(path);
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
                    AddFile(path);
                });
                this.Close();
            }

        }
    }
}
