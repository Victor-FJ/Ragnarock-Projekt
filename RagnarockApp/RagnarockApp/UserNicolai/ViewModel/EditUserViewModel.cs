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
    class EditUserViewModel: INotifyPropertyChanged
    {
        //Instance
        private User _selectedUser;
        private int _selectedIndex;
        private ICommand _removeCommand;
       


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



        /// <summary>
        /// En Kommando der fjerner en bruger fra listen
        /// </summary>
        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
            set { _removeCommand = value; }
        }

        /// <summary>
        /// viser når metoden forandre sig
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex;}
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                ((RelayCommand) _removeCommand).RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// notificere når properties ændres
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// En metode der fjerner en bruger
        /// </summary>
        public void RemoveUser()
        {
            UserCatalog.RemoveAt(SelectedIndex);
            Save();
        }

        //Func
        /// <summary>
        /// Sørger for man ikke sletter bruger før man har trykket på brugeren
        /// </summary>
        /// <returns></returns>
        public bool SelectedIndexIsNotSet()
        {
            return SelectedIndex != -1;
        }


        //Async
        /// <summary>
        /// sørger for brugere bliver gemt
        /// </summary>
        private async void Save()
        {
            await PersistencyFacade.SaveUsersAsJsonAsync(UserCatalogSingleton.UserInstants.Users);
        }

        //Constructor
        /// <summary>
        /// Initialisere kommandoerne
        /// </summary>
        public EditUserViewModel()
        {
            _removeCommand = new RelayCommand(RemoveUser,SelectedIndexIsNotSet);
            UserCatalog = UserCatalogSingleton.UserInstants;
        }
    }
}
