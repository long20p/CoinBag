using System;
using System.Collections.Generic;
using System.Text;

namespace CoinBag.Exceptions
{
    public class FileExistException : Exception
    {
	    public FileExistException(string message) : base(message)
	    { 
	    }
    }
}
