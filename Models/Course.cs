﻿namespace ElevenCourses.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }
}