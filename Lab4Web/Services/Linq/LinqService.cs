using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4Web.Services.Linq
{
    // Class representing a student
    public class Student
    {
        // Constructor
        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Properties
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // Static class holding a list of students
    public static class StudentRepository
    {
        // List of students
        public static List<Student> Students { get; } = new List<Student>()
        {
            new Student("John", 25),
            new Student("Alice", 29),
            new Student("Bob", 33),
            new Student("Emily", 22),
            new Student("Michael", 28),
            new Student("Sophia", 31),
            new Student("David", 24),
            new Student("Emma", 27),
            new Student("James", 30),
            new Student("Olivia", 26)
        };
    }

    // Interface defining LINQ operations on students
    public interface ILinqService
    {
        // Returns a list of students older than a specified age
        List<Student> GetStudentsOlderThan(int age);

        // Returns a list of student names
        List<string> GetStudentNames();

        // Returns the total number of students
        int CountStudents();

        // Returns a list of students filtered by name
        List<Student> FilterStudentsByName(string name);

        // Returns a list of students that exist in both the current list and another list of students
        List<Student> JoinStudents(List<Student> otherStudents);

        // Groups students by age
        IEnumerable<IGrouping<int, Student>> GroupStudentsByAge();
    }

    // Service implementing ILinqService interface
    public class LinqService : ILinqService
    {
        // Returns a list of students older than a specified age
        public List<Student> GetStudentsOlderThan(int age)
        {
            var query = from student in StudentRepository.Students
                        where student.Age > age
                        select student;

            return query.ToList();
        }

        // Returns a list of student names
        public List<string> GetStudentNames()
        {
            return StudentRepository.Students.Select(student => student.Name).ToList();
        }

        // Returns the total number of students
        public int CountStudents()
        {
            var query = from student in StudentRepository.Students
                        select student;

            return query.Count();
        }

        // Returns a list of students filtered by name
        public List<Student> FilterStudentsByName(string name)
        {
            return StudentRepository.Students.Where(student => student.Name.Contains(name)).ToList();
        }

        // Returns a list of students that exist in both the current list and another list of students
        public List<Student> JoinStudents(List<Student> otherStudents)
        {
            var query = from student in StudentRepository.Students
                        join otherStudent in otherStudents on student.Name equals otherStudent.Name
                        select student;

            return query.ToList();
        }

        // Groups students by age
        public IEnumerable<IGrouping<int, Student>> GroupStudentsByAge()
        {
            return StudentRepository.Students.GroupBy(student => student.Age);
        }
    }
}
