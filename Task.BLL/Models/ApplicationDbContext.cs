using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Task.BLL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamQuestion> ExamQuestion { get; set; }
        public DbSet<MultipleChoiceQuestionAnswer> MultipleChoiceQuestionAnswer { get; set; }
        public DbSet<StudentExam> StudentExam { get; set; }
        public DbSet<StudentExamAnswer> StudentExamAnswer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(s => s.ID);
                e.Property(s => s.ID).ValueGeneratedOnAdd();
                e.HasAlternateKey(s => s.Email);
            });
            modelBuilder.Entity<Exam>(e =>
            {
                e.HasKey(s => s.ID);
                e.Property(s => s.ID).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ExamQuestion>(e =>
            {
                e.HasKey(s => s.ID);
                e.Property(s => s.ID).ValueGeneratedOnAdd();
                e.HasOne(s => s.Exam).WithMany(e => e.Questions).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<MultipleChoiceQuestionAnswer>(e =>
            {
                e.HasKey(s => s.ID);
                e.Property(s => s.ID).ValueGeneratedOnAdd();
                e.HasOne(s => s.ExamQuestion).WithMany(q => q.Answers).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<StudentExam>(e =>
            {
                e.HasKey(s => s.ID);
                e.HasOne(s => s.Exam).WithMany(e => e.Students).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(s => s.Student).WithMany(e => e.Exams).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<StudentExamAnswer>(e =>
            {
                e.HasKey(s => s.ID);
                e.HasOne(s => s.ExamQuestion).WithMany(q => q.StudentExamAnswers).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(s => s.MultipleChoiceQuestionAnswer).WithMany(a => a.StudentExamAnswers).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(s => s.StudentExam).WithMany(e => e.ExamAnswers).OnDelete(DeleteBehavior.Restrict);
            });
            Seed(modelBuilder);
        }
        private void Seed(ModelBuilder modelBuilder)
        {
            var currentDateTime = DateTime.Now;
            int questionIndex = 1;
            int questionAnswerIndex = 1;
            //Seed
            for (int i = 1; i <= 10; i++)
            {
                modelBuilder.Entity<Exam>().HasData(new Models.Exam
                {
                    ID = i,
                    Name = $"Exam {i}",
                    StartDate = currentDateTime,
                    EndDate = currentDateTime.AddDays(i)
                });
                for (int j = 1; j <= 10; j++)
                {
                    modelBuilder.Entity<ExamQuestion>().HasData(new ExamQuestion
                    {
                        ID = questionIndex,
                        Text = $"Question {j}",
                        QuestionType = j <= 5 ? Enums.QuestionType.TrueOrFalse : Enums.QuestionType.MCQ,
                        Answer = j <= 5,
                        ExamID = i
                    });
                    if (j > 5)
                        modelBuilder.Entity<MultipleChoiceQuestionAnswer>().HasData(new List<MultipleChoiceQuestionAnswer>
                        {
                            new MultipleChoiceQuestionAnswer
                            {
                                ID=questionAnswerIndex,
                                Text="Answer 1",
                                ExamQuestionID=questionIndex
                            },
                            new MultipleChoiceQuestionAnswer
                            {
                                ID=questionAnswerIndex+1,
                                Text="Answer 2",
                                IsCorrectAnswer=true,
                                ExamQuestionID=questionIndex
                            },
                        });
                    questionIndex++;
                    questionAnswerIndex += 2;
                }
                modelBuilder.Entity<Student>().HasData(new Student
                {
                    ID = i,
                    Email = $"student{i}@test.com",
                    Code = $"STD{i}",
                    Name = $"Student {i}",
                    Password = $"P@ssw0rd"
                });
            }
        }
    }
}
