using CloudinaryDotNet.Actions;

namespace WebApp.API.Services.IServices
{
    public interface IPhotoServices
    {

        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);

    }
}
