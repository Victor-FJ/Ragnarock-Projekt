using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.UserNicolai.Model;

namespace RagnarockApp.QuizVictor.Model
{
    public class PlaySession
    {
        public Quiz PlayedQuiz { get; set; }
        public User PlayedUser { get; set; }
        public List<int> AnswerList { get; set; }

        public bool Used50 { get; set; }
        public bool UsedHint { get; set; }
        public bool UsedStats { get; set; }

        public PlaySession(Quiz playedQuiz, User playedUser)
        {
            PlayedQuiz = playedQuiz;
            PlayedUser = playedUser;
            AnswerList = new List<int>();
        }
    }
}
