namespace DataLibrary.Logic
{
    public interface IIdentifiable<T>
    {
        T Id { get; set; }
    }
}
