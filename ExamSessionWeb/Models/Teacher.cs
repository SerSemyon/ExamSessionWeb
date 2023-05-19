using System;
using System.Collections.Generic;

namespace ExamSessionWeb.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();

    public virtual ICollection<TeacherItem> TeacherItems { get; set; } = new List<TeacherItem>();
}
