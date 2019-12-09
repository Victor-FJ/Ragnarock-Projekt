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
using RagnarockApp.QuizVictor.Exceptions;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class EditQuizViewModel : INotifyPropertyChanged
    {
        public QuizPlayer QuizPlayer { get; set; }

        private Quiz _selectedQuiz;
        public Quiz SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                if (value == null)
                {
                    QuizNameToEdit = "";
                    EnableEdit = false;
                }
                else
                {
                    QuizNameToEdit = value.QuizName;
                    EnableEdit = true;
                }
                OnPropertyChanged(nameof(QuizNameToEdit));
                OnPropertyChanged(nameof(EnableEdit));
                ((RelayCommand)ModifyNameCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ModifyQuizCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteQuizCommand).RaiseCanExecuteChanged();
            }
        }

        public string QuizNameToCreate { get; set; }
        public string ErrorToCreate { get; set; }
        public string QuizNameToEdit { get; set; }
        public string ErrorToEdit { get; set; }
        public bool EnableEdit { get; set; }

        public ICommand CreateQuizCommand { get; set; }
        public ICommand ModifyNameCommand { get; set; }
        public ICommand ModifyQuizCommand { get; set; }
        public ICommand DeleteQuizCommand { get; set; }

        public EditQuizViewModel()
        {
            QuizPlayer = QuizPlayer.Instance;
            CreateQuizCommand = new RelayCommand(CreateQuiz);
            ModifyNameCommand = new RelayCommand(ModifyName, QuizIsSelected);
            ModifyQuizCommand = new RelayCommand(ModifyQuiz, QuizIsSelected);
            DeleteQuizCommand = new RelayCommand(DeleteQuiz, QuizIsSelected);
            SelectedQuiz = null;
        }

        #region EditQuizHandler

        //Functions

        public bool QuizIsSelected()
        {
            return SelectedQuiz != null;
        }

        //Actions

        public void CreateQuiz()
        {
            try
            {
                QuizPlayer.CreateQuiz(QuizNameToCreate);
                //Goto new page
            }
            catch (ValueEmptyException exception)
            {
                ErrorToCreate = exception.Message;
            }
            catch (NameAlreadyExistException exception)
            {
                ErrorToCreate = exception.Message;
            }
            finally
            {
                OnPropertyChanged(nameof(ErrorToCreate));
            }
        }

        public async void ModifyName()
        {
            try
            {
                QuizPlayer.ModifyQuizName(SelectedQuiz.QuizName, QuizNameToEdit, false);
                if (await MessageDialogHelper.ShowWInput($"Are you sure you want to change the name of the quiz?\n({SelectedQuiz.QuizName}) -> ({QuizNameToEdit})", "Change Name?"))
                    QuizPlayer.ModifyQuizName(SelectedQuiz.QuizName, QuizNameToEdit, true);
                MainViewModel.Instance.NavigateToPage(typeof(EditQuizPage));
            }
            catch (ValueEmptyException exception)
            {
                ErrorToEdit = exception.Message;
            }
            catch (NameAlreadyExistException exception)
            {
                ErrorToEdit = exception.Message;
            }
            finally
            {
                OnPropertyChanged(nameof(ErrorToEdit));
            }
        }

        public void ModifyQuiz()
        {
            QuizPlayer.MarkedQuiz = SelectedQuiz;
            //Goto new page
        }

        public async void DeleteQuiz()
        {
            if (await MessageDialogHelper.ShowWInput($"Are you sure you want to delete the quiz?\n({SelectedQuiz.QuizName})", "Delete quiz?"))
                QuizPlayer.DeleteQuiz(SelectedQuiz.QuizName, true);
            MainViewModel.Instance.NavigateToPage(typeof(EditQuizPage));
            OnPropertyChanged(nameof(ErrorToEdit));
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