using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task.BLL.DTOs;
using Task.BLL.Models;

namespace Task.BLL.Logic
{
    public class StudentExamLogic
    {
        private readonly ApplicationDbContext _context;
        public StudentExamLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SaveAnswers(ExamStudentAnswersDTO model, int studentID)
        {
            var examWithAnswers = _context.Exam.Where(e => e.ID == model.ExamID)
                                               .Select(e => new
                                               {
                                                   e.EndDate,
                                                   Questions = e.Questions.Select(q => new
                                                   {
                                                       q.ID,
                                                       q.Answer,
                                                       q.QuestionType,
                                                       MCQAnswers = q.Answers.Select(a => new
                                                       {
                                                           a.ID,
                                                           a.IsCorrectAnswer
                                                       })
                                                   })
                                               }).FirstOrDefault();
            if (examWithAnswers == null)
                throw new ArgumentException($"Cannot find exam with id {model.ExamID}");

            if (examWithAnswers.EndDate < DateTime.Now)
                throw new InvalidOperationException("Cannot submit this answers after exam due date");

            var existingStudentExam = _context.StudentExam.Where(s => s.StudentID == studentID && s.ExamID == model.ExamID)
                                                          .Include(e => e.ExamAnswers)
                                                          .FirstOrDefault();
            if (existingStudentExam == null)
                existingStudentExam = _context.StudentExam.Add(new StudentExam
                {
                    ExamID = model.ExamID,
                    StudentID = studentID
                }).Entity;

            _context.StudentExamAnswer.RemoveRange(existingStudentExam.ExamAnswers);
            foreach (var answer in model.ExamAnswers)
            {
                var question = examWithAnswers.Questions.FirstOrDefault(q => q.ID == answer.QuestionID);
                if (question == null)
                    throw new ArgumentException($"Cannot find question with id {answer.QuestionID}");

                existingStudentExam.ExamAnswers.Add(new StudentExamAnswer
                {
                    ExamQuestionID = answer.QuestionID,
                    ISCorrect = question.QuestionType == Enums.QuestionType.TrueOrFalse ? question.Answer == answer.Answer :
                                                                                          question.MCQAnswers.FirstOrDefault(a => a.IsCorrectAnswer)?.ID == answer.SelectedMCQAnswer,
                    MultipleChoiceQuestionAnswerID = question.QuestionType != Enums.QuestionType.TrueOrFalse ? answer.SelectedMCQAnswer : null,
                    TrueOrFalseAnswer = question.QuestionType == Enums.QuestionType.TrueOrFalse && answer.Answer
                });
            }
            _context.SaveChanges();
        }
        public List<StudentAnswersDTO> GetAnswers(int examID, int studentID)
        {
            var existingStudentExamAnswers = _context.StudentExamAnswer.Where(a => a.StudentExam.StudentID == studentID && a.StudentExam.ExamID == examID)
                                                                       .Select(a => new StudentAnswersDTO
                                                                       {
                                                                           TrueOrFalseAnswer = a.TrueOrFalseAnswer,
                                                                           IsCorrect = a.ISCorrect,
                                                                           MCQAnswer = a.MultipleChoiceQuestionAnswerID.HasValue ? a.MultipleChoiceQuestionAnswer.Text : null,
                                                                           Question = a.ExamQuestion.Text
                                                                       }).ToList();
            if (existingStudentExamAnswers == null || existingStudentExamAnswers.Count == 0)
                throw new InvalidOperationException("Student didn't solve this exam");

            return existingStudentExamAnswers;

        }
    }
}
