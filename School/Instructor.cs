using System;
using System.Collections.Generic;

namespace SchoolDB.School
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public int InstructorId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}
