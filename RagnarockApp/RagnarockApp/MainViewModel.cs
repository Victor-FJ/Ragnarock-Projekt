﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        public User ActiveUser { get; set; }

        #region Pages

        public Type EditUserPage
        { get { return typeof(EditUserPage); } }
        public Type LoginUserPage
        { get { return typeof(LoginUserPage); } }
        public Type EditEventPage
        { get { return typeof(EditEventPage); } }
        public Type EventPage
        { get { return typeof(EventPage); } }
        public Type EditQuizPage
        { get { return typeof(EditQuizPage); } }
        public Type PlayQuizPage
        { get { return typeof(PlayQuizPage); } }

        #endregion

        public ICommand NavToPageCommand { get; set; }

        public MainViewModel(NavigationService navService) : this()
        {
            _navigationService = navService;
        }
        public MainViewModel()
        {
            _instance = this;
            NavToPageCommand = new RelayCommandWParam(NavigateToPage);
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