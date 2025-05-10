using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Program.models;

namespace Program.functions
{
    public static class FlashcardsRW
    {
        public static Flashcards ReadFlashCards(string filepath)
        {
            Flashcards result = new Flashcards(filepath);
            bool question = true;
            string line;
            Flashcard oneFlashcard = new Flashcard();

            using (StreamReader reader = new StreamReader(filepath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (question) { 

                        oneFlashcard = new Flashcard();
                        oneFlashcard.Question = line;
                        question = false;
                    }
                    else {
                        oneFlashcard.Answer = line;
                        result.SetOfFlashcards.Add(oneFlashcard);
                        question = true;
                    }
                }
            }
            return result;
        }

        public static void WriteFlashCards(Flashcards flashcards)
        {
            using (StreamWriter writer = new StreamWriter(flashcards.Path, append:false))
            {
                foreach (Flashcard flashcard in flashcards.SetOfFlashcards){
                    writer.WriteLine(flashcard.Question);
                    writer.WriteLine(flashcard.Answer);
                }
            }
            return;
        }
    }
}
