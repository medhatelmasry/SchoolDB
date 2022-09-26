using Microsoft.EntityFrameworkCore;
using SchoolDB.School;

SchoolContext context = new SchoolContext();

bool showMenu = true;
while (showMenu)
{
    showMenu = MainMenu();
}

bool MainMenu()
{
    Console.WriteLine(new string('=', 50));
    Console.WriteLine("Choose an option:");
    Console.WriteLine("0) Exit");
    Console.WriteLine("1) Display Instructors");
    Console.WriteLine("2) Display instructorts with first name starting with J");
    Console.WriteLine("3) Display instructorts sorted by first name");
    Console.WriteLine("4) Select only first & last names");
    Console.WriteLine("5) Display full names");
    Console.WriteLine("6) Display Course count By Instructor");
    Console.WriteLine("7) Add Instructor");
    Console.WriteLine("8) Update Instructor");
    Console.WriteLine("9) Delete Instructor");
    Console.Write("\r\nSelect an option: ");

    switch (Console.ReadLine())
    {
        case "0":
            return false;
        case "1":
            getInstructors();
            return true;
        case "2":
            getInstructorsWithFirstNameStartingWithJ();
            return true;
        case "3":
            getInstructorsSortedByFirstName();
            return true;
        case "4":
            getInstructorsOnlyNames();
            return true;
        case "5":
            getInstructorsOnlyNamesAliased();
            return true;
        case "6":
            getCourseCountByInstructor();
            return true;
        case "7":
            insertInstructor("Tim", "Day", "tim@day.com");
            getInstructors();
            return true;
        case "8":
            updateEmail(6, "timdat@outlook.com");
            getInstructors();
            return true;
        case "9":
            deleteInstructor(6);
            getInstructors();
            return true;
        default:
            return true;
    }
}


void getInstructors()
{
    var data = context.Instructors;

    foreach (var item in data)
    {
        Console.WriteLine($"{item.InstructorId}\t{item.FirstName}\t{item.LastName}\t{item.Email}");
    }
}

void getInstructorsWithFirstNameStartingWithJ()
{
    var data = context.Instructors
    .Where(i => i.FirstName.StartsWith("J"));

    foreach (var item in data)
    {
        Console.WriteLine($"{item.InstructorId}\t{item.FirstName}\t{item.LastName}\t{item.Email}");
    }
}

void getInstructorsSortedByFirstName()
{
    var data = context.Instructors
    .OrderBy(i => i.FirstName);

    Console.WriteLine(data.ToQueryString());

    foreach (var item in data)
    {
        Console.WriteLine($"{item.InstructorId}\t{item.FirstName}\t{item.LastName}\t{item.Email}");
    }
}

void getInstructorsOnlyNames()
{
    var data = context.Instructors
    .Select(i => new
    {
        i.FirstName,
        i.LastName
    });

    Console.WriteLine(data.ToQueryString());

    foreach (var item in data)
    {
        Console.WriteLine($"{item.FirstName}\t{item.LastName}");
    }
}

void getInstructorsOnlyNamesAliased()
{
    var data = context.Instructors
    .Select(i => new
    {
        FullName = i.FirstName + " " + i.LastName
    });

    Console.WriteLine(data.ToQueryString());

    foreach (var item in data)
    {
        Console.WriteLine($"{item.FullName}");
    }
}

void getCourseCountByInstructor()
{
    var data = context.Courses
       .Include(i => i.Instructor)
       .GroupBy(n => new
       {
           n.Instructor!.FirstName,
           n.Instructor!.LastName
       })
       .Select(g => new
       {
           Name = g.Key.FirstName + " " + g.Key.LastName,
           Count = g.Count()
       })
       .OrderByDescending(cp => cp.Count);

    foreach (var item in data)
    {
        Console.WriteLine($"{item.Name}\t{item.Count}");
    }
}

void insertInstructor(string fn, string ln, string email) {
    var instructor = new Instructor() {
        FirstName = fn,
        LastName = ln,
        Email = email
    };

    context.Instructors.Add(instructor);
    context.SaveChanges();
}

void updateEmail(int id, string email) {
    var instructor = context.Instructors.Find(id);

    if (instructor != null) {
        instructor.Email = email;
        context.SaveChanges();
    } else {
        Console.WriteLine($"Instructor with ID = {id} cannot be found");
    }
}

void deleteInstructor(int id) {
    var instructor = context.Instructors.Find(id);

    if (instructor != null) {
        context.Instructors.Remove(instructor);
        context.SaveChanges();
    } else {
        Console.WriteLine($"Instructor with ID = {id} cannot be found");
    }
}