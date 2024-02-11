using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentList
{
    public class StudentListConnection
    {
        private List<Student> students;
        private int studentId = 0;
        private string firstname = "";
        private string lastName = "";
        private string email = "";
        private int age = 0;
        private int gradeNo = 0;
        private string street;
        private string city;
        private string country;
        private string section;
        private string subjectName;
        private int universityId;
        private int subjectCode;
        private int subjectHours;
        private string universityName;
        private string universityLocation;

        private DatabaseConnectionHelper helper = new DatabaseConnectionHelper();
       

        public void AddStudent()
        {
            Student newStudent = GetInputNewStudent();


            helper.AddToDatabase(newStudent);
        }

        public void ListStudent()
        {
            string searchedEmail = GetEmailInputStudent();
            var searchedStudent = helper.ListStudentData(searchedEmail);
            printStudent(searchedStudent);
        }

        private void printStudent(Student student)
        {
            if (student.StudentID != null && student.StudentID != 0)
            {
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("*Student ID: " + student.StudentID + "\n*First name: " + student.StudentFirstname + "\n*Last name: " + student.StudentLastname + "\n*Email: " + student.StudentEmailAddress + "\n*Street: " + student.Address.Street + "\n*City: " + student.Address.City + "\n*Country: " + student.Address.Country + "\n*University ID: " + student.University.UniversityId + "\n*University name: " + student.University.UniversityName + "\n*University Location: " + student.University.UniversityLocation + "\n*Subject Code: " + student.Subject.SubjectCode + "\n*Subject Name: " + student.Subject.SubjectName + "\n*Subject Hours: " + student.Subject.SubjectHours + "\n*Grade Number: " + student.Grade.GradeNo + "\n*Section: " + student.Grade.Section) ;
                Console.WriteLine("*******************************************************************************");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("This student is not registred in student list");
                Console.WriteLine("************************************************************************");
                Console.WriteLine("Please press enter to return to main many");
                Console.ReadLine();
            }
        }

        private string GetEmailInputStudent()
        {
            Console.WriteLine("Please type Email address for student you want to search or delete: ");
            email = Console.ReadLine();
            return email;
        }

        private Student GetInputNewStudent()
        {
            Console.WriteLine("Please type students id: ");
            studentId = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(studentId.ToString()))
            {
                Console.WriteLine("Students id can't be empty! Input age once more");
                studentId = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Please type Firstname: ");
            firstname = Console.ReadLine();
            if (string.IsNullOrEmpty(firstname))
            {
                Console.WriteLine("Name can't be empty! Input students name once more");
                firstname = Console.ReadLine();
            }
            Console.WriteLine("Please type Lastname: ");
            lastName = Console.ReadLine();
            if (string.IsNullOrEmpty(lastName))
            {
                Console.WriteLine("Last name can't be empty! Input students lastname once more");
                lastName = Console.ReadLine();
            }
            Console.WriteLine("Please type Email address: ");
            email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email can't be empty! Input your email once more");
                email = Console.ReadLine();
            }
            Console.WriteLine("Please type students age: ");
            age = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(age.ToString()))
            {
                Console.WriteLine("Students age can't be empty! Input age once more");
                age = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Please type students university id: ");
            universityId = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(universityId.ToString()))
            {
                Console.WriteLine("Students university id can't be empty! Input id once more");
                universityId = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Please type university name: ");
            universityName = Console.ReadLine();
            if (string.IsNullOrEmpty(universityName))
            {
                Console.WriteLine("University name can't be empty! Input university name once more");
                universityName = Console.ReadLine();
            }
            Console.WriteLine("Please type university location: ");
            universityLocation = Console.ReadLine();
            if (string.IsNullOrEmpty(universityLocation))
            {
                Console.WriteLine("University location can't be empty! Input university location once more");
            }
                Console.WriteLine("Please type students street address: ");
            street = Console.ReadLine();
            if (string.IsNullOrEmpty(street))
            {
                Console.WriteLine("Address can't be empty! Input students street name once more");
                street = Console.ReadLine();
            }
            Console.WriteLine("Please type City: ");
            city = Console.ReadLine();
            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("City can't be empty! Input students city name once more");
                city = Console.ReadLine();
            }
            Console.WriteLine("Please type Country: ");
            country = Console.ReadLine();
            if (string.IsNullOrEmpty(country))
            {
                Console.WriteLine("Country can't be empty! Input your city name once more");
                country = Console.ReadLine();
            }
            Console.WriteLine("Please type students grade number: ");
            gradeNo = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(gradeNo.ToString()) || gradeNo == 0)
            {
                Console.WriteLine("Students grade can't be empty or 0! Input age once more");
                gradeNo = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Please type subject section: ");
            section = Console.ReadLine();
            if (string.IsNullOrEmpty(section))
            {
                Console.WriteLine("Section can't be empty! Input your section name once more");
                section = Console.ReadLine();
            }
            Console.WriteLine("Please type subject name: ");
            subjectName = Console.ReadLine();
            if (string.IsNullOrEmpty(subjectName))
            {
                Console.WriteLine("Subject name can't be empty! Input your subject name once more");
                subjectName = Console.ReadLine();
            }
            Console.WriteLine("Please type subjects id number: ");
            subjectCode = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(subjectCode.ToString()))
            {
                Console.WriteLine("Subjects id number can't be empty or 0! Input id number once more");
                subjectCode = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Please type subjects total hours: ");
            subjectHours = Convert.ToInt32(Console.ReadLine());
            if (string.IsNullOrEmpty(subjectHours.ToString()))
            {
                Console.WriteLine("Subjects total hours can't be empty or 0! Input total hours once more");
                subjectHours = Convert.ToInt32(Console.ReadLine());
            }

            return new Student(studentId, firstname, lastName, email, age, new Address(studentId, street, city, country), new Grade(studentId, gradeNo, section, subjectName), new Subject(subjectCode, subjectName, subjectHours), new University(universityId, universityName, universityLocation));
        }

        public void RemoveStudent()
        {
            string searchedEmail = GetEmailInputStudent();
            helper.DeleteStudentData(searchedEmail);
        }
    }
}

