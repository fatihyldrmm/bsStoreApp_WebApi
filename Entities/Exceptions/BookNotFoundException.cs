namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundExeption
    {
        public BookNotFoundException(int id) : base($"The book with id: {id} could not found.")
        {

        }
    }
}
