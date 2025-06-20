namespace WebApiLibrary.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AuthorFullName { get; set; } = null!;
        public string AuthorEmail { get; set; } = null!;
        public string SerialCode { get; set; } = null!;
        public string PublicationCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<IndexEntry> IndexEntries { get; set; } = new();
    }
}
