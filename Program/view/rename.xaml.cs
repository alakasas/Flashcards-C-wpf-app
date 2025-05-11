using System;
using System.IO;
using System.Collections.Generic;
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
using Program.models;

namespace Program.view
{
    /// <summary>
    /// Interaction logic for rename.xaml
    /// </summary>
    public partial class rename : Window
    {
        Button button;
        public rename(Button button)
        {
            InitializeComponent();
            this.button = button;
        }

        private void ClickRename(object sender, RoutedEventArgs e)
        {

            Flashcards flashcards = (Flashcards) button.Tag;

            string OldFilePath = flashcards.Path;

            string newPath = Path.Combine(Path.GetDirectoryName(OldFilePath), NewName.Text + ".txt");

            File.Move(OldFilePath, newPath);

            this.Close();
        }
    }
}
