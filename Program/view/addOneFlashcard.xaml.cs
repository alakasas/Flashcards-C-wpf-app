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
        public addOneFlashcard(Button button, List<Flashcard> list)
        {
            InitializeComponent();
            sourceButton = button;
            this.list = list;
        }

        private async void addFlashcard(object sender, RoutedEventArgs e)
        {
            
            Flashcard newFlascard = new Flashcard();
            newFlascard.Question = Qes.Text;
            newFlascard.Answer = Ans.Text;
            Flashcards flashcards = (Flashcards) sourceButton.Tag;

            flashcards.SetOfFlashcards.Add(newFlascard);

            if (list != null) 
                list.Add(newFlascard);

            sourceButton.Tag = flashcards;



            this.Close();

        }
    }
}
