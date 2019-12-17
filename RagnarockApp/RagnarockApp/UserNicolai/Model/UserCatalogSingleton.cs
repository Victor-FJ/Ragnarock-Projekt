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
    public class UserCatalogSingleton
    {
        private static UserCatalogSingleton _userInstants = new UserCatalogSingleton();

        public ObservableCollection<User> Users
        { get; set; }

        //Property
        public static UserCatalogSingleton UserInstants
        {
            get { return _userInstants; }
        }

        //Users
        /// <summary>
        /// Kan opdatere ændringer i Users listen
        /// </summary>
        private UserCatalogSingleton()
        {
            Users = new ObservableCollection<User>();
            Users.Add(new User("Nicolai", 123456, true, "nico", "123456"));
        }

        //Methods

        public void AddUser(User newUser)
        {
            Validate(newUser);
            Users.Add(newUser);
        }

        public void RemoveAt(int index)
        {
            Users.RemoveAt(index);
        }


        /// <summary>
        /// Bruges til at identificere om brugernavn og Password er korrekt
        /// </summary>
        /// <param name="userName">Viser om brugernavn findes</param>
        /// <param name="userCode">Viser om Password findes</param>
        /// <returns> Retunere en bruger hvis den findes</returns>
        public User Login(string userName, string userCode)
        {
            if (String.IsNullOrWhiteSpace(userName))
                throw new EmptyInputException("Du har ikke skrevet noget i Brugernavn");
            foreach (User user in Users)
                if (user.UserName == userName)
                    if (user.Code == userCode)
                        return user;
                    else
                        throw new PasswordException("Koden er ikke korrekt");
            throw new UserNameException("Brugernavnet passer ikke");
        }

        private void Validate(User userAdd)
        {
            if (String.IsNullOrWhiteSpace(userAdd.UserName))
                throw new EmptyInputException("Du har ikke skrevet noget i Brugernavn");
            if (userAdd.Id.ToString().Length != 6) 
                throw new IdExceptions("Der er ikke de rette antal cifre. Du har skrevet " + userAdd.Id.ToString().Length + " cifre");
            foreach (User user in Users)
                if (user.UserName == userAdd.UserName)
                    throw new UserNameAlreadyUsedException("Brugernavn ikke tilgængeligt");

        }

    }
}
