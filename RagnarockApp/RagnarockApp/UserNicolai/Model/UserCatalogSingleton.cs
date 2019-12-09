using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.Common;
using RagnarockApp.UserNicolai.Exeptions;

namespace RagnarockApp.UserNicolai.Model
{
    class UserCatalogSingleton
    {
       private static UserCatalogSingleton _userInstants = new UserCatalogSingleton();
       public  ObservableCollection<User> Users { get; }

        //Property
        public static UserCatalogSingleton UserInstants
        {
            get { return _userInstants; }
        }

        //Users
        private UserCatalogSingleton()
        {
            Users = new ObservableCollection<User>();
            Users.Add(new User("Grim", 123575, true, "Servant", 6852147));
            Users.Add(new User("Mandy", 489653, false, "Leader", 11112011));
            Users.Add(new User("Billy", 645872, false, "Brainless", 11111111));
        }

        //Methods

        public void AddUser(User newUser)
        {
        
         try
         {
             Validate(newUser);
             Users.Add(newUser);
         }
         catch (AddExceptions adex)
         {
             MessageDialogHelper.Show( adex.Message, "Du har fået en AddExeption");
         }
        }

        public void RemoveAt(int index)
        {
            Users.RemoveAt(index);
        }


        //Exeptions
        public void Add(int index, User userAdd)
        {
           
        }

        private void Validate( User userAdd)
        {
            if (userAdd.Id.ToString().Length != 6)
            {
                throw new AddExceptions("Der er ikke de rette antal cifre. Du har skrevet " + userAdd.Id.ToString().Length + " cifre");
            }
        }

    }
}
