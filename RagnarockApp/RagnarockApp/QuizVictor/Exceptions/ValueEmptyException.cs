﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Exceptions
{
    /// <summary>
    /// This exception is thrown in case it is attempted to
    /// set a quistion property to be empty
    /// </summary>
    public class ValueEmptyException : Exception
    {
        public ValueEmptyException(string message) 
            : base(message)
        {
        }
    }
}
