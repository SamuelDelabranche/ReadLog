using System;

namespace ReadLog.Core
{
    public enum ExceptionType
    {
        Unknown,
        MangaNotFound,
        AlreadyAdded
    }

    public class CustomExceptionBase : Exception
    {
        public ExceptionType ExceptionType { get; }
        public CustomExceptionBase()
        {
            ExceptionType = ExceptionType.Unknown;
        }
       
        public CustomExceptionBase(string message)
            : base(message)
        {
            ExceptionType = ExceptionType.Unknown;
        }
        
        public CustomExceptionBase(ExceptionType exceptionType, string message)
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
}
