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

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public QuizPlayer QuizPlayerInstance { get; set; }

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
            QuizPlayerInstance = QuizPlayer.Instance;
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
            QuizPlayerInstance.MarkedQuiz = SelectedQuiz;
            MainViewModel.Instance.NavigateToPage(typeof());
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
