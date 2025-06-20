namespace WebApiLibrary.Models.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AuthorFullName { get; set; } = null!;
        public string AuthorEmail { get; set; } = null!;
        public string SerialCode { get; set; } = null!;
        public string PublicationCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<IndexEntryDto> IndexEntries { get; set; } = new();
    }

    public class IndexEntryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Page { get; set; }
    }

}
