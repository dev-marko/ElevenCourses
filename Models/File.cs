namespace ElevenCourses.Models;

public class File
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public string FileUrl { get; set; }
    public string CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }
    public Guid WeekId { get; set; }
    public Week Week { get; set; }
}