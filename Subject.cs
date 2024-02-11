using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace StudentList
{
    public class Subject
    {
        public Subject(int subjectCode, string subjectName, int subjectHours)
        {
            SubjectCode = subjectCode; 
            SubjectName = subjectName; 
            SubjectHours = subjectHours;
        }

        public int SubjectCode { get; set; }
        public int SubjectHours { get; set; }
        public string SubjectName { get; set; }

    }

}
