namespace Entities.Exceptions
{
    public abstract class NotFoundExeption : Exception
    {
        protected NotFoundExeption(string message) : base(message) 
        {
            
        }

    }
    
}
