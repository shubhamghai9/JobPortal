using System.Text;
using UglyToad.PdfPig;
using Xceed.Words.NET;

public interface IResumeTextExtractor
{
    Task<string> ExtractTextAsync(IFormFile file, CancellationToken ct = default);
}

public class ResumeTextExtractor : IResumeTextExtractor
{
    private static readonly HashSet<string> PdfExt = new(StringComparer.OrdinalIgnoreCase) { ".pdf" };
    private static readonly HashSet<string> DocxExt = new(StringComparer.OrdinalIgnoreCase) { ".docx" };
    private static readonly HashSet<string> TxtExt = new(StringComparer.OrdinalIgnoreCase) { ".txt" };

    public async Task<string> ExtractTextAsync(IFormFile file, CancellationToken ct = default)
    {
        var ext = Path.GetExtension(file.FileName);
        if (string.IsNullOrWhiteSpace(ext)) throw new InvalidOperationException("Unknown file type.");

        if (PdfExt.Contains(ext)) return await ExtractPdfAsync(file, ct);
        if (DocxExt.Contains(ext)) return await ExtractDocxAsync(file, ct);
        if (TxtExt.Contains(ext)) return await ExtractTxtAsync(file, ct);

        throw new NotSupportedException("Only PDF, DOCX, and TXT files are supported.");
    }

    private static async Task<string> ExtractTxtAsync(IFormFile file, CancellationToken ct)
    {
        using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
        return await reader.ReadToEndAsync();
    }

    private static async Task<string> ExtractPdfAsync(IFormFile file, CancellationToken ct)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, ct);
        ms.Position = 0;

        var sb = new StringBuilder();
        using var doc = PdfDocument.Open(ms);
        foreach (var page in doc.GetPages())
        {
            sb.AppendLine(page.Text);
        }
        return sb.ToString();
    }

    private static async Task<string> ExtractDocxAsync(IFormFile file, CancellationToken ct)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, ct);
        ms.Position = 0;

        using var doc = DocX.Load(ms);
        return doc.Text;
    }
}
