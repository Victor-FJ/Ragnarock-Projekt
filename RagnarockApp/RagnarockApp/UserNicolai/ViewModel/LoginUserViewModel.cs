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
using RagnarockApp.UserNicolai.Exeptions;
using RagnarockApp.UserNicolai.Model;
using RagnarockApp.UserNicolai.View;

namespace RagnarockApp.UserNicolai.ViewModel
{
    class LoginUserViewModel:INotifyPropertyChanged
    {
        //Property
        public string UserName { get; set; }
        public string UserCode { get; set; }

        public UserCatalogSingleton LoginCatalog { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand OpretBrugerCommand { get; set; }
        //Constructor

        public LoginUserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            OpretBrugerCommand = new RelayCommand(OpretBruger);
            LoginCatalog = UserCatalogSingleton.UserInstants;
        }

        public string ConfirmText { get; set; }

        //Action
        /// <summary>
        /// Bruges til at identificere hvem der er logget ind som bruger
        /// </summary>
        public void Login()
        {
            try
            {
                User activeUser = LoginCatalog.Login(UserName, UserCode);
                MainViewModel.Instance.ActiveUser = activeUser;
                ConfirmText = "Du er nu logget ind";
                OnPropertyChanged(nameof(ConfirmText));
            }
            catch (EmptyInputException eiex)
            {
                MessageDialogHelper.Show(eiex.Message, "Du har fået en EiexException");
            }
            catch (UserNameException upex)
            {
                MessageDialogHelper.Show(upex.Message, "Fejl i Login");
            }
            catch(PasswordException puex)
            {
                MessageDialogHelper.Show(puex.Message,"Fejl i Login");
            }
           
        }
        /// <summary>
        /// Opretter bruger
        /// </summary>
        public void OpretBruger()
        {
            MainViewModel.Instance.NavigateToPage(typeof(CreateUserPage));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
