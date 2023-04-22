using ElevenCourses.Service.Interface;

namespace ElevenCourses.Service.Implementation
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists("weeks"))
                    {
                        Directory.CreateDirectory("weeks");
                    }

                    path = Path.GetFullPath(Path.Combine("weeks", folderPath));

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return path;
                }
                else
                {
                    return path;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}