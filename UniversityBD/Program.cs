using UniversityDB;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;



bool check = true;

/// /////////////////
var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("StringConnect.json");
var config = builder.Build();
string connection_string = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.UseSqlServer(connection_string).Options;
/// ////////////////////////

while (check)
{
    Console.WriteLine("1. Information input(initial data)");
    Console.WriteLine("2. Information output");
    Console.WriteLine("3. Information input(1 record)");
    Console.WriteLine("4. Information change(1 record)");
    Console.WriteLine("5. Information delete(1 record)");
    Console.WriteLine("6. Console clear");
    Console.WriteLine("7. Exit");    

    if (!Int32.TryParse(Console.ReadLine(), out int number))
    {
        Console.WriteLine("Wrong number!");
        continue;
    }
    switch (number)
    {
        case 1:
            Information_Inition_Data();
            break;
        case 2:
            Information_Output();
            break;
        case 3:
            Information_Input();
            break;
        case 4:
            Change();
            break;
        case 5:
            Delete();
            break;
        case 6:
            Console.Clear();
            break;
        case 7:
            check = false;
            break;
        default:
            Console.WriteLine("Wrong number");
            break;
    }



}

void Information_Inition_Data ()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        Specialization artificial_intelligence = new Specialization() { Name = "artificial intelligence" };
        Specialization mobile_system = new Specialization() { Name = "mobile system" };

        List<Specialization> list_of_specializations = new List<Specialization>();
        list_of_specializations.Add(artificial_intelligence);
        list_of_specializations.Add(mobile_system);

        Course course1 = new Course() { Name = 1  };
        Course course2 = new Course() { Name = 2};
        Course course3 = new Course() { Name = 3 };
        Course course4 = new Course() { Name = 4 };

        List<Course> list_of_courses = new List<Course> ();
        list_of_courses.Add(course1);
        list_of_courses.Add(course2);
        list_of_courses.Add(course3);
        list_of_courses.Add(course4);
        
        

        University brstu = new University() { Name = "Brest State Technical University", specializations = list_of_specializations, courses = list_of_courses };

        Connection_SpecializationsWithCourse conSpecWithCour1 = new Connection_SpecializationsWithCourse() { course = course1, specialization = mobile_system, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour2 = new Connection_SpecializationsWithCourse() { course = course1, specialization = artificial_intelligence , University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour3 = new Connection_SpecializationsWithCourse() { course = course2, specialization = mobile_system, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour4 = new Connection_SpecializationsWithCourse() { course = course2, specialization = artificial_intelligence, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour5 = new Connection_SpecializationsWithCourse() { course = course3, specialization = mobile_system, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour6 = new Connection_SpecializationsWithCourse() { course = course3, specialization = artificial_intelligence, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour7 = new Connection_SpecializationsWithCourse() { course = course4, specialization = mobile_system, University = brstu };
        Connection_SpecializationsWithCourse conSpecWithCour8 = new Connection_SpecializationsWithCourse() { course = course4, specialization = artificial_intelligence, University = brstu };

        Student Max_t = new Student() { Name = "Maksim T.", Course = course4, Specialization = artificial_intelligence, University = brstu };
        Student Pavel_A = new Student() { Name = "Pavel A.", Course = course4, Specialization = artificial_intelligence, University = brstu };
        Student Dima_S = new Student() { Name = "Dima S.", Course = course4, Specialization = artificial_intelligence, University = brstu };
        Student Dima_K = new Student() { Name = "Dima K.", Course = course4, Specialization = artificial_intelligence, University = brstu };
        Student Leha_G = new Student() { Name = "Leha G.", Course = course3, Specialization = mobile_system, University = brstu };
        Student Andrey_C = new Student() { Name = "Andrey C.", Course = course4, Specialization = mobile_system, University = brstu };

        db.Universities.Add(brstu);
        db.Specializations.AddRange(artificial_intelligence, mobile_system);
        db.Courses.AddRange(course1, course2, course3, course4);
        db.Students.AddRange(Max_t, Pavel_A, Dima_K, Dima_S, Leha_G, Andrey_C);
        db.Connection_SpecializationsWithCourses.AddRange(conSpecWithCour1, conSpecWithCour2, conSpecWithCour3, conSpecWithCour4, conSpecWithCour5, conSpecWithCour6, conSpecWithCour7, conSpecWithCour8);
        db.SaveChanges();
        Console.WriteLine("The changes were successfully saved.");
    }
}

void Information_Output()
{
    Console.WriteLine("");
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var list_of_univers = db.Universities
            .Include(sp => sp.specializations)
            .Include(st => st.students)
            .Include(c => c.courses)
            .Include(c => c.connection_SpecializationWithCourses)
            .ToList();
        foreach(var univer in list_of_univers)
        {
            Console.WriteLine($"University - {univer.Name}");
            foreach (var conCourWithSpec in univer.connection_SpecializationWithCourses.OrderBy(c => c?.courseId))
            //foreach (var conCourWithSpec in univer.connection_SpecializationWithCourses)
            {
                foreach ( var st in conCourWithSpec.specialization.students.Where( c => c.CourseId == conCourWithSpec.courseId))
                {
                    Console.WriteLine($"    Course - {conCourWithSpec.course.Name}");
                    Console.WriteLine($"        Specialization - {conCourWithSpec.specialization.Name}");
                    Console.WriteLine($"            {st.Name}");
                }
            }
        }
    }    
}

void Information_Input()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var univers = db.Universities.ToList();
        Console.WriteLine("Id Name");
        bool new_univer = false;
        foreach (var u in univers)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Input the id or name of the university:");
        string? name = "";
        int name_is_number;
        University? univer = null;
        while (name == "" || univer == null)
        {
            name = Console.ReadLine();
            //check whether this univ exist or not.  Create it if it doesn't. 
            if (Int32.TryParse(name, out name_is_number))
            {
                univer = univers.Where(u => u.Id == name_is_number).FirstOrDefault();
                CheckCorrectInput(univer, ref name);                
            }
            else if (name != "")
            {
                univer = new University();
                univer.Name = name;
                new_univer = true;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Id Name");
        var courses = db.Courses.ToList();
        foreach (var c in courses)
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }
        Console.WriteLine("Input the id of course");

        name = "";
        name_is_number = 0;
        Course? course = null;
        bool new_course = false;
        while (name == "" || course == null)
        {
            name = Console.ReadLine();
            //check whether this course exist or not.  Create it if it doesn't. 
            if (Int32.TryParse(name, out name_is_number))
            {
                course = courses.Where(u => u.Id == name_is_number).FirstOrDefault();
                CheckCorrectInput(course, ref name);                
            }
            else if (name != "")
            {
                course = new Course();
                course.Name = name_is_number;
                course.University = univer;
                new_course = true;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Id Name");
        Specialization? specialization = null;
        var specializations = db.Specializations.ToList();
        foreach (var c in specializations)
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }
        Console.WriteLine("Input the id of specialization");

        name = "";
        name_is_number = 0;
        bool new_specialization = false;
        while (name == "" || specialization == null)
        {
            name = Console.ReadLine();
            //check whether this specialization exist or not.  Create it if it doesn't. 
            if (Int32.TryParse(name, out name_is_number))
            {
                specialization = (Specialization?)specializations.Where(u => u.Id == name_is_number).FirstOrDefault();
                CheckCorrectInput(specialization, ref name);
            }
            else if (name != "")
            {
                specialization = new Specialization();
                specialization.Name = name;
                specialization.University = univer;
                new_specialization = true;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Id course specialization");
        var connection_SpecializationsWithCourses = db.Connection_SpecializationsWithCourses
            .Where(c => c.course == course && c.specialization == specialization).ToList();
        foreach (var c in connection_SpecializationsWithCourses)
        {
            Console.WriteLine($"{c.Id}. {c.course.Name} - {c.specialization.Name}");
        }
        if (connection_SpecializationsWithCourses.Count == 0)
        {
            Console.WriteLine($"0. {course} - {specialization}");
        }
        Console.WriteLine("Input the id of connect");

        name = "";
        name_is_number = 0;
        Connection_SpecializationsWithCourse? connect_cor_spec = null;
        bool new_connect_cor_spec = false;
        while (name == "" || connection_SpecializationsWithCourses == null)
        {
            name = Console.ReadLine();
            //check whether this connect exist or not.  Create it if it doesn't. 
            if (Int32.TryParse(name, out name_is_number) && name_is_number != 0)
            {
                connect_cor_spec = connection_SpecializationsWithCourses.Where(u => u.Id == name_is_number).FirstOrDefault();
                CheckCorrectInput(connect_cor_spec, ref name);                
            }
            else if (name != "")
            {
                connect_cor_spec = new Connection_SpecializationsWithCourse();
                connect_cor_spec.course = course;
                connect_cor_spec.specialization = specialization;
                connect_cor_spec.University = univer;
                new_connect_cor_spec = true;
            }
        }


        Console.WriteLine();
        Student? student = null;
        name = "";
        Console.WriteLine("Enter the student's name.");
        while (name == "" && student == null)
        {
            name = Console.ReadLine();
            var students = db.Students.Where(c => c.Name == name).ToList();
            if (students.Count > 0)
            {
                Console.WriteLine("This student already exists.");
            }
            else
            {
                student = new Student();
                student.Name = name;
                student.University = univer;
                student.Specialization = specialization;
                student.Course = course;
            }
        }

        if (new_univer) db.Universities.Add(univer);
        if (new_course) db.Courses.Add(course);
        if (new_specialization) db.Specializations.Add(specialization);
        if (new_connect_cor_spec) db.Connection_SpecializationsWithCourses.Add(connect_cor_spec);
        db.Students.Add(student);
        db.SaveChanges();
    }
}

void Change()
{
    bool change_check = true;
    while (change_check)
    {
        Console.WriteLine("Change:");
        Console.WriteLine("1. University");
        Console.WriteLine("2. Course");
        Console.WriteLine("3. Specialization");
        Console.WriteLine("4. Student");
        Console.WriteLine("5. Exit");

        int change_nubmer = 0;
        if (!Int32.TryParse(Console.ReadLine(), out change_nubmer))
        {
            Console.WriteLine("Wrong number");
        }

        switch (change_nubmer)
        {
            case 1:
                ChangeUniversity();
                break;
            case 2:
                ChangeCourse();
                break;
            case 3:
                ChangeSpecialization();
                break;
            case 4:
                ChangeStudent();
                break;
            case 5:
                change_check = false;
                break;
            default:
                Console.WriteLine("Wrong number");
                break;
        }    

    }
}

void Delete()
{
    bool delete_check = true;
    while (delete_check)
    {
        Console.WriteLine("Delete:");
        Console.WriteLine("1. University");
        Console.WriteLine("2. Course");
        Console.WriteLine("3. Specialization");
        Console.WriteLine("4. Student");
        Console.WriteLine("5. Exit");

        int change_nubmer = 0;
        if (!Int32.TryParse(Console.ReadLine(), out change_nubmer))
        {
            Console.WriteLine("Wrong number");
        }

        switch (change_nubmer)
        {
            case 1:
                DeleteUniv();
                break;
            case 2:
                DeleteCourse();
                break;
            case 3:
                DeleteSpecialization();
                break;
            case 4:
                DeleteStudent();
                break;
            case 5:
                delete_check = false;
                break;
            default:
                Console.WriteLine("Wrong number");
                break;
        }

    }
}
void ChangeUniversity()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var univers = db.Universities.ToList();
        foreach(var u in univers)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        while(check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var univ = univers.Where(u => u.Id == change).FirstOrDefault();
            if (univ == null)
            {
                Console.WriteLine("Wrong number");
                continue;            
            }
            Console.WriteLine("Enter the new name of the university.");
            univ.Name = Console.ReadLine();
            check = false;
        }
        db.SaveChanges();
    }
}

void ChangeCourse()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var courses = db.Courses.ToList();
        foreach (var u in courses)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var course = courses.Where(u => u.Id == change).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            Console.WriteLine("Enter the new name of the course.");
            while (!Int32.TryParse(Console.ReadLine(), out name))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            course.Name = name;
            check = false;
        }
        db.SaveChanges();
    }
}

