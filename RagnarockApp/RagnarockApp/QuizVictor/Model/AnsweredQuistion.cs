using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Model
{
    public class AnsweredQuistion
    {
        public Quistion AQuistion { get; set; }
        public int UserAnswer { get; set; }

        public AnsweredQuistion(Quistion aQuistion, int userAnswer)
        {
            AQuistion = aQuistion;
            UserAnswer = userAnswer;
        }
    }
}
