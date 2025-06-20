using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Context;
using WebApiLibrary.Models;
using WebApiLibrary.Models.Dtos;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10;

        public DocumentsController(AppDbContext context)
            => _context = context;

        // GET api/documents/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocumentDto>> GetById(int id)
        {
            var doc = await _context.Documents
                .Include(d => d.IndexEntries)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doc == null)
                return NotFound();

            var result = new DocumentDto
            {
                Id = doc.Id,
                Name = doc.Name,
                AuthorFullName = doc.AuthorFullName,
                AuthorEmail = doc.AuthorEmail,
                SerialCode = doc.SerialCode,
                PublicationCode = doc.PublicationCode,
                CreatedAt = doc.CreatedAt,
                IndexEntries = doc.IndexEntries
                    .Select(ix => new IndexEntryDto
                    {
                        Id = ix.Id,
                        Name = ix.Name,
                        Page = ix.Page
                    })
                    .ToList()
            };

            return Ok(result);
        }

        // GET 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> Search(
            [FromQuery] string? serialCode,
            [FromQuery] string? publicationCode,
            [FromQuery] string? author,
            [FromQuery] string? email,
            [FromQuery] int page = 1)
        {
            var query = _context.Documents.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(serialCode))
                query = query.Where(d => d.SerialCode == serialCode);

            if (!string.IsNullOrWhiteSpace(publicationCode))
                query = query.Where(d => d.PublicationCode == publicationCode);

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(d => d.AuthorFullName.Contains(author));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(d => d.AuthorEmail.Contains(email));

            var docs = await query
                .OrderBy(d => d.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var result = docs
                .Select(d => new DocumentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    AuthorFullName = d.AuthorFullName,
                    AuthorEmail = d.AuthorEmail,
                    SerialCode = d.SerialCode,
                    PublicationCode = d.PublicationCode,
                    CreatedAt = d.CreatedAt,
                    IndexEntries = new List<IndexEntryDto>()  // no cargados en search
                })
                .ToList();

            return Ok(result);
        }

        // POST api/documents
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentDtos dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { error = errors });
            }

            var doc = new Document
            {
                Name = dto.Name,
                AuthorFullName = dto.AuthorFullName,
                AuthorEmail = dto.AuthorEmail,
                SerialCode = dto.SerialCode,
                PublicationCode = dto.PublicationCode,
                CreatedAt = DateTime.UtcNow
            };

            doc.IndexEntries = dto.IndexEntries
                .Select(ix => new IndexEntry { Name = ix.Name, Page = ix.Page })
                .ToList();

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = doc.Id }, null);
        }

        // PUT api/documents/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DocumentDto>> Update(int id, [FromBody] UpdateDocumentDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { error = errors });
            }

            var doc = await _context.Documents
                .Include(d => d.IndexEntries)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doc == null)
                return NotFound();

            // Actualizar campos
            doc.Name = dto.Name;
            doc.AuthorFullName = dto.AuthorFullName;
            doc.AuthorEmail = dto.AuthorEmail;
            doc.SerialCode = dto.SerialCode;
            doc.PublicationCode = dto.PublicationCode;
            doc.UpdatedAt = DateTime.UtcNow;

            // Reemplazar índice
            _context.IndexEntries.RemoveRange(doc.IndexEntries);
            doc.IndexEntries = dto.IndexEntries
                .Select(ix => new IndexEntry { Name = ix.Name, Page = ix.Page, DocumentId = id })
                .ToList();

            await _context.SaveChangesAsync();

            // Mapear entidad → DTO para devolver
            var result = new DocumentDto
            {
                Id = doc.Id,
                Name = doc.Name,
                AuthorFullName = doc.AuthorFullName,
                AuthorEmail = doc.AuthorEmail,
                SerialCode = doc.SerialCode,
                PublicationCode = doc.PublicationCode,
                CreatedAt = doc.CreatedAt,
                UpdatedAt = doc.UpdatedAt,
                IndexEntries = doc.IndexEntries
                    .Select(ix => new IndexEntryDto
                    {
                        Id = ix.Id,
                        Name = ix.Name,
                        Page = ix.Page
                    })
                    .ToList()
            };

            return Ok(result);
        }

        // DELETE api/documents/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doc = await _context.Documents
                .Include(d => d.IndexEntries)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doc == null)
                return NotFound();

            // Soft delete
            doc.IsDeleted = true;
            doc.DeletedAt = DateTime.UtcNow;

            // Eliminar capitulos
            _context.IndexEntries.RemoveRange(doc.IndexEntries);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
