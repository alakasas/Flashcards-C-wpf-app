using Program.models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Program.view
{
    /// <summary>
    /// Interaction logic for ScoreModeWindow.xaml
    /// </summary>
    public partial class ScoreModeWindow : Window
    {
        List<Flashcard> list;
        List<Flashcard> IncorectList = new List<Flashcard>();
        int idx = 0;
        int correct = 0;
        Brush ButtonBrush;
        Brush red = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAE5555"));
        Brush green = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4DA478"));

        public ScoreModeWindow(List<Flashcard> list)
        {
            this.list = list;
            InitializeComponent();
            ShowAnotherFlashcardScore();

        }

        public void ShowAnotherFlashcardScore()
        {
            if (list != null && list.Count != 0 && idx < list.Count)
                MainTextScoreMode.Text = list[idx].Question;
            else
            {
                if (IncorectList.Count > 0)
                {
                    var result = MessageBox.Show(
                        $"You had {correct.ToString()} out of {list.Count} correct which is {((int)(((float)correct / list.Count) * 100)).ToString()}%\nDo you want to repeat incorect qestions only?",
                        "Confirmation",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        
                        var newWindow = new ScoreModeWindow(IncorectList)
                        {
                            Owner = this.Owner,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        this.Close();
                        newWindow.ShowDialog();
                    }
                }
                else
                {
                    var result = MessageBox.Show("Everything was correct");
                }
                this.Close();

            }


        }
        private void MainCardScoreModeClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (MainTextScoreMode.Text != "")
            {

                if (list[idx].Question == MainTextScoreMode.Text)
                    MainTextScoreMode.Text = list[idx].Answer;
                else
                    MainTextScoreMode.Text = list[idx].Question;
            }
        }
        private void MouseEnterButonEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {

            Button button = sender as Button;
            ButtonBrush = button.Background;

            SolidColorBrush oldSolidColor = button.Background as SolidColorBrush;
            Color Oldcolor = oldSolidColor.Color;
            Color lighter = Color.FromRgb(
                (byte)Math.Max(Oldcolor.R - 30, 30),
                (byte)Math.Max(Oldcolor.G - 30, 30),
                (byte)Math.Max(Oldcolor.B - 30, 30)
            );
            button.Background = new SolidColorBrush(lighter);
        }

        private void MouseLeaveButonEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = ButtonBrush;
        }

        private void ButtonClickCorrectEvent(object sender, RoutedEventArgs e)
        {
            idx++;
            correct++;
            ShowAnotherFlashcardScore();
            wasCorrect.Background = green;
        }
        private void ButtonClickWrongEvent(object sender, RoutedEventArgs e)
        {
            IncorectList.Add(list[idx]);
            idx++;
            ShowAnotherFlashcardScore();
            wasCorrect.Background = red;
        }

    }
}
