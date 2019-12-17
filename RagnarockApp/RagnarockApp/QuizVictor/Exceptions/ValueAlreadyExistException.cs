using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// set the value of a property to a value that is already used by another property
    /// </summary>
    public class ValueAlreadyExistException : Exception
    {
        public ValueAlreadyExistException(string message) 
            : base(message)
        {
        }
    }
}
