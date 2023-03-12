using CloudinaryDotNet.Actions;

namespace SwimBikeRunGroopWebApp.Interfaces
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
         Task<DeletionResult> DeletePhotoAsync(string publicId); 

    }
}
