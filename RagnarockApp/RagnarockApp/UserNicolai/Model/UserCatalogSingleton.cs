using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.UserNicolai.Model
{
    class UserCatalogSingleton
    {
       public static UserCatalogSingleton _userInstants = new UserCatalogSingleton();
       public  ObservableCollection<User> Users { get; }

        //Property
        public static UserCatalogSingleton StudentInstants
        {
            get { return _userInstants; }
        }

        //Users
        private UserCatalogSingleton()
        {
            Users = new ObservableCollection<User>();
            Users.Add(new User("Grim", 123575, true));
            Users.Add(new User("Mandy", 489653, false));
            Users.Add(new User("Billy", 645872, false));
        }

        //Methods

        public void AddUser(User newUser)
        {
         Users.Add(newUser);
        }

        public void RemoveAt(int index)
        {
            Users.RemoveAt(index);
        }
    }
}
