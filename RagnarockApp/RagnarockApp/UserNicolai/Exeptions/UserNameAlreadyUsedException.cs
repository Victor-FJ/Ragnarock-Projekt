using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.UserNicolai.Exeptions
{
    class UserNameAlreadyUsedException: Exception
    {
        public UserNameAlreadyUsedException(string message) : base(message)
        {

        }
    }
}
