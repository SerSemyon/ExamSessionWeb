using System;
using System.Collections.Generic;

namespace ExamSessionWeb.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string? Name { get; set; }

    public int? HoursNum { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual ICollection<TeacherItem> TeacherItems { get; set; } = new List<TeacherItem>();
}
