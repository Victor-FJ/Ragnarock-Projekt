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


        //Property
        public UserCatalogSingleton UserCatalog { get; set; }

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
            UserCatalog.AddUser(SelectedUser);
            Save();
            ConfirmText2 = "Du er nu oprettet som bruger";
            OnPropertyChanged(nameof(ConfirmText2));
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
