namespace WebApiLibrary.Models
{
    public class IndexEntry
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Page { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; } = null!;
    }
}
