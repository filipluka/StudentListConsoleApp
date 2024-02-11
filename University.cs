using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentList
{
    public class University
    {
        public University(int universityId, string universityName, string universityLocation)
        {
           UniversityId = universityId;
           UniversityName = universityName;
           UniversityLocation = universityLocation;
        }

        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityLocation { get; set; }
    }
}
