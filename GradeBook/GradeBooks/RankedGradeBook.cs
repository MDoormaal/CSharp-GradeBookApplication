using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name)
            : base(name)
        {
            base.Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (base.Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");

            int dropNumber = base.Students.Count / 5;

            var orderedStudents = base.Students
                    .OrderByDescending(s => s.AverageGrade)
                    .ToList();

            char grade = 'A';

            for (int i = 1; i <= base.Students.Count; i++)
            {
                if (orderedStudents[i-1].AverageGrade == averageGrade)
                    return grade;

                if (i % dropNumber == 0)
                {
                    switch (grade)
                    {
                        case 'A': grade = 'B'; break;
                        case 'B': grade = 'C'; break;
                        case 'C': grade = 'D'; break;
                        case 'D': grade = 'F'; break;
                    }
                }

                if (grade == 'F')
                    break;
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (base.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (base.Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
