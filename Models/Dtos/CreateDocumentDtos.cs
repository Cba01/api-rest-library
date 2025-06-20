using System.ComponentModel.DataAnnotations;

namespace WebApiLibrary.Models.Dtos
{
    public class CreateDocumentDtos
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string AuthorFullName { get; set; } = null!;

        [Required, EmailAddress]
        public string AuthorEmail { get; set; } = null!;

        [Required, RegularExpression("^[0-9A-Fa-f]+$", ErrorMessage = "SerialCode debe ser hexadecimal")]
        public string SerialCode { get; set; } = null!;

        [Required, RegularExpression(
            "^(ISO-\\d+|Ley N° \\d{1,3}(\\.\\d{3})*|P-\\d{2}\\.\\d{8})$",
            ErrorMessage = "PublicationCode con formato inválido")]
        public string PublicationCode { get; set; } = null!;

        [MinLength(1, ErrorMessage = "Debe incluir al menos un elemento en el índice")]
        public List<CreateIndexEntryDto> IndexEntries { get; set; } = new();


        public class CreateIndexEntryDto
        {
            [Required, MaxLength(200)]
            public string Name { get; set; } = null!;

            public int? Page { get; set; }
        }
    }
}
