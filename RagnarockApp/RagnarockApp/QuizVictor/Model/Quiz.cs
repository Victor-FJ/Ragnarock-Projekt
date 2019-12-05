using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Model
{
    public class Quiz
    {
        public List<Quistion> Quistions { get; set; }

        public string QuizName { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Quiz(string quizName)
        {
            QuizName = quizName;
            CreatedDate = DateTimeOffset.Now;
        }
    }
}
