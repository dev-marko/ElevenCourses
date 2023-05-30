using ElevenCourses.Models.Relations;

namespace ElevenCourses.Models;

public class Question
{
    public Guid id { set; get; }
    public string question { set; get; }
    public string correctAnswer { set; get; }
    public int points { set; get; }
    public string Answer1 { set; get; }
    public string Answer2 { set; get; }
    public string Answer3 { set; get; }
    public string Answer4 { set; get; }

    public ICollection<QuestionInTest> Tests { set; get; }
}