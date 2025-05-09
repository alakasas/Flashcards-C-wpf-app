using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.models;

namespace Program.functions
{
    public static class FlashcardsRW
    {
        public static HashSet<Flashcard> ReadFlashCards(string filepath)
        {
            var result = new HashSet<Flashcard>();
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
                        result.Add(oneFlashcard);
                        question = true;
                    }
                }
            }
            return result;
        }

        public static void WriteFlashCards(HashSet<Flashcard> flashcards, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (Flashcard flashcard in flashcards){
                    writer.WriteLine(flashcard.Question);
                    writer.WriteLine(flashcard.Answer);
                }
            }
            return;
        }
    }
}
