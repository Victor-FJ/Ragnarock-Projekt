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
using RagnarockApp.Persistency;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class PlayQuistionViewModel : INotifyPropertyChanged
    {
        public QuizPlayer ThisQuizPlayer { get; set; }
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
                ThisQuizPlayer = QuizPlayer.Instance;
            if (ThisQuizPlayer.CurrentPlaySession != null)
                MarkedQuistion = ThisQuizPlayer.CurrentPlaySession.PlayedQuiz.Quistions[ThisQuizPlayer.MarkedQuistionNo];

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
            return !ThisQuizPlayer.CurrentPlaySession.Used50;
        }

        public bool DisplayHintIsAvail()
        {
            if (_quistionIsCompleted)
                return false;
            return !ThisQuizPlayer.CurrentPlaySession.UsedHint;
        }

        public bool ShowStatisticsIsAvail()
        {
            if (_quistionIsCompleted)
                return false;
            return !ThisQuizPlayer.CurrentPlaySession.UsedStats;
        }

        //Actions

        public async void Continue()
        {
            if (ThisQuizPlayer.CurrentPlaySession.PlayedQuiz.Quistions.Count > ++ThisQuizPlayer.MarkedQuistionNo)
                MainViewModel.Instance.NavigateToPage(typeof(PlayQuistionPage));
            else
            {
                await PersistencyFacade.SaveQuizzesAsJsonAsync(QuizManager.Instance.Quizzes);
                MainViewModel.Instance.NavigateToPage(typeof(ResultQuizPage));
            }
        }

        public void Answer(object answerChoice)
        {
            int answerChoice2 = Int32.Parse((string) answerChoice);
            MarkedQuistion.AnswerStats[answerChoice2]++;
            if (answerChoice2 == MarkedQuistion.Answer)
                ChosenAnswer[answerChoice2] = 2;
            else
                ChosenAnswer[answerChoice2] = 1;
            CorrectAnswer = MarkedQuistion.Answer;
            ThisQuizPlayer.CurrentPlaySession.AnswerQuistions.Add(new AnsweredQuistion(MarkedQuistion, ChosenAnswer));
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
            if (!MainViewModel.Instance.ActiveUser.Administrator)
                ThisQuizPlayer.CurrentPlaySession.Used50 = true;
            ((RelayCommand)HalfAnswerOptCommand).RaiseCanExecuteChanged();
        }

        public void DisplayHint()
        {
            HintBox = MarkedQuistion.Hint;
            OnPropertyChanged(nameof(HintBox));
            if (!MainViewModel.Instance.ActiveUser.Administrator)
                ThisQuizPlayer.CurrentPlaySession.UsedHint = true;
            ((RelayCommand)DisplayHintCommand).RaiseCanExecuteChanged();
        }

        public void ShowStatistics()
        {
            StatsBox = new string[4];
            for (int i = 0; i < 4; i++)
            {
                StatsBox[i] = (MarkedQuistion.AnswerStats[i] / (MarkedQuistion.TotalStats * 0.01)).ToString("N0") + " %";
            }
            OnPropertyChanged(nameof(StatsBox));
            if (!MainViewModel.Instance.ActiveUser.Administrator)
                ThisQuizPlayer.CurrentPlaySession.UsedStats = true;
            ((RelayCommand)ShowStatisticsCommand).RaiseCanExecuteChanged();
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
