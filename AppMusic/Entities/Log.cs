using System;

namespace AppMusic.Entities
{
    public class Log
    {
        public string Message { get; set; }
        public string ErroMessage { get; set; }
        public string ErrorCode { get; set; }
        public bool Status { get; set; }
        public override string ToString()
        {
            return Status ? SuccessMessage() : ErrorMessage();
        }

        public string SuccessMessage()
        {
            return Message;
        }

        public string ErrorMessage()
        {
            return ErrorCode + ErroMessage;
        }
    }
}
