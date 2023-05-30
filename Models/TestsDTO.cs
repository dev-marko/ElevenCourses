namespace ElevenCourses.Models;

public class TestsDTO
{
    public Test test { set; get; }
    public List<Question> questions { set; get; }

    public TestsDTO()
    {
        this.test = null;
        this.questions = new List<Question>();
    }
}