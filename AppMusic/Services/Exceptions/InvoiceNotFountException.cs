using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Services.Exceptions
{
    class InvoiceNotFountException : ApplicationException
    {
        public InvoiceNotFountException(string Message) : base(Message) { }
    }
}
