using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// set the name of a quiz to name that is already used by another quiz
    /// </summary>
    public class NameAlreadyExistException : Exception
    {
        public NameAlreadyExistException(string message) 
            : base(message)
        {
        }
    }
}
