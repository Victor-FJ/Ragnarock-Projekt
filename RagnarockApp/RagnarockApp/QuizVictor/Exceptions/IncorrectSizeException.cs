using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// set the AnswerOptions to an array with a size other than 4
    /// </summary>
    public class IncorrectSizeException : Exception
    {
        public IncorrectSizeException(string message) 
            : base(message)
        {
        }
    }
}
