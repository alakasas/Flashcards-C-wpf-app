using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Program.functions;
using Program.models;

namespace Program.view
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            MessageBox.Show("Current Directory: " + Environment.CurrentDirectory);
            MessageBox.Show("Hello from normal mode!");
            InitializeComponent();
            LoadFlashcardFiles();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
        }
        private void LoadFlashcardFiles()
        {
            FlashcardPanel.Children.Clear();

            string folderPath = Path.GetFullPath(@"..\..\..\filesFlashCards");
            
            
            string[] files = Directory.GetFiles(folderPath, "*.txt");
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                var flashcards = FlashcardsRW.ReadFlashCards(file); 

                AddButtonToLeft(FlashcardPanel, fileName, flashcards);
            }
        }
        private void AddButtonToLeft(StackPanel stackPanel, string fileName, HashSet<Flashcard> flashcards)
        {
            Button button = new Button
            {
                Content = fileName,
                Margin = new Thickness(5),
                Tag = flashcards

            };
            button.Click += showFlashcardsClick;
            stackPanel.Children.Add(button);
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                addFromFile secondWindow = new addFromFile();
                secondWindow.ShowDialog();
                LoadFlashcardFiles();
            }

        }

        private void showFlashcardsClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            MessageBox.Show("nic");

        }

    }
    
}
