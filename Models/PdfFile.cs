namespace ElevenCourses.Models;

public class PdfFile 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }

    public string? Path { get; set; }

    public Guid WeekId { get; set; }

    public Week? Week { get; set; }

}