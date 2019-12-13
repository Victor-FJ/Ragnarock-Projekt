using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Quiz> Quizzes { get; set; }

        private Quiz _selectedQuiz;
        public Quiz SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                OnPropertyChanged();
                ((RelayCommand)PlayQuizCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand PlayQuizCommand { get; set; }

        public PlayQuizViewModel()
        {
            Quizzes = new ObservableCollection<Quiz>();
            foreach (Quiz quiz in QuizPlayer.Instance.Quizzes)
                if (quiz.Quistions.Count > 0)
                    Quizzes.Add(quiz);
            PlayQuizCommand = new RelayCommand(PlayQuiz, QuizIsSelected);
        }

        #region EditQuizHandler

        //Functions

        public bool QuizIsSelected()
        {
            return SelectedQuiz != null;
        }

        //Actions

        public void PlayQuiz()
        {
            QuizPlayer.Instance.MarkedQuiz = SelectedQuiz;
            MainViewModel.Instance.NavigateToPage(typeof(PlayQuistionPage));
        }

        #endregion

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
