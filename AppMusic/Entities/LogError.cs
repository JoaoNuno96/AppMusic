using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusic.Entities
{
    internal class LogError : LogBase
    {
        public string ErroMessage { get; set; }
        public string ErrorCode { get; set; }

        public override string ToString()
        {
            return $"{this.ErrorCode} - {this.ErroMessage}";
        }
    }
}
