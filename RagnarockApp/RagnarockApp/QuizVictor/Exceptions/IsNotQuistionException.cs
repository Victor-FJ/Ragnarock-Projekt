using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// set the quistion of a TheQuistionInput to a value without the '?'
    /// </summary>
    public class IsNotQuistionException : Exception
    {
        public IsNotQuistionException(string message) 
            : base(message)
        {
        }
    }
}
