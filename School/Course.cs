using System;
using System.Collections.Generic;

namespace SchoolDB.School
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string CourseDescription { get; set; } = null!;
        public decimal? Credits { get; set; }
        public int? InstructorId { get; set; }

        public virtual Instructor? Instructor { get; set; }
    }
}
