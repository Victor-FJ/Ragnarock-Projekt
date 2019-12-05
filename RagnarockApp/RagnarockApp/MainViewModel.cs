using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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


namespace RagnarockApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static MainViewModel _instance;
        public static MainViewModel Instance
        { get { return _instance; } }

        public Page FramePage { get; set; }

        #region Pages

        public Page EditUserPage
        { get { return new EditUserPage(); } }
        public Page LoginUserPage
        { get { return new LoginUserPage(); } }
        public Page EditEventPage
        { get { 
                return new EditEventPage(); } }
        public Page EventPage
        { get { 
                return new EventPage(); } }
        public Page EditQuizPage
        { get { return new EditQuizPage(); } }
        public Page PlayQuizPage
        { get { return new PlayQuizPage(); } }

        #endregion

        public ICommand NavToPageCommand { get; set; }

        public MainViewModel()
        {
            _instance = this;
            NavToPageCommand = new RelayCommandWParam(NavigateToPage);
        }

        #region MainHandler

        public void NavigateToPage(object page)
        {
            FramePage = (Page)page;
            OnPropertyChanged(nameof(FramePage));
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