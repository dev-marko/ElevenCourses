namespace ElevenCourses.Service.Interface
{
    public interface IBufferedFileUploadService
    {
        Task<string> UploadFile(string folderPath, IFormFile file);
    }
}
