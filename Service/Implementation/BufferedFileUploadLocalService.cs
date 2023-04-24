using ElevenCourses.Service.Interface;

namespace ElevenCourses.Service.Implementation
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            var path = "";
            try
            {
                if (file.Length <= 0) return path;
                
                if (!Directory.Exists("CourseDocuments"))
                {
                    Directory.CreateDirectory("CourseDocuments");
                }

                path = Path.GetFullPath(Path.Combine("CourseDocuments", folderPath));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                await using var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create);
                await file.CopyToAsync(fileStream);
                
                return path;

            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}