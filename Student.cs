using Microsoft.Identity.Client;
using StudentList;
using System;
using System.Diagnostics;

public class Student
{
    public Student()
    {
    }

    public Student(int studentId, string firstname, string lastName, string email, int age, Address address, Grade grade, Subject subject, University university)
    {
        StudentID = studentId;
        StudentFirstname = firstname;
        StudentLastname = lastName;
        StudentEmailAddress = email;
        Age = age;
        Address = address;
        Grade = grade;
        Subject = subject;
        University = university;
    }

    public int StudentID { get; set; }
    public string StudentFirstname { get; set; }
    public string StudentLastname { get; set; }
    public string StudentEmailAddress { get; set; }
    public int Age { get; set; }

    public Address Address { get; set; }
    public Grade Grade { get; set; }
    public Subject Subject { get; set; }
    public University University { get; set; }

  
}
