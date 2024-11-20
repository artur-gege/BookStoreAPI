namespace BookStoreAPI.Domain.ModelsDTO
{
    public class BookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
    }
}
