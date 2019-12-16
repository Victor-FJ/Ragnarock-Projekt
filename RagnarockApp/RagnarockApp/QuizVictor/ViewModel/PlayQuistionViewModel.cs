using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class PlayQuistionViewModel : INotifyPropertyChanged
    {
        public QuizPlayer QuizPlayer { get; set; }
        public Quistion MarkedQuistion { get; set; }


        private bool _quistionIsCompleted;
        private bool[] _availAnswerOpt = {true, true, true, true};

        public int CorrectAnswer { get; set; }
        public int[] ChosenAnswer { get; set; }

        public string HintBox { get; set; }
        public string[] StatsBox { get; set; }


        public ICommand ContinueCommand { get; set; }

        public ICommand AnswerOpt1Command { get; set; }
        public ICommand AnswerOpt2Command { get; set; }
        public ICommand AnswerOpt3Command { get; set; }
        public ICommand AnswerOpt4Command { get; set; }

        public ICommand HalfAnswerOptCommand { get; set; }
        public ICommand DisplayHintCommand { get; set; }
        public ICommand ShowStatisticsCommand { get; set; }

        public PlayQuistionViewModel()
        {
            if (QuizPlayer.Instance != null)
                QuizPlayer = QuizPlayer.Instance;
            if (QuizPlayer.CurrentPlaySession != null)
                MarkedQuistion = QuizPlayer.CurrentPlaySession.PlayedQuiz.Quistions[QuizPlayer.MarkedQuistionNo];

            ContinueCommand = new RelayCommand(Continue, QuistionIsCompleted);
            AnswerOpt1Command = new RelayCommandWParam(Answer, AnswerOpt1Enabled);
            AnswerOpt2Command = new RelayCommandWParam(Answer, AnswerOpt2Enabled);
            AnswerOpt3Command = new RelayCommandWParam(Answer, AnswerOpt3Enabled);
            AnswerOpt4Command = new RelayCommandWParam(Answer, AnswerOpt4Enabled);
            HalfAnswerOptCommand = new RelayCommand(HalfAnswerOpt, HalfAnswerOptIsAvail);
            DisplayHintCommand = new RelayCommand(DisplayHint, DisplayHintIsAvail);
            ShowStatisticsCommand = new RelayCommand(ShowStatistics, ShowStatisticsIsAvail);

            CorrectAnswer = -1;
            ChosenAnswer = new int[4];
        }

        #region EditQuizHandler

        //Functions

        public bool QuistionIsCompleted()
        {
            return _quistionIsCompleted;
        }

        public bool AnswerOpt1Enabled()
        {
            return _availAnswerOpt[0];
        }

        public bool AnswerOpt2Enabled()
        {
            return _availAnswerOpt[1];
        }

        public bool AnswerOpt3Enabled()
        {
            return _availAnswerOpt[2];
        }

        public bool AnswerOpt4Enabled()
        {
            return _availAnswerOpt[3];
        }

        public bool HalfAnswerOptIsAvail()
        {
            if (_quistionIsCompleted)
                return false;
            return !QuizPlayer.CurrentPlaySession.Used50;
        }

        public bool DisplayHintIsAvail()
        {
            if (_quistionIsCompleted)
                return false;
            return !QuizPlayer.CurrentPlaySession.UsedHint;
        }

        public bool ShowStatisticsIsAvail()
        {
            if (_quistionIsCompleted)
                return false;
            return !QuizPlayer.CurrentPlaySession.UsedStats;
        }

        //Actions

        public void Continue()
        {
            if (QuizPlayer.CurrentPlaySession.PlayedQuiz.Quistions.Count > ++QuizPlayer.MarkedQuistionNo)
                MainViewModel.Instance.NavigateToPage(typeof(PlayQuistionPage));
            //Else
            //Navigation to end page
        }

        public void Answer(object answerChoice)
        {
            int answerChoice2 = Int32.Parse((string) answerChoice);
            QuizPlayer.CurrentPlaySession.AnswerQuistions.Add(new AnsweredQuistion(MarkedQuistion, answerChoice2));
            if (answerChoice2 == MarkedQuistion.Answer)
                ChosenAnswer[answerChoice2] = 2;
            else
                ChosenAnswer[answerChoice2] = 1;
            CorrectAnswer = MarkedQuistion.Answer;
            QuistionIsComplete();
        }

        public void HalfAnswerOpt()
        {
            _availAnswerOpt = new bool[4];
            _availAnswerOpt[MarkedQuistion.Answer] = true;
            Random randomGenarator = new Random();
            int index = randomGenarator.Next(0, 4);
            if (_availAnswerOpt[index] && index < 3)
                _availAnswerOpt[index + 1] = true;
            else if (_availAnswerOpt[index] && index == 3)
                _availAnswerOpt[0] = true;
            else
                _availAnswerOpt[index] = true;
            UpdateAnswerOptions();
        }

        public void DisplayHint()
        {
            HintBox = MarkedQuistion.Hint;
            OnPropertyChanged(nameof(HintBox));
        }

        public void ShowStatistics()
        {

        }

        #endregion

        private void QuistionIsComplete()
        {
            _quistionIsCompleted = true;
            _availAnswerOpt = new bool[4];
            OnPropertyChanged(nameof(ChosenAnswer));
            OnPropertyChanged(nameof(CorrectAnswer));
            ((RelayCommand)ContinueCommand).RaiseCanExecuteChanged();
            ((RelayCommand)HalfAnswerOptCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DisplayHintCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ShowStatisticsCommand).RaiseCanExecuteChanged();
            UpdateAnswerOptions();
        }

        private void UpdateAnswerOptions()
        {
            ((RelayCommandWParam)AnswerOpt1Command).RaiseCanExecuteChanged();
            ((RelayCommandWParam)AnswerOpt2Command).RaiseCanExecuteChanged();
            ((RelayCommandWParam)AnswerOpt3Command).RaiseCanExecuteChanged();
            ((RelayCommandWParam)AnswerOpt4Command).RaiseCanExecuteChanged();
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
