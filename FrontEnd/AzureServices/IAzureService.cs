using FrontEnd.AzureServices.Models;

namespace FrontEnd.AzureServices;

public interface IAzureStorage
{
    Task<BlobResponseDto> UploadAsync(Stream file, string fileName);

    Task<BlobDto?> DownloadAsync(string blobFilename);

    Task<BlobResponseDto> DeleteAsync(string blobFilename);

    Task<List<BlobDto>> ListAsync();
}