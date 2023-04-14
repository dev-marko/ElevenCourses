using Microsoft.AspNetCore.Identity;

namespace ElevenCourses.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Course> CreatedCourses { get; set; }
    public ICollection<File> CreatedFiles { get; set; }
}