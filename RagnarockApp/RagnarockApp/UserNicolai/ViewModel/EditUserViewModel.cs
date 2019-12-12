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
using RagnarockApp.UserNicolai.Model;

namespace RagnarockApp.UserNicolai.ViewModel
{
    class EditUserViewModel: INotifyPropertyChanged
    {
        //Instance
        private User _selectedUser;
        private int _selectedIndex;
        private ICommand _removeCommand;
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


        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        /// <summary>
        /// En Kommando der fjerner en bruger fra listen
        /// </summary>
        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
            set { _removeCommand = value; }
        }


        public int SelectedIndex
        {
            get { return _selectedIndex;}
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
                ((RelayCommand) _removeCommand).RaiseCanExecuteChanged();
                ((RelayCommand)_addCommand).RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Action
        /// <summary>
        /// En metode der opretter en ny bruger
        /// </summary>
        public void AddUser()
        {
            UserCatalog.AddUser(SelectedUser);
        }

        /// <summary>
        /// En metode der fjerner en bruger
        /// </summary>
        public void RemoveUser()
        {
            UserCatalog.RemoveAt(SelectedIndex);
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

        //Constructor
        /// <summary>
        /// Initialisere kommandoerne
        /// </summary>
        public EditUserViewModel()
        {
            _addCommand = new RelayCommand(AddUser);
            _removeCommand = new RelayCommand(RemoveUser,SelectedIndexIsNotSet);
            UserCatalog = UserCatalogSingleton.UserInstants;
        }
    }
}
