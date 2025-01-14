﻿using System;
using API.Dtos;
using API.Dtos.RequestDto;
using API.Dtos.ResponseDto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.Extensions.ObjectPool;
using PersonalInformationDto = API.Dtos.ResponseDto.PersonalInformationDto;


namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, Student>()
                .ForMember(dst => dst.SchoolNumber, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, StudentAffairs>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));
           
            CreateMap<User, Teacher>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Classroom, ClassRoomDto>()
                .ForMember(dst => dst.ClassRoomCode, opt => opt.MapFrom(src => src.ClassRoomCode))
                .ForMember(dst => dst.ClassRoomName, opt => opt.MapFrom(src => src.ClassRoomName));

            CreateMap<API.Dtos.RequestDto.LessonDto, Lesson>();
            CreateMap<StudyProgramDto, StudyProgram>();
            CreateMap<SemesterDto, Semester>();

            CreateMap<StudentPersonalityInformation, PersonalInformationDto>()
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dst => dst.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.ToString()))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Student.LastName));

            CreateMap<StudentInformation, EducationInformationDto>()
                .ForMember(dst => dst.AdvisorTeacher,
                    opt => opt.MapFrom(src => src.AdvisorTeacher.FirstName + " " + src.AdvisorTeacher.LastName))
                .ForMember(dst => dst.StudyTime, opt => opt.MapFrom(src => src.StudyTime.Name))
                .ForMember(dst => dst.CurrentClass, opt => opt.MapFrom(src => src.Semester.Year.ToString()))
                .ForMember(dst => dst.EducationType, opt => opt.MapFrom(src => src.EducationType.ToString()))
                .ForMember(dst => dst.Semester, opt => opt.MapFrom(src => src.Semester.SemesterName))
                .ForMember(dst => dst.Faculty, opt => opt.MapFrom(src => src.Faculty.FacultyName))
                .ForMember(dst => dst.StudyProgram, opt => opt.MapFrom(src => src.StudyProgram.ProgramName))
                .ForMember(dst => dst.SchoolNumber, opt => opt.MapFrom(src => src.Student.SchoolNumber));

            CreateMap<Grade, GradeCardDto>()
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(x => x.Lesson.Teacher.FirstName + " " + x.Lesson.Teacher.LastName))
                .ForMember(dst => dst.Akts, opt => opt.MapFrom(src => src.Lesson.Akts))
                .ForMember(dst => dst.GradesAverage, opt => opt.MapFrom(src => src.Average))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.Lesson.LessonCode))
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.Lesson.LessonName))
                .ForMember(dst => dst.PracticeTime, opt => opt.MapFrom(src => src.Lesson.PracticeTime))
                .ForMember(dst => dst.TheoryTime, opt => opt.MapFrom(src => src.Lesson.TheoryTime))
                .ForMember(dst => dst.Letter, opt => opt.MapFrom<LetterResolver<GradeCardDto>>())
                .ForMember(dst => dst.LetterGrade, opt => opt.MapFrom<LetterGradeResolver<GradeCardDto>>())
                .ForMember(dst => dst.Semester, opt => opt.MapFrom(src => src.Lesson.Semester.Id))
                .ForMember(dst => dst.LessonYear, opt => opt.MapFrom(src => src.Lesson.Semester.Year));
            CreateMap<Lesson, CurriculumGradeCardDto>()
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.LessonName))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.LessonCode))
                .ForMember(dst => dst.LessonType, opt => opt.MapFrom(src => src.LessonType))
                .ForMember(dst => dst.Akts, opt => opt.MapFrom(src => src.Akts))
                .ForMember(dst => dst.SuccessStatus, opt => opt.MapFrom(src => LessonStatus.Unregistered))
                .ForMember(dst => dst.Semester, opt => opt.MapFrom(src => src.Semester.Id));

            CreateMap<Grade, CurriculumGradeCardDto>()
                .ForMember(dst => dst.GradesAverage, opt => opt.MapFrom(src => src.Average))
                .ForMember(dst => dst.Letter, opt => opt.MapFrom<LetterResolver<CurriculumGradeCardDto>>())
                .ForMember(dst => dst.LetterGrade, opt => opt.MapFrom<LetterGradeResolver<CurriculumGradeCardDto>>())
                .ForMember(dst => dst.NumberOfLessonTaken, opt => opt.MapFrom(src => src.NumberOfLessonTaken))
                .ForMember(dst => dst.LessonStatus, opt => opt.MapFrom<LessonStatusResolver<CurriculumGradeCardDto>>())
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.Lesson.LessonName))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.Lesson.LessonCode))
                .ForMember(dst => dst.LessonType, opt => opt.MapFrom(src => src.Lesson.LessonType))
                .ForMember(dst => dst.Akts, opt => opt.MapFrom(src => src.Lesson.Akts))
                .ForMember(dst => dst.SuccessStatus, opt => opt.MapFrom<LessonStatusResolver<CurriculumGradeCardDto>>())
                .ForMember(dst => dst.StatusAbsenteeism, opt => opt.MapFrom(src => src.FailedAbsenteeism))
                .ForMember(dst => dst.Semester, opt => opt.MapFrom(src => src.Lesson.Semester.Id));

            CreateMap<Lesson, LessonDateDto>()
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.LessonCode))
                .ForMember(dst => dst.LessonCount, opt => opt.MapFrom(src => src.LessonofNumber))
                .ForMember(dst => dst.LessonDay, opt => opt.MapFrom<LessonDayResolver>())
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.LessonName))
                .ForMember(dst => dst.LessonStartHour, opt => opt.MapFrom(src => src.LessonStartHour))
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName));

            CreateMap<Lesson, MidExamDto>()
                .ForMember(dst => dst.ClassroomName, opt => opt.MapFrom(src => src.ExamClassRoom.ClassRoomName))
                .ForMember(dst => dst.MidExamDate, opt => opt.MapFrom(src => src.MidTermTime))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.LessonCode))
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.LessonName))
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName));

            CreateMap<Lesson, FinalExamDto>()
                .ForMember(dst => dst.ClassroomName, opt => opt.MapFrom(src => src.ExamClassRoom.ClassRoomName))
                .ForMember(dst => dst.FinalExamDate, opt => opt.MapFrom(src => src.MidTermTime))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.LessonCode))
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.LessonName))
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + src.Teacher.LastName));

            CreateMap<Grade, SemesterGradeCardDto>()
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Lesson.Teacher.FirstName + " " + src.Lesson.Teacher.LastName))
                .ForMember(dst => dst.GradeAverage, opt => opt.MapFrom(src => src.Average))
                .ForMember(dst => dst.LastExam, opt => opt.MapFrom(src => src.MakeUpExam))
                .ForMember(dst => dst.MidExam, opt => opt.MapFrom(src => src.MidTerm))
                .ForMember(dst => dst.StatusAbsenteeism, opt => opt.MapFrom(src => src.FailedAbsenteeism))
                .ForMember(dst => dst.SuccessStatus, opt => opt.MapFrom<LessonStatusResolver<GradeDto>>())
                .ForMember(dst => dst.Letter, opt => opt.MapFrom<LetterResolver<SemesterGradeCardDto>>())
                .ForMember(dst => dst.LetterGrade, opt => opt.MapFrom<LetterGradeResolver<SemesterGradeCardDto>>())
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.Lesson.LessonName))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.Lesson.LessonCode));

            CreateMap<Semester, SemesterDto>();
            CreateMap<Lesson, LessonToAddDto>()
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName));
            CreateMap<Grade, LessonToAddDto>()
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Lesson.Teacher.FirstName + " " + src.Lesson.Teacher.LastName))
                .ForMember(dst => dst.Repetition, opt => opt.MapFrom(src => src.NumberOfLessonTaken > 1 ? true : false))
                .ForMember(dst => dst.Akts, opt => opt.MapFrom(src => src.Lesson.Akts))
                .ForMember(dst => dst.LessonCode, opt => opt.MapFrom(src => src.Lesson.LessonCode))
                .ForMember(dst => dst.LessonName, opt => opt.MapFrom(src => src.Lesson.LessonName))
                .ForMember(dst => dst.TheoryTime, opt => opt.MapFrom(src => src.Lesson.TheoryTime))
                .ForMember(dst => dst.PracticeTime, opt => opt.MapFrom(src => src.Lesson.PracticeTime));

            CreateMap<Lesson, Dtos.LessonDto>()
                .ForMember(dst => dst.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName));

            CreateMap<LessonToAddDto, LessonToAddDto>()
                .ForMember(dst => dst.Repetition, opt => opt.MapFrom(src => true));

            CreateMap<Grade, Grade>() //for repitition lessons.
                .ForMember(dst => dst.SemesterId, opt => opt.MapFrom(src => src.SemesterId + 2))
                .ForMember(dst => dst.Average, opt => opt.Ignore())
                .ForMember(dst => dst.FailedAbsenteeism, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.FailedLowGrade, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.FinalExam, opt => opt.Ignore())
                .ForMember(dst => dst.GradeLetter, opt => opt.Ignore())
                .ForMember(dst => dst.MakeUpExam, opt => opt.Ignore())
                .ForMember(dst => dst.MidTerm, opt => opt.Ignore())
                .ForMember(dst => dst.NumberOfLessonTaken, opt => opt.MapFrom(src => src.NumberOfLessonTaken + 1))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .AfterMap((src, dst) => dst.Student.Id = src.Student.Id);


            CreateMap<Teacher, TeacherBasicDto>();

            CreateMap<StudyProgram, StudyProgramDto>();

            CreateMap<Faculty, FacultiesDto>();

            CreateMap<Student, StudentBasicDto>()
                .ForMember(dst => dst.StudentNumber, opt => opt.MapFrom(src => src.SchoolNumber))
                .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dst => dst.StudyPrograms,
                    opt => opt.MapFrom(src => src.Information.StudyProgram.ProgramName));

            CreateMap<Dtos.RequestDto.PersonalInformationDto, StudentPersonalityInformation>()
                .ForMember(dst => dst.MaritalStatus,
                    opt => opt.MapFrom(src => (MaritalStatus) Enum.Parse(typeof(MaritalStatus), src.MaritalStatus)))
                .ForMember(dst => dst.Gender,
                    opt => opt.MapFrom(src => (Gender) Enum.Parse(typeof(Gender), src.Gender)));

            CreateMap<Classroom, ClassRoomDto>();

            CreateMap<LessonAddDto, Lesson>();

            CreateMap<PdfFile, Dtos.ResponseDto.PdfDto>();
        }
    }
}
