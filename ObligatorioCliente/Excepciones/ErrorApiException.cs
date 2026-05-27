namespace Excepciones
{
    public class ErrorApiException : Exception
    {
        public ErrorApiException()
        {
            
        }

        public ErrorApiException(string message) : base(message)
        {
            
        }

        public ErrorApiException(string message, Exception inner) : base(message, inner) 
        {
            
        }
    }
}
