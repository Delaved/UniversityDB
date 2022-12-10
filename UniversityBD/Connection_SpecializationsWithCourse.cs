using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDB
{
    public class Connection_SpecializationsWithCourse
    {
        public int Id { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
        public int specializationId { get; set; }
        public Specialization specialization {get; set;}

        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}
