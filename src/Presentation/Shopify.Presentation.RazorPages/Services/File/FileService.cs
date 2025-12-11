
namespace Shopify.Presentation.RazorPages.Services.File;
public class FileService : IFileService
{

    public async Task Delete(string fileName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

        if (System.IO.File.Exists(fullPath))
        {
            await Task.Run(() => System.IO.File.Delete(fullPath), cancellationToken);
        }
    }

    public async Task<string> Upload(IFormFile file, string folder, CancellationToken cancellationToken)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folder);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        return Path.Combine("Files", folder, uniqueFileName);
    }
}