using ElevenCourses.Models.Relations;

namespace ElevenCourses.Models;

public class Course
{
    [DisplayName("Course")]
    public Guid Id { get; set; }
    [DisplayName("Course Name")]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? CreatorId { get; set; }
    public ApplicationUser? Creator { get; set; }
    public ICollection<CourseUser> EnrolledUsers { get; } = new List<CourseUser>();
    public ICollection<Week>? Weeks { get; set; }
}