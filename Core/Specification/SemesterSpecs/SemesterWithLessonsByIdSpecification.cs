﻿using Core.Entities;

namespace Core.Specification.SemesterSpecs
{
    public class SemesterWithLessonsByIdSpecification : BaseSpecification<Semester>
    {
        public SemesterWithLessonsByIdSpecification()
        {
            AddInclude(x => x.Lessons);
        }

        public SemesterWithLessonsByIdSpecification(int semesterId) : base(src => src.Id == semesterId)
        {
            
        }
    }
}
