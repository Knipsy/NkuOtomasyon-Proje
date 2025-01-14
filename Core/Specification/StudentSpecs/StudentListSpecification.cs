﻿using Core.Entities;

namespace Core.Specification.StudentSpecs
{
    public class StudentListSpecification : BaseSpecification<Student>
    {
        public StudentListSpecification() 
        {
            AddInclude(src => src.Information.StudyProgram);
            AddOrderBy(src => src.Information.StudyProgram.ProgramName);   
        }

        public StudentListSpecification(string studentNumber) : base(src => src.SchoolNumber == studentNumber)
        {
            AddInclude(src => src.PersonalityInformation);
        }
    }
}
