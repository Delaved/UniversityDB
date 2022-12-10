using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDB
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }

    }
}
