﻿namespace ElevenCourses.Models;

public class Week
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public ICollection<File> Files { get; set; }
}