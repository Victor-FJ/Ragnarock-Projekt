using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.EventArsen.Model;
using RagnarockApp.EventArsen.View;
using RagnarockApp.Persistency;
using RagnarockApp.QuizVictor.Model;
using RagnarockApp.UserNicolai.View;
using RagnarockApp.QuizVictor.View;
using RagnarockApp.UserNicolai.Model;


namespace RagnarockApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly NavigationService _navigationService;

        private static MainViewModel _instance;
        public static MainViewModel Instance
        { get { return _instance; } }
        
        private User _activeUser;
        public User ActiveUser
        {
            get { return _activeUser; }
            set
            {
                _activeUser = value;
                IsAdmin = (_activeUser.Administrator) ? 1 : 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public int IsAdmin { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        #region Pages

        
        public Type LoginUserPage
        { get { return typeof(LoginUserPage); } }
        public Type EventPage
        { get { return typeof(EventPage); } }
        public Type PlayQuizPage
        { get { return typeof(PlayQuizPage); } }
        public Type EditUserPage
        { get { return typeof(EditUserPage); } }
        public Type CreateUserPage
        { get { return typeof(CreateUserPage); } }
        public Type EditEventPage
        { get { return typeof(EditEventPage); } }
        public Type EditQuizPage
        { get { return typeof(EditQuizPage); } }
        

        #endregion

        public ICommand NavToPageCommand { get; set; }
        public ICommand NavBackCommand { get; set; }
        public ICommand NavForwardCommand { get; set; }

        public MainViewModel(NavigationService navService)
        {
            _navigationService = navService;
            _instance = this;
            NavToPageCommand = new RelayCommandWParam(NavigateToPage);
            NavBackCommand = new RelayCommand(NavigateBack);
            NavForwardCommand = new RelayCommand(NavigateForward);
            LoadFiles();
            ActiveUser = new User("Gæst", 0, false, "Gæst", "");
        }

        #region MainHandler

        public void NavigateToPage(object pageType)
        {
            _navigationService.Navigate((Type) pageType);
        }

        public void NavigateBack()
        {
            _navigationService.GoBack();
        }

        public void NavigateForward()
        {
            _navigationService.GoForward();
        }

        #endregion

        private async void LoadFiles()
        {
            string message = "";

            //Loading quizzes
            try
            {
                List<Quiz> quizzes = await PersistencyFacade.LoadQuizzesFromJsonAsync();
                if (quizzes == null)
                    throw new FileLoadException();
                QuizManager.Instance.Quizzes = quizzes;
            }
            catch (FileLoadException)
            {
                message += "\nQuiz filen er tom";
            }
            catch (FileNotFoundException)
            {
                message += "\nQuiz filen blev ikke fundet";
            }

            //Loading users
            try
            {
                ObservableCollection<User> users = await PersistencyFacade.LoadUsersFromJsonAsync();
                if (users == null)
                    throw new FileLoadException();
                UserCatalogSingleton.UserInstants.Users = users;
            }
            catch (FileLoadException)
            {
                message += "\nBruger filen er tom";
            }
            catch (FileNotFoundException)
            {
                message += "\nBruger filen blev ikke fundet";
            }

            //Loading events
            try
            {
                ObservableCollection<Event> events = await PersistencyFacade.LoadEventsFromJsonAsync();
                if (events == null)
                    throw new FileLoadException();
                EventManagerSingleton.Instance.Events = events;
            }
            catch (FileLoadException)
            {
                message += "\nEvent filen er tom";
            }
            catch (FileNotFoundException)
            {
                message += "\nEvent filen blev ikke fundet";
            }
            if (message != "")
                MessageDialogHelper.Show($"Følgende fejl fandt sted:{message}\n\nVed at interagere med appen overskrives gamle filer", "Filen loadede ikke");
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