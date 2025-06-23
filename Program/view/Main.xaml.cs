using Program.functions;
using Program.models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            InitializeComponent();
            this.Loaded += Window1_Loaded;
        }
        private async void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFlashcardFiles();
        }
        private async Task LoadFlashcardFiles()
        {
            FlashcardPanel.Children.Clear();

            string[] files = Directory.GetFiles(folderPath, "*.txt");

            var tasks = files.Select(async file =>
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                var flashcards = await Task.Run(() => FlashcardsRW.ReadFlashCards(file));

                await Dispatcher.InvokeAsync(() =>
                {
                    AddButtonToLeft(FlashcardPanel, fileName, flashcards);
                });
            });

            await Task.WhenAll(tasks);

            SortButtonsAlphabetically();
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
            button.Click += ShowFlashcardsClick;
            button.MouseEnter += MouseEnterButonEvent;
            button.MouseLeave += MouseLeaveButonEvent;
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAB8AB4"));
            MenuItem item1 = new MenuItem();
            item1.Header = "Add flashcard";
            item1.Click += (object s, RoutedEventArgs e) =>
            {
                ShowFlashcardsClick(button, new RoutedEventArgs());
                addOneFlashcard addOneFlashcard = new addOneFlashcard(button, list)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                addOneFlashcard.ShowDialog();


            };

            MenuItem item2 = new MenuItem();
            item2.CommandParameter = button;
            item2.Header = "Rename";
            item2.Click += Rename;

            MenuItem item3 = new MenuItem();
            item3.Header = "Delete";
            item3.Click += async (s, e) =>
            {
                File.Delete(flashcards.Path);
                await LoadFlashcardFiles();
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
        private async void Rename(object sender, RoutedEventArgs e)
        {
        
            Button myButton;
            if (sender.GetType() == typeof(MenuItem)) { 
                MenuItem item = (MenuItem)sender;
                myButton = item.CommandParameter as Button;
                await FlashcardsRW.WriteFlashCards(myButton.Tag as Flashcards);
            }
            else
            {
                myButton = sender as Button;
            }
                
            rename rename = new rename(myButton)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            rename.ShowDialog();

            await LoadFlashcardFiles();
        }
            



        private async void AddFromFile(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                addFromFile secondWindow = new addFromFile()
                {
                    Owner = this,                             
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                secondWindow.ShowDialog();
                await LoadFlashcardFiles();
            }

        }

        private void ShowFlashcardsClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null){

                curentlyClickedButton = clickedButton;
                Flashcards currentFlashcards = (Flashcards) clickedButton.Tag;
                list = currentFlashcards.SetOfFlashcards;
                idx = 0;
                ShowAnotherFlashcard();

            }
           

        }

        private void ShowAnotherFlashcard()
        {
            if(list != null && list.Count != 0 && idx >= 0)
                MainFlashcard.Text = list[idx].Question;
            
            else
                MainFlashcard.Text = "";
            
                
        }

        private void GoLeftClick(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                idx = (idx + list.Count - 1) % list.Count;
                ShowAnotherFlashcard();
            }
        }

        private void GoRightClick(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                idx = (idx + 1) % list.Count;
                ShowAnotherFlashcard();
            }
        }

        private void MainFlashcardClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (MainFlashcard.Text != "")
            {
                
                if (list[idx].Question == MainFlashcard.Text)
                    MainFlashcard.Text = list[idx].Answer;
                else
                    MainFlashcard.Text = list[idx].Question;
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

                ShowAnotherFlashcard();
            }
        }

        private async void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Do something before the window closes
            foreach (Button button in FlashcardPanel.Children)
            {
                Flashcards flashcards = (Flashcards)button.Tag;
                await FlashcardsRW.WriteFlashCards(flashcards);
            }
        }

        private async void CreateNew(object sender, RoutedEventArgs e)
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
            await LoadFlashcardFiles();
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

        public void MouseLeaveButonEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = ButtonBrush;
        }

        private void ChangeFlashcardEvent(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count > 0)
            {
                addOneFlashcard addOneFlashcard = new addOneFlashcard(curentlyClickedButton, list, rename: true, idx)
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                addOneFlashcard.ShowDialog();
                ShowAnotherFlashcard();
            }

        }

        private void DeleteFlashcardEvent(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                list.RemoveAt(idx);
                if(list.Count > 0 )
                    idx = (idx - 1 + list.Count) % list.Count;
                ShowAnotherFlashcard();
            }
        }
        private void ScoreButtonClickEvent(object sender, RoutedEventArgs e)
        {
            if (list != null && list.Count != 0)
            {
                ScoreModeWindow newWindow;
                if ((sender as Button) == randomButton)
                {
                    var rng = new Random();
                    var shuffled = list.OrderBy(x => rng.Next()).ToList();
                    newWindow = new ScoreModeWindow(shuffled)
                    {
                        Owner = this,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                }
                else
                {
                    newWindow = new ScoreModeWindow(list)
                    {
                        Owner = this,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                }

                newWindow.ShowDialog();
                
            }
        }

        private void SearchBoxKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string query = SearchBox.Text.ToLower();
            


            foreach (var child in FlashcardPanel.Children)
            {
                if (child is Button btn)
                {
                    string content = btn.Content.ToString().ToLower();
                    btn.Visibility = content.Contains(query) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void SearchBoxPlaceHolderGotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBoxPlaceHolder.Text == "search")
            {
                SearchBoxPlaceHolder.Text = "";
            }


        }

        private void SearchBoxPlaceHolderLostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                SearchBoxPlaceHolder.Text = "search";
            }
        }

        private void SortButtonsAlphabetically()
        {
            var sorted = FlashcardPanel.Children
                           .OfType<Button>()
                           .OrderBy(b => b.Content?.ToString() ?? "")
                           .ToList();

            FlashcardPanel.Children.Clear();

            foreach (var btn in sorted)
                FlashcardPanel.Children.Add(btn);
        }
    }
}
    
