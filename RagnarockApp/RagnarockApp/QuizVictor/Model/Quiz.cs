using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Model
{
    public class Quiz
    {
        public string QuizName { get; set; }

        public List<Quistion> Quistions { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Quiz(string quizName)
        {
            QuizName = quizName;
            Quistions = new List<Quistion>();
            CreatedDate = DateTimeOffset.Now;
        }
    }
}
