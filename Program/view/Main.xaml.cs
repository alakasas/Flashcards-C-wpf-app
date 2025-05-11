using System.Diagnostics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        Brush ButtonBrush;
        Button curentlyClickedButton;
        public Window1()
        {
            MessageBox.Show("Current Directory: " + Environment.CurrentDirectory);
            MessageBox.Show("Hello from normal mode!");
            InitializeComponent();
            LoadFlashcardFiles();
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
                Height = 30,
                Margin = new Thickness(15, 4, 15, 4),
                Tag = flashcards,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB28FB8")),
                FontSize = 12,
                BorderThickness = new Thickness(2),
                FontWeight = FontWeights.Bold,
                

            };
            button.Click += showFlashcardsClick;
            button.MouseEnter += MouseEnterButonEvent;
            button.MouseLeave += MouseLeaveButonEvent;
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

            rename.ShowDialog();

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
                curentlyClickedButton = clickedButton;
                Flashcards currentFlashcards = (Flashcards) clickedButton.Tag;
                list = currentFlashcards.SetOfFlashcards.ToList();
                idx = 0;
                if (list.Count > 0)
                    showAnotherFlashcard();
                else
                {
                    MainFlashcardButton.Tag = null;
                    MainFlashcard.Text = "";
                }
            }
           

        }

        private void showAnotherFlashcard()
        {
            if(list != null && list.Count != 0 && idx >= 0)
            {
                MainFlashcardButton.Tag = list[idx];
                MainFlashcard.Text = list[idx].Question;

            }
            else
            {
                MainFlashcardButton.Tag = null;
                MainFlashcard.Text = "";
            }
                
        }

        private void GoLeft_Click(object sender, RoutedEventArgs e)
        {
            if (MainFlashcardButton.Tag != null)
            {
                idx = (idx + list.Count - 1) % list.Count;
                MessageBox.Show(idx.ToString());
                showAnotherFlashcard();
            }
        }

        private void GoRight_Click(object sender, RoutedEventArgs e)
        {
            if (MainFlashcardButton.Tag != null)
            {
                idx = (idx + 1) % list.Count;
                showAnotherFlashcard();
            }
        }

        private void MainFlashcard_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (MainFlashcard.Text != "")
            {
                Flashcard flashcard = (Flashcard)button.Tag;
                
                if (flashcard.Question == MainFlashcard.Text)
                    MainFlashcard.Text = flashcard.Answer;
                else
                    MainFlashcard.Text = flashcard.Question;
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

                showAnotherFlashcard();
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
            string filePath = Path.Combine(folderPath, "new" + num.ToString() + ".txt");
            while (File.Exists(filePath))
            {
                num++;
                filePath = Path.Combine(folderPath,"new" + num.ToString() + ".txt");
            }
            using (File.Create(filePath)) { }
            LoadFlashcardFiles();
        }

        private void MouseEnterButonEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {

            Button button = sender as Button;
            ButtonBrush = button.Background;

            SolidColorBrush oldSolidColor =  button.Background as SolidColorBrush;
            Color Oldcolor = oldSolidColor.Color;
            Color lighter = Color.FromRgb(
                (byte) Math.Max(Oldcolor.R - 30, 30),
                (byte) Math.Max(Oldcolor.G - 30, 30),
                (byte) Math.Max(Oldcolor.B - 30, 30)
            );
            button.Background = new SolidColorBrush(lighter);
        }

        private void MouseLeaveButonEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = ButtonBrush;
        }

        private void ChangeFlashcardEvent(object sender, RoutedEventArgs e)
        {


        }

        private void DeleteFlashcardEvent(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                MessageBox.Show("1");
                Flashcards flashcards = (Flashcards)curentlyClickedButton.Tag;
                MessageBox.Show("w");
                flashcards.SetOfFlashcards.Remove(list[idx]);
                list.RemoveAt(idx);
                if(list.Count > 0 )
                    idx = (idx - 1 + list.Count) % list.Count;
                showAnotherFlashcard();
                }
            }

        }
    }
    
