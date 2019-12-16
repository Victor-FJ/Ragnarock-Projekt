using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RagnarockApp.Annotations;
using RagnarockApp.Common;
using RagnarockApp.Persistency;
using RagnarockApp.UserNicolai.Exeptions;
using RagnarockApp.UserNicolai.Model;

namespace RagnarockApp.UserNicolai.ViewModel
{
    public class CreateUserViewModel : INotifyPropertyChanged
    {


        //Instance
        private User _selectedUser;
        private ICommand _addCommand;
        private int _isAdmin;

        //Property
        public UserCatalogSingleton UserCatalog { get; set; }


        public int IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value; 
                OnPropertyChanged();
            }
        }


        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if (value == null)
                    _selectedUser = new User();
                OnPropertyChanged();
            }
        }

        public UserCatalogSingleton CreateCatalog { get; set; }

        public string ConfirmText2 { get; set; }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        //Action
        /// <summary>
        /// En metode der opretter en ny bruger
        /// </summary>
        public void AddUser()
        {
            try
            {
                UserCatalog.AddUser(SelectedUser);
                Save();
                ConfirmText2 = "Du er nu oprettet som bruger";
                OnPropertyChanged(nameof(ConfirmText2));
            }
            catch (IdExceptions adex)
            {
                MessageDialogHelper.Show(adex.Message, "Du har fået en AddExeption");
            }
            catch (EmptyInputException eiex)
            {
                MessageDialogHelper.Show(eiex.Message, "Du har fået en EiexException");
            }
            catch (UserNameAlreadyUsedException adex)
            {
                MessageDialogHelper.Show(adex.Message, "Du har fået en UserNameException");
            }
        }

        //Constructor
        /// <summary>
        /// Initialisere kommandoerne
        /// </summary>
        public CreateUserViewModel()
        {
            _addCommand = new RelayCommand(AddUser);
            UserCatalog = UserCatalogSingleton.UserInstants;
            SelectedUser = new User();
            if (MainViewModel.Instance != null)
                IsAdmin = MainViewModel.Instance.IsAdmin;
        }


        //Async
        /// <summary>
        /// sørger for brugere bliver gemt
        /// </summary>
        private async void Save()
        {
            await PersistencyFacade.SaveUsersAsJsonAsync(UserCatalogSingleton.UserInstants.Users);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
