using System.Diagnostics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Controls;

using System.Xml.Serialization;
using Program.functions;
using Program.models;

namespace Program.view
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<Flashcard> list;
        int idx;
        string folderPath = Path.GetFullPath(@"..\..\..\filesFlashCards");
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
            
            string[] files = Directory.GetFiles(folderPath, "*.txt");
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                var flashcards = FlashcardsRW.ReadFlashCards(file); 

                AddButtonToLeft(FlashcardPanel, fileName, flashcards);
            }
        }
        private Button AddButtonToLeft(StackPanel stackPanel, string fileName, Flashcards flashcards)
        {
            Button button = new Button
            {
                Content = fileName,
                Margin = new Thickness(5),
                Tag = flashcards

            };
            button.Click += showFlashcardsClick;
            ContextMenu contextMenu = new ContextMenu();

            MenuItem item1 = new MenuItem();
            item1.Header = "Add flashcard";
            item1.Click += (object s, RoutedEventArgs e) =>
            {

                addOneFlashcard addOneFlashcard = new addOneFlashcard(button, list);
                addOneFlashcard.ShowDialog();


            };

            MenuItem item2 = new MenuItem();
            item2.CommandParameter = button;
            item2.Header = "Rename";
            item2.Click += Rename;

            MenuItem item3 = new MenuItem();
            item3.Header = "delete";
            item3.Click += (s, e) =>
            {
                File.Delete(flashcards.Path);
                LoadFlashcardFiles();
            };

            // Add items to the context menu
            contextMenu.Items.Add(item1);
            contextMenu.Items.Add(item2);
            contextMenu.Items.Add(item3);

            // Assign the context menu to the button
            button.ContextMenu = contextMenu;
            stackPanel.Children.Add(button);

            return button;
            
        }
        private void Rename(object sender, RoutedEventArgs e)
        {
            Button myButton;
            if (sender.GetType() == typeof(MenuItem)) { 
                MenuItem item = (MenuItem)sender;
                myButton = item.CommandParameter as Button;
            }
            else
            {
                myButton = sender as Button;
            }
                
            rename rename = new rename(myButton);
            MessageBox.Show("dffff");
            rename.ShowDialog();
            MessageBox.Show("asdfasdf");
            LoadFlashcardFiles();
        }
            



        private void Button_Click(object sender, RoutedEventArgs e)
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
            if (clickedButton != null){
                Flashcards currentFlashcards = (Flashcards) clickedButton.Tag;
                list = currentFlashcards.SetOfFlashcards.ToList();
                idx = 0;
                if (list.Count > 0)
                    showAnotherFlashcard(list, idx);
                else
                    {
                    MainFlashcard.Tag = null;
                    MainFlashcard.Content = "";
                    }
            }
           

        }

        private void showAnotherFlashcard(List<Flashcard> list, int idx)
        {

            MainFlashcard.Tag = list[idx];
            MainFlashcard.Content = list[idx].Question;

        }

        private void GoLeft_Click(object sender, RoutedEventArgs e)
        {
            if (MainFlashcard.Tag != null)
            {
                idx = (idx + list.Count - 1) % list.Count;
                MessageBox.Show(idx.ToString());
                showAnotherFlashcard(list, idx);
            }
        }

        private void GoRight_Click(object sender, RoutedEventArgs e)
        {
            if (MainFlashcard.Tag != null)
            {
                idx = (idx + 1) % list.Count;
                showAnotherFlashcard(list, idx);
            }
        }

        private void MainFlashcard_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Tag != null)
            {
                Flashcard flashcard = (Flashcard)button.Tag;
                if (flashcard.Question == button.Content)
                    button.Content = flashcard.Answer;
                else
                    button.Content = flashcard.Question;
            }
        }

        private void Swap(object sender, RoutedEventArgs e)
        {
            if (list != null)
            {
                string aux_string;

                foreach (Flashcard item in list)
                {
                    aux_string = item.Question;
                    item.Question = item.Answer;
                    item.Answer = aux_string;
                }

                showAnotherFlashcard(list, idx);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Do something before the window closes
            foreach (Button button in FlashcardPanel.Children)
            {
                Flashcards flashcards = (Flashcards)button.Tag;
                FlashcardsRW.WriteFlashCards(flashcards);
            }
            MessageBox.Show("Window is closing!");
        }

        private void CreateNew(object sender, RoutedEventArgs e)
        {
            string newFileName = "new";
            int num = 0;
            string filePath = Path.Combine(folderPath, "new.txt");
            while (File.Exists(filePath))
            {
                num++;
                filePath = Path.Combine(folderPath,"new" + num.ToString() + ".txt");
            }
            File.Create(filePath);
            Button button = AddButtonToLeft(FlashcardPanel, "new" + num.ToString(), new Flashcards(filePath));
        }
    }
    
}
