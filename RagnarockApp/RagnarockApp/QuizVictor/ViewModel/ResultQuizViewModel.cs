using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RagnarockApp.Common;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class ResultQuizViewModel
    {
        public PlaySession ThisPlaySession { get; set; }

        public int NoOfCorrectAnswers { get; set; }

        public int IsAllCorrect { get; set; }

        public ICommand PlayAgainCommand { get; set; }
        public ICommand PlayAnotherCommand { get; set; }

        public ResultQuizViewModel()
        {
            if (QuizPlayer.Instance != null)
            {
                ThisPlaySession = QuizPlayer.Instance.CurrentPlaySession;
                if (ThisPlaySession != null)
                {
                    foreach (AnsweredQuistion answeredQuistion in ThisPlaySession.AnswerQuistions)
                        if (answeredQuistion.UserAnswer[answeredQuistion.AQuistion.Answer] != 0)
                            NoOfCorrectAnswers++;
                    if (NoOfCorrectAnswers == ThisPlaySession.PlayedQuiz.Quistions.Count)
                        IsAllCorrect = 0;
                    else
                        IsAllCorrect = 1;
                }
            }
            PlayAgainCommand = new RelayCommand(PlayQuizAgain);
            PlayAnotherCommand = new RelayCommand(PlayMore);
        }

        #region PlayQuizHandler

        //Functions

        //Actions

        public void PlayQuizAgain()
        {
            QuizPlayer.Instance.CurrentPlaySession.AnswerQuistions = new List<AnsweredQuistion>();
            QuizPlayer.Instance.CurrentPlaySession.Used50 = false;
            QuizPlayer.Instance.CurrentPlaySession.UsedHint = false;
            QuizPlayer.Instance.CurrentPlaySession.UsedStats = false;
            QuizPlayer.Instance.MarkedQuistionNo = 0;
            MainViewModel.Instance.NavigateToPage(typeof(PlayQuistionPage));
        }

        public void PlayMore()
        {
            MainViewModel.Instance.NavigateToPage(typeof(PlayQuizPage));
        }

        #endregion
    }
}
