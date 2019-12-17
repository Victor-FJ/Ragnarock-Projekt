using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.EventArsen.Exceptions
{
    public class DateNotAvailableException:Exception
    {
        public DateNotAvailableException(string message) : base(message)
        {

        }
    }
}
