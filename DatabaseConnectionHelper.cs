using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentList
{
    public class DatabaseConnectionHelper
    {
        public void AddToDatabase(Student student)
        {
            int stdId = 0;

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\filip\\Projects\\StudentList\\Data\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                SqlCommand objCmd1 = new SqlCommand("INSERT INTO students(studentFirstname,studentLastname, studentEmailaddress, studentAge, universityId) VALUES(@param2,@param3, @param4, @param5, @param6);", conn);
                objCmd1.Parameters.AddWithValue("@param2", student.StudentFirstname);
                objCmd1.Parameters.AddWithValue("@param3", student.StudentLastname);
                objCmd1.Parameters.AddWithValue("@param4", student.StudentEmailAddress);
                objCmd1.Parameters.AddWithValue("@param5", student.Age);
                objCmd1.Parameters.AddWithValue("@param6", student.University.UniversityId);

                try
                {
                    conn.Open();
                    objCmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message.ToString(), "Error Message");
                }
                finally
                {
                    conn.Close();
                }

                stdId = GetIdVariable(student.StudentEmailAddress);

                SqlCommand objCmd2 = new SqlCommand("INSERT INTO addresses(studentId, street, city, country) VALUES(@param7,@param8,@param9, @param10)", conn);
                objCmd2.Parameters.AddWithValue("@param7", stdId);
                objCmd2.Parameters.AddWithValue("@param8", student.Address.Street);
                objCmd2.Parameters.AddWithValue("@param9", student.Address.City);
                objCmd2.Parameters.AddWithValue("@param10", student.Address.Country);

                SqlCommand objCmd3 = new SqlCommand("INSERT INTO grades(studentId, gradeNbr,section, subjectCode) VALUES(@param11, @param12,@param13, @param14)", conn);
                objCmd3.Parameters.AddWithValue("@param11", stdId);
                objCmd3.Parameters.AddWithValue("@param12", student.Grade.GradeNo);
                objCmd3.Parameters.AddWithValue("@param13", student.Grade.Section);
                objCmd3.Parameters.AddWithValue("@param14", student.Subject.SubjectCode);

                SqlCommand objCmd4 = new SqlCommand("INSERT INTO subjects(subjectCode, subjectName, subjectHours, studentId) VALUES(@param15,@param16,@param17, @param18)", conn);
                objCmd4.Parameters.AddWithValue("@param15", student.Subject.SubjectCode);
                objCmd4.Parameters.AddWithValue("@param16", student.Subject.SubjectName);
                objCmd4.Parameters.AddWithValue("@param17", student.Subject.SubjectHours);
                objCmd4.Parameters.AddWithValue("@param18", stdId);

                SqlCommand objCmd5 = new SqlCommand("INSERT INTO universities(universityId, universityName, universityLocation, studentId) VALUES(@param19,@param20,@param21, @param22)", conn);
                objCmd5.Parameters.AddWithValue("@param19", student.University.UniversityId);
                objCmd5.Parameters.AddWithValue("@param20", student.University.UniversityName);
                objCmd5.Parameters.AddWithValue("@param21", student.University.UniversityLocation);
                objCmd5.Parameters.AddWithValue("@param22", stdId);

                try
                {
                    conn.Open();
                    objCmd2.ExecuteNonQuery();
                    objCmd3.ExecuteNonQuery();
                    objCmd4.ExecuteNonQuery();
                    objCmd5.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message.ToString(), "Error Message");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int GetIdVariable(string email)
        {
            int idStd = 0;

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\filip\\Projects\\StudentList\\Data\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                SqlCommand idCmd = new SqlCommand("SELECT StudentId from Students where StudentEmailaddress like @studentEmail", conn);
                idCmd.Parameters.AddWithValue("@studentEmail", email);

                conn.Open();

                using (SqlDataReader oReader = idCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        idStd = oReader.GetInt32(oReader.GetOrdinal("StudentId"));
                    }
                }
                conn.Close();
            }
            
            return idStd;
        }

        internal void DeleteStudentData(string searchedEmail)
        {
            var query = "DELETE Students where Students.StudentEmailaddress like @studentEmail";

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\filip\\Projects\\StudentList\\Data\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();
                    SqlCommand delCmd = new SqlCommand(query, conn);
                    delCmd.Parameters.AddWithValue("@studentEmail", searchedEmail);
                    delCmd.ExecuteNonQuery();
                    Console.WriteLine("Student removed");
                }
                catch {
                    Console.WriteLine("Couldn't delete student");
                }
                finally {
                    conn.Close();
                }
            }
        }

        internal Student ListStudentData(string searchedEmail)
        {
            Student searchedStudent = new Student();

            var joinQuery = "SELECT S.StudentId, S.StudentFirstName, S.StudentLastName, S.StudentEmailaddress, S.UniversityId, " +
                "A.Street AS Address_Street, A.City AS Address_City, A.Country AS Address_Country, " +
                "G.GradeNbr AS Grade_nbr, G.Section AS Grade_section, G.SubjectCode AS Grade_subjectCode, " +
                "SUB.SubjectHours AS Subject_hours, SUB.SubjectName AS Subject_Name, " +
                "U.UniversityName AS University_Name, U.UniversityLocation AS University_Location " +
                "from STUDENTS S JOIN ADDRESSES A on S.StudentId = A.StudentId " +
                "INNER JOIN GRADES G on S.StudentId = G.StudentId " +
                "INNER JOIN SUBJECTS SUB on G.SubjectCode = SUB.SubjectCode " +
                "INNER JOIN UNIVERSITIES U on U.UniversityId = S.UniversityId " +
                "where StudentEmailaddress like @studentEmail";

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\filip\\Projects\\StudentList\\Data\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                SqlCommand findCmd = new SqlCommand(joinQuery, conn);
                findCmd.Parameters.AddWithValue("@studentEmail", searchedEmail);

                conn.Open();
                using (
                    SqlDataReader oReader = findCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        searchedStudent.StudentID = oReader.GetInt32(oReader.GetOrdinal("StudentId"));
                        searchedStudent.StudentFirstname = oReader["StudentFirstname"].ToString();
                        searchedStudent.StudentLastname = oReader["StudentLastname"].ToString();
                        searchedStudent.StudentEmailAddress = oReader["StudentEmailaddress"].ToString();
                        searchedStudent.Address = new Address(
                            oReader.GetInt32(oReader.GetOrdinal("StudentId")),
                            oReader["Address_Street"].ToString(),
                            oReader["Address_City"].ToString(),
                            oReader["Address_Country"].ToString()
                            );
                        searchedStudent.Grade = new Grade(
                            oReader.GetInt32(oReader.GetOrdinal("StudentId")),
                            oReader.GetInt32(oReader.GetOrdinal("Grade_nbr")),
                            oReader["Grade_section"].ToString(),
                            oReader["Grade_subjectCode"].ToString()
                            );
                        searchedStudent.Subject = new Subject(
                             oReader.GetInt32(oReader.GetOrdinal("Grade_subjectCode")),
                             oReader["Subject_Name"].ToString(),
                             oReader.GetInt32(oReader.GetOrdinal("Subject_hours"))
                            );
                        searchedStudent.University = new University(
                             oReader.GetInt32(oReader.GetOrdinal("UniversityId")),
                             oReader["University_Name"].ToString(),
                             oReader["University_Location"].ToString()
                            );
                    }
                    conn.Close();
                    return searchedStudent;
                }
            }
        }

        internal void UpdateStudentData(int id, string parameter, string table, string newValue)
        {
            var queryUpdate = "UPDATE " + table + " SET " + parameter + " = '" + newValue + "' where StudentId = "  + id;

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\filip\\Projects\\StudentList\\Data\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();
                    SqlCommand upCmd = new SqlCommand(queryUpdate, conn);
                    upCmd.ExecuteNonQuery();
                    Console.WriteLine("Student data updated");
                }
                catch
                {
                    Console.WriteLine("Couldn't update student");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
