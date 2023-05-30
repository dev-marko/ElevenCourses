namespace ElevenCourses.Models.Relations;

public class QuestionInTest
{
    public Guid questionId { set; get; }
    public Question question { set; get; }
    public Guid testId { set; get; }
    public Test test { set; get; }
}