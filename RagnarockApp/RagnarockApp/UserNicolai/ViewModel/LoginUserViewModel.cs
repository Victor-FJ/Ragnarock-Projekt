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
        public int UserCode { get; set; }

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

        //Action
        public void Login()
        {
            try
            {
                LoginCatalog.Login(UserName, UserCode);
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

        public void OpretBruger()
        {
            MainViewModel.Instance.NavigateToPage(typeof(EditUserPage));
        }







        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
