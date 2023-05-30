using ElevenCourses.Models;

public class Results
{
    public Guid id { set; get; }
    public string userId { set; get; }
    public ApplicationUser user { set; get; }
    public int points { set; get; }
    public string testName { set; get; }
    public int procent { set; get; }
    public Guid testid { set; get; }
    public Test test { set; get; }
}