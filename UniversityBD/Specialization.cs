using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDB
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }  
      //  public List<Course> courses { get; set; } = new List<Course>();
        public List<Student> students { get; set; } = new List<Student>();
        public List<Connection_SpecializationsWithCourse> connection_SpecializationsWithCourses { get; set; } = new List<Connection_SpecializationsWithCourse>();
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
