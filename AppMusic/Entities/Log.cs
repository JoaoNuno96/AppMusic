using System;

namespace AppMusic.Entities
{
    public class Log : LogBase
    {
        public string Message { get; set; }
        public override string ToString()
        {
            return Message;
        }
    }
}
