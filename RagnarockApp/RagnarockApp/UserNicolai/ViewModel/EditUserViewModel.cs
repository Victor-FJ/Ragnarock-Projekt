﻿using System;
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
        public static UserCatalogSingleton UserCatalog { get; set; }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

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
                SelectedIndex = value;
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


        //Ation

        public void AddUser()
        {
            UserCatalog.AddUser(SelectedUser);
        }

        public void RemoveUser()
        {
            UserCatalog.RemoveAt(SelectedIndex);
        }

    }
}
