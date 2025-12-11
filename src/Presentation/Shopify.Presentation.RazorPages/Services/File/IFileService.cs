namespace Shopify.Presentation.RazorPages.Services.File
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file, string folder, CancellationToken cancellationToken);
        Task Delete(string fileName, CancellationToken cancellationToken);
    }
}
