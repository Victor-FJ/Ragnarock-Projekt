using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Model
{
    public class QuizPlayer
    {
        private static QuizPlayer _instance = new QuizPlayer();
        public static QuizPlayer Instance
        { get { return _instance; } }

        public List<Quiz> Quizzes { get; set; }

        private QuizPlayer()
        {
            Quizzes = new List<Quiz>();
        }

        
    }
}
