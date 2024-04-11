namespace AppMusic.Services.Exceptions
{
    class MusicNotAvailableException : ApplicationException
    {
        public MusicNotAvailableException(string message) : base(message) { }
    }
}
