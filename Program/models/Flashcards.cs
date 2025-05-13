using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Program.models
{
    public class Flashcards
    {

        public Flashcards(string path) { 
            this.Path = path;
            this.SetOfFlashcards = new List<Flashcard>();
        }
        public List<Flashcard> SetOfFlashcards {  get; set; }
        public string Path { get; set; }

    }
}
