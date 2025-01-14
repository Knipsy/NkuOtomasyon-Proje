﻿using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
   public class NkuContext:DbContext
    {
        public NkuContext(DbContextOptions<NkuContext> options):base(options)
        {
            
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<PdfFile> PdfFiles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudyLesson> StudyLessons { get; set; }
        public DbSet<StudyProgram> StudyPrograms { get; set; }
        public DbSet<StudyTime> StudyTimes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentAffairs> StudentAffairs { get; set; }
        public DbSet<StudentInformation> StudentInformations { get; set; }
        public DbSet<StudentPersonalityInformation> StudentPersonalityInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName=="Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType
                        .GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();

                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }

            modelBuilder.Entity<StudyLesson>().HasKey(i => new
            {
                i.StudentId,
                i.LessonCode
            });

            modelBuilder.Entity<Lesson>().HasKey(i => new
            {
                i.LessonCode
            });
        

            modelBuilder.Entity<Student>()
                .HasOne(p => p.Information)
                .WithOne(p => p.Student);
               
            modelBuilder.Entity<Student>()
                .HasOne(p => p.PersonalityInformation)
                .WithOne(p => p.Student);

               

            

            modelBuilder.Entity<Semester>()
                .Property(s => s.Year)
                .HasConversion(o => o.ToString(),
                    o => (CurrentYear) Enum.Parse(typeof(CurrentYear), o));
            modelBuilder.Entity<StudentPersonalityInformation>()
                .Property(s => s.Gender)
                .HasConversion(o => o.ToString(),
                    o => (Gender) Enum.Parse(typeof(Gender), o));
            modelBuilder.Entity<StudentPersonalityInformation>()
                .Property(s => s.MaritalStatus)
                .HasConversion(o => o.ToString(),
                    o => (MaritalStatus)Enum.Parse(typeof(MaritalStatus), o));
            modelBuilder.Entity<StudentInformation>()
                .Property(s => s.EducationType)
                .HasConversion(o => o.ToString(),
                    o => (EducationType)Enum.Parse(typeof(EducationType), o));
            modelBuilder.Entity<StudentInformation>()
                .Property(s => s.RecordType)
                .HasConversion(o => o.ToString(),
                    o => (RecordType)Enum.Parse(typeof(RecordType), o));
            //modelBuilder.Entity<Student>()
            //    .Property(s => s.Type)
            //    .HasConversion(o => o.ToString(), 
            //        o => (Types) Enum.Parse(typeof(Types), o));
            //modelBuilder.Entity<Admin>()
            //    .Property(s => s.Type)
            //    .HasConversion(o => o.ToString(),
            //        o => (Types)Enum.Parse(typeof(Types), o));
            //modelBuilder.Entity<StudentAffairs>()
            //    .Property(s => s.Type)
            //    .HasConversion(o => o.ToString(),
            //        o => (Types)Enum.Parse(typeof(Types), o));
            //modelBuilder.Entity<Teacher>()
            //    .Property(s => s.Type)
            //    .HasConversion(o => o.ToString(),
            //        o => (Types)Enum.Parse(typeof(Types), o));
        }
    }
}
