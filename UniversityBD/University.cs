using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDB
{
    public class University
    {
        public int Id {get;set;}
        public string? Name {get;set;}
        public List<Connection_SpecializationsWithCourse> connection_SpecializationWithCourses {get;set;} = new List<Connection_SpecializationsWithCourse>();     
        public List<Specialization> specializations {get;set;} =new List<Specialization>();
        public List<Student> students { get; set; } = new List<Student>();  
        public List<Course> courses {get;set;} = new List<Course>();
    }

    //public static class List_of_University
    //{
    //    private static List<University> list_of_univer = new List<University>();
    //    public static void Add(University u)
    //    { 
    //        list_of_univer.Add(u);
    //    }
    //    public static bool Check_Exsist(University u)
    //    {
    //        if(list_of_univer.Contains(u))
    //            return true;
    //        else return false;
    //    }
    //}
}
