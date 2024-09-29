using System;

namespace AppMusic.Services.Exceptions
{
    class InvoiceNotFountException : ApplicationException
    {
        public InvoiceNotFountException(string Message) : base(Message) { }
    }
}
