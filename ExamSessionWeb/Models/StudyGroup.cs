﻿using System;
using System.Collections.Generic;

namespace ExamSessionWeb.Models;

public partial class StudyGroup
{
    public string NumberGroup { get; set; } = null!;

    public string? Specialization { get; set; }

    public int? Elder { get; set; }

    public int? Tutor { get; set; }

    public int? NumberOfStudents { get; set; }

    public virtual Student? ElderNavigation { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? TutorNavigation { get; set; }
}
