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
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.QuizVictor.Exceptions;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.QuizVictor.View;

namespace RagnarockApp.QuizVictor.ViewModel
{
    public class EditQuistionViewModel : INotifyPropertyChanged
    {
        public Quiz QuizToEdit { get; set; }

        public ObservableCollection<Quistion> QuistionCollection { get; set; }

        private Quistion _selectedQuistion;
        public Quistion SelectedQuistion
        {
            get { return _selectedQuistion; }
            set
            {
                _selectedQuistion = value;
                if (value == null)
                    _selectedQuistion = new Quistion();
                else
                {
                    TheQuistion = _selectedQuistion.TheQuistion;
                    Hint = _selectedQuistion.Hint;
                    AnswerOptions = _selectedQuistion.AnswerOptions;
                    bool[] answerBools = new bool[4];
                    answerBools[_selectedQuistion.Answer] = true;
                    Answer = answerBools;
                }
            }
        }

        public ICommand AddQuistionCommand { get; set; }
        public ICommand UpdateQuistionCommand { get; set; }
        public ICommand DeleteQuistionCommand { get; set; }

        #region EditQuistionProperties

        private string _theQuistion;
        public string TheQuistion
        {
            get { return _theQuistion; }
            set
            {
                _theQuistion = value;
                OnPropertyChanged();
                try
                {
                    SelectedQuistion.TheQuistion = _theQuistion;
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
                }
            }
        }
        public string TheQuistionError { get; set; }


        private string _hint;
        public string Hint
        {
            get { return _hint; }
            set
            {
                _hint = value;
                OnPropertyChanged();
                try
                {
                    SelectedQuistion.Hint = _hint;
                    HintError = "";
                }
                catch (ValueEmptyException exception)
                {
                    HintError = exception.Message;
                }
                finally
                {
                    OnPropertyChanged(nameof(HintError));
                }
            }
        }
        public string HintError { get; set; }

        private string[] _answerOptions;
        public string[] AnswerOptions
        {
            get { return _answerOptions; }
            set
            {
                _answerOptions = value;
                OnPropertyChanged();
                try
                {
                    SelectedQuistion.AnswerOptions = _answerOptions;
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
                }
            }
        }
        public string AnswerOptionsError { get; set; }


        private bool[] _answer;
        public bool[] Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public EditQuistionViewModel()
        {
            QuizToEdit = QuizPlayer.Instance.MarkedQuiz;
            AddQuistionCommand = new RelayCommand(AddQuistion);
            UpdateQuistionCommand = new RelayCommand(UpdateQuistion);
            DeleteQuistionCommand = new RelayCommand(DeleteQuistion);
            
            if (QuizToEdit != null)
                QuistionCollection = new ObservableCollection<Quistion>(QuizToEdit.Quistions);
        }

        #region EditQuistionHandler

        //Functions



        //Actions

        public void AddQuistion()
        {

        }

        public void UpdateQuistion()
        {

        }

        public void DeleteQuistion()
        {

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