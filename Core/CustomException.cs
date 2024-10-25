using System;

namespace ReadLog.Core
{
    public enum ExceptionType
    {
        Unknown,
        MangaNotFound,
        AlreadyAdded,
        JsonError
    }

    public class CustomExceptionBase : Exception
    {
        public ExceptionType ExceptionType { get; }
        public CustomExceptionBase(string message = "No error message")
            : base(message)
        {
            ExceptionType = ExceptionType.Unknown;
        }
        
        public CustomExceptionBase(ExceptionType exceptionType, string message = "No error message")
            : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
    
    public class MangaNotFoundException : CustomExceptionBase
    {
        public MangaNotFoundException(string message = "Manga not Found!")
            : base(ExceptionType.MangaNotFound, message)
        {
        }
    }

    public class MangaAlreadyAddedExecption : CustomExceptionBase
    {
        public MangaAlreadyAddedExecption(string message = "Manga Already Added!")
            : base(ExceptionType.AlreadyAdded, message)
        {
        }
    }

    public class CustomJsonErrorException : CustomExceptionBase 
    { 
        public CustomJsonErrorException(string message = "Json error!")
            : base(ExceptionType.JsonError, message)
        {
        }
    }
}
