﻿using System;
using System.Collections.Generic;
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
                OnPropertyChanged();
            }
        }

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

        public MainViewModel(NavigationService navService) : this()
        {
            _navigationService = navService;
        }
        public MainViewModel()
        {
            _instance = this;
            NavToPageCommand = new RelayCommandWParam(NavigateToPage);
            NavBackCommand = new RelayCommand(NavigateBack);
            NavForwardCommand = new RelayCommand(NavigateForward);
            LoadFiles();
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
            try
            {
                QuizPlayer.Instance.Quizzes = await PersistencyFacade.LoadQuizzesFromJsonAsync();
            }
            catch (FileNotFoundException)
            {
                MessageDialogHelper.Show("Loading for the first time?", "File not found!");
            }
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