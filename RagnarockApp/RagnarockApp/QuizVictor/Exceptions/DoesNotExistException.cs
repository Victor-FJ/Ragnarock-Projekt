using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// find a object that does not exist
    /// </summary>
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(string message) 
            : base(message)
        {
        }
    }
}
