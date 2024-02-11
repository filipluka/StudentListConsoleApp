using System;

public class Grade
{
    public Grade(int studentId, int gradeNo, string section, string subject)
    {
        StudentId = studentId;
        GradeNo = gradeNo;
        Section = section;
        SubjectName = subject;
    }

    public int StudentId { get; set; }
    public int GradeNo { get; set; }
    public string Section { get; set; }
    public string SubjectName { get; set; }

}
