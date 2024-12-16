using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaYP02._01
{
    public class Student
    {
        public int Id { get; set; }
        public List<double> Grades { get; set; }

        public Student(int id)
        {
            Id = id;
            Grades = new List<double>();
        }

        public double GetAverageGrade()
        {
            return Grades.Average();
        }
    }
}
