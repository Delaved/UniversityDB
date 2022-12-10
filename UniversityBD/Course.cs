using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDB
{
    public class Course
    {
        public int Id { get; set; }
        public int Name { get; set; }
        // public List<Specialization> specializations { get; set; } = new List<Specialization>();
        public List<Connection_SpecializationsWithCourse> connection_SpecializationsWithCourses { get; set; } = new List<Connection_SpecializationsWithCourse>();
        public int UniversityId { get; set; }
        public University University { get; set; }
        public List<Student> students { get; set; } = new List<Student> ();

    }
}
