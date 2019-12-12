using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.Persistency;
using RagnarockApp.QuizVictor.Exceptions;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class EditQuistionViewModel : INotifyPropertyChanged
    {
        public Quiz QuizToEdit { get; set; }
        public ObservableCollection<Quistion> QuistionCollection { get; set; }


        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        private Quistion _editedQuistion = new Quistion();
        
        private Quistion _selectedQuistion;
        public Quistion SelectedQuistion
        {
            get { return _selectedQuistion; }
            set
            {
                _selectedQuistion = value;
                if (value == null)
                {
                    TheQuistionInput = "";
                    HintInput = "";
                    AnswerOptionsInput = new string[4];
                    AnswerInput = new bool[4];
                    TheQuistionError = "*";
                    HintError = "*";
                    AnswerOptionsError = "*";
                    AnswerError = "*";
                    OnPropertyChanged(nameof(TheQuistionError));
                    OnPropertyChanged(nameof(HintError));
                    OnPropertyChanged(nameof(AnswerOptionsError));
                    OnPropertyChanged(nameof(AnswerError));
                }
                else
                {
                    TheQuistionInput = _selectedQuistion.TheQuistion;
                    HintInput = _selectedQuistion.Hint;
                    AnswerOptionsInput = _selectedQuistion.AnswerOptions;
                    bool[] answerBools = new bool[4];
                    answerBools[_selectedQuistion.Answer] = true;
                    AnswerInput = answerBools;
                }
                ((RelayCommand)UpdateQuistionCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteQuistionCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddQuistionCommand { get; set; }
        public ICommand UpdateQuistionCommand { get; set; }
        public ICommand DeleteQuistionCommand { get; set; }
        public ICommand SaveQuizCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        #region EditQuistionProperties

        private string _theQuistionInput;
        public string TheQuistionInput
        {
            get { return _theQuistionInput; }
            set
            {
                _theQuistionInput = value;
                OnPropertyChanged();
                try
                {
                    _editedQuistion.TheQuistion = _theQuistionInput;
                    TheQuistionError = "";
                }
                catch (ValueEmptyException exception)
                {
                    TheQuistionError = exception.Message;
                }
                catch (IsNotQuistionException exception)
                {
                    TheQuistionError = exception.Message;
                }
                finally
                {
                    OnPropertyChanged(nameof(TheQuistionError));
                    ((RelayCommand)AddQuistionCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateQuistionCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string TheQuistionError { get; set; }


        private string _hintInput;
        public string HintInput
        {
            get { return _hintInput; }
            set
            {
                _hintInput = value;
                OnPropertyChanged();
                try
                {
                    _editedQuistion.Hint = _hintInput;
                    HintError = "";
                }
                catch (ValueEmptyException exception)
                {
                    HintError = exception.Message;
                }
                finally
                {
                    OnPropertyChanged(nameof(HintError));
                    ((RelayCommand)AddQuistionCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateQuistionCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string HintError { get; set; }

        private string[] _answerOptionsInput;
        public string[] AnswerOptionsInput
        {
            get { return _answerOptionsInput; }
            set
            {
                _answerOptionsInput = value;
                OnPropertyChanged();
                try
                {
                    _editedQuistion.AnswerOptions = _answerOptionsInput;
                    AnswerOptionsError = "";
                }
                catch (ValueEmptyException exception)
                {
                    AnswerOptionsError = exception.Message;
                }
                catch (ValueAlreadyExistException exception)
                {
                    AnswerOptionsError = exception.Message;
                }
                finally
                {
                    OnPropertyChanged(nameof(AnswerOptionsError));
                    ((RelayCommand)AddQuistionCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateQuistionCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string AnswerOptionsError { get; set; }


        private bool[] _answerInput;
        public bool[] AnswerInput
        {
            get { return _answerInput; }
            set
            {
                _answerInput = value;
                OnPropertyChanged();
                try
                {
                    for (int i = 0; i < AnswerInput.Length; i++)
                        if (AnswerInput[i])
                            _editedQuistion.Answer = i;
                    AnswerError = "";
                }
                catch (IndexOutOfRangeException exception)
                {
                    AnswerError = exception.Message;
                }
                finally
                {
                    OnPropertyChanged(nameof(AnswerError));
                    ((RelayCommand)AddQuistionCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateQuistionCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string AnswerError { get; set; }

        #endregion

        public EditQuistionViewModel()
        {
            QuizToEdit = QuizPlayer.Instance.MarkedQuiz;
            AddQuistionCommand = new RelayCommand(AddQuistion, QuistionIsValid);
            UpdateQuistionCommand = new RelayCommand(UpdateQuistion, QuistionIsSelectedAndValid);
            DeleteQuistionCommand = new RelayCommand(DeleteQuistion, QuistionIsSelected);
            SaveQuizCommand = new RelayCommand(SaveQuiz);
            CancelCommand = new RelayCommand(Cancel);
            
            if (QuizToEdit != null)
                QuistionCollection = new ObservableCollection<Quistion>(QuizToEdit.Quistions);
            SelectedQuistion = null;
        }

        #region EditQuistionHandler

        //Functions

        public bool QuistionIsSelected()
        {
            return _selectedQuistion != null;
        }
        public bool QuistionIsValid()
        {
            bool theQuistionValid = _editedQuistion.TheQuistion == TheQuistionInput;
            bool hintValid = _editedQuistion.Hint == HintInput;
            bool answerOptValid = _editedQuistion.AnswerOptions == AnswerOptionsInput;
            bool answer = AnswerInput[_editedQuistion.Answer];
            return theQuistionValid && hintValid && answerOptValid && answer;
        }

        public bool QuistionIsSelectedAndValid()
        {
            return QuistionIsSelected() && QuistionIsValid();
        }

        //Actions

        public void AddQuistion()
        {
            QuistionCollection.Add(_editedQuistion);
            _editedQuistion = new Quistion();
            SelectedIndex = QuistionCollection.Count - 1;
        }

        public void UpdateQuistion()
        {
            int index = SelectedIndex;
            QuistionCollection[index] = _editedQuistion;
            _editedQuistion = new Quistion();
            SelectedIndex = index;
        }

        public void DeleteQuistion()
        {
            QuistionCollection.RemoveAt(SelectedIndex);
        }

        public async void SaveQuiz()
        {
            QuizToEdit.Quistions = QuistionCollection.ToList();
            await PersistencyFacade.SaveQuizzesAsJsonAsync(QuizPlayer.Instance.Quizzes);
            MainViewModel.Instance.NavigateBack();
        }

        public void Cancel()
        {
            MainViewModel.Instance.NavigateBack();
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