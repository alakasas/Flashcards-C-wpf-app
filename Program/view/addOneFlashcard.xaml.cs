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
    /// Interaction logic for addOneFlashcard.xaml
    /// </summary>
    public partial class addOneFlashcard : Window
    {
        private Button sourceButton;
        private List<Flashcard> list;
        private bool rename;
        private int idx;

        public addOneFlashcard(Button button, List<Flashcard> list, bool rename=false, int idx=0)
        {
            InitializeComponent();
            sourceButton = button;
            this.list = list;
            this.rename = rename;
            this.idx = idx;

            if (rename)
            {
                Qes.Text = list[idx].Question;
                Ans.Text = list[idx].Answer;
                AddChangeButton.Content = "Change";
            }
        }

        private async void addFlashcard(object sender, RoutedEventArgs e)
        {
            if (!rename)
            {
                Flashcard newFlascard = new Flashcard();
                newFlascard.Question = Qes.Text;
                newFlascard.Answer = Ans.Text;
                list.Add(newFlascard);

            }
            else
            {
                list[idx].Question = Qes.Text;
                list[idx].Answer = Ans.Text;
            }
            Flashcards flashcards = (Flashcards)sourceButton.Tag;
            flashcards.SetOfFlashcards = list;
            sourceButton.Tag = flashcards;

            this.Close();

        }

    }
}