void ChangeSpecialization ()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var specializations = db.Specializations.ToList();
        foreach (var u in specializations)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var specialization = specializations.Where(u => u.Id == change).FirstOrDefault();
            if (specialization == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            Console.WriteLine("Enter the new name of the specialization.");
            specialization.Name = Console.ReadLine();
            check = false;
        }
        db.SaveChanges();
    }
}

void ChangeStudent ()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var students = db.Students.ToList();
        foreach (var u in students)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var student = students.Where(u => u.Id == change).FirstOrDefault();
            if (student == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            Console.WriteLine("Enter the new name of the student.");
            student.Name = Console.ReadLine();
            check = false;
        }
        db.SaveChanges();
    }
}

void CheckCorrectInput(object? obj, ref string name)
{
    if (obj == null)
    {
        name = "";
        Console.WriteLine("Wrong id.");
    }
}

void DeleteUniv()
{
    using (ApplicationContext db = new ApplicationContext(options))
    { 
        var univs = db.Universities.ToList();
        foreach (var u in univs)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var univ = univs.Where(u => u.Id == change).FirstOrDefault();
            if (univ == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            db.Universities.Remove(univ);
            check = false;
        }
        db.SaveChanges();
    }
}

void DeleteCourse()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var courses = db.Courses.ToList();
        foreach (var u in courses)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var cour = courses.Where(u => u.Id == change).FirstOrDefault();
            if (cour == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var conn_spec_cour = db.Connection_SpecializationsWithCourses.Where(u => u.courseId == change);
            foreach (var c in conn_spec_cour)
            {
                db.Connection_SpecializationsWithCourses.Remove(c);
            }
            var students = db.Students.Where(u => u.CourseId == change);
            foreach (var c in students)
            {
                db.Students.Remove(c);
            }
            db.Courses.Remove(cour);
            check = false;
        }
        db.SaveChanges();
    }
}
void DeleteSpecialization()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var specializations = db.Specializations.ToList();
        foreach (var u in specializations)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var specialization = specializations.Where(u => u.Id == change).FirstOrDefault();
            if (specialization == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var conn_spec_cour = db.Connection_SpecializationsWithCourses.Where(u => u.specializationId == change);
            foreach(var c in conn_spec_cour)
            {
                db.Connection_SpecializationsWithCourses.Remove(c);
            }
            var students = db.Students.Where(u => u.SpecializationId == change);
            foreach (var c in students)
            {
                db.Students.Remove(c);
            }
            //db.SaveChanges();
            db.Specializations.Remove(specialization);
            check = false;
        }
        db.SaveChanges();
    }
}
void DeleteStudent()
{
    using (ApplicationContext db = new ApplicationContext(options))
    {
        var students = db.Students.ToList();
        foreach (var u in students)
        {
            Console.WriteLine($"{u.Id}. {u.Name}");
        }
        Console.WriteLine("Choose id.");
        int change = 0;
        bool check = true;
        int name = 0;
        while (check)
        {
            if (!Int32.TryParse(Console.ReadLine(), out change))
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            var student = students.Where(u => u.Id == change).FirstOrDefault();
            if (student == null)
            {
                Console.WriteLine("Wrong number");
                continue;
            }
            db.Students.Remove(student);
            check = false;
        }
        db.SaveChanges();
    }
}
