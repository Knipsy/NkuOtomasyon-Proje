﻿

namespace Core.Entities
{
    public class Grade
    {
        public Grade()
        {
            NumberOfLessonTaken = 1;
        }
        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
        public int Id { get; set; }
        public bool FailedAbsenteeism { get; set; }
        public bool FailedLowGrade { get; set; }
        public int? MidTerm { get; set; }
        public int? FinalExam { get; set; }
        public int? MakeUpExam { get; set; }
        public int? Average { get; set; }
        public string GradeLetter { get; set; }
        public int NumberOfLessonTaken { get; set; }
        public int SemesterId { get; set; }

    }
}
