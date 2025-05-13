using System;
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
using System.Windows.Shapes;
using Program.models;

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
            showAnotherFlashcardScore();

        }

        public void showAnotherFlashcardScore()
        {
            if (list != null && list.Count != 0 && idx < list.Count)
                MainTextScoreMode.Text = list[idx].Question;
            else
            {
                var result = MessageBox.Show(
                    $"You had {correct.ToString()} out of {list.Count} correct which is {((int)(((float)correct / list.Count) * 100)).ToString()}%\nDo you want to repeat incorect qestions only?",
                    "Confirmation",
                    MessageBoxButton.YesNo);

                this.Close();

                if (result == MessageBoxResult.Yes)
                {
                    var newWindow = new ScoreModeWindow(IncorectList);
                    newWindow.ShowDialog();
                }
                
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
            showAnotherFlashcardScore();
            wasCorrect.Background = green;
        }
        private void ButtonClickWrongEvent(object sender, RoutedEventArgs e)
        {
            IncorectList.Add(list[idx]);
            idx++;
            showAnotherFlashcardScore();
            wasCorrect.Background = red;
        }

    }
}
