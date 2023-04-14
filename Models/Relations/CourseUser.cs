namespace ElevenCourses.Models.Relations;

public class CourseUser
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}