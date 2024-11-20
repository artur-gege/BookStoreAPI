namespace BookStoreAPI.Application.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class BookAlreadyExistsException : Exception
    {
        public BookAlreadyExistsException(string message) : base(message) { }
    }

    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(string message) : base(message) { }
    }

    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(string message) : base(message) { }
    }

    public class OrderAlreadyExistsException : Exception
    {
        public OrderAlreadyExistsException(string message) : base(message) { }
    }

    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message) { }
    }

}