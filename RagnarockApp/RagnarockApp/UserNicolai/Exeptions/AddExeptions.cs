using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.UserNicolai.Exeptions
{
    /// <summary>
    /// Denne constructor bruges til at sige når man ikke har lavet 6 cifre i Id
    /// </summary>
    public class AddExeptions: Exception
    {
        public AddExeptions(string message) : base(message)
        {

        }



    }
}
