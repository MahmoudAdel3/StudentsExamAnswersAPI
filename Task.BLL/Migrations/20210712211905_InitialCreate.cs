using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task.BLL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.UniqueConstraint("AK_Student_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<bool>(type: "bit", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exam_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExam", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentExam_Exam_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExam_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceQuestionAnswer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    ExamQuestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceQuestionAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceQuestionAnswer_ExamQuestion_ExamQuestionID",
                        column: x => x.ExamQuestionID,
                        principalTable: "ExamQuestion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExamAnswer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISCorrect = table.Column<bool>(type: "bit", nullable: false),
                    TrueOrFalseAnswer = table.Column<bool>(type: "bit", nullable: false),
                    ExamQuestionID = table.Column<int>(type: "int", nullable: false),
                    StudentExamID = table.Column<int>(type: "int", nullable: false),
                    MultipleChoiceQuestionAnswerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamAnswer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentExamAnswer_ExamQuestion_ExamQuestionID",
                        column: x => x.ExamQuestionID,
                        principalTable: "ExamQuestion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExamAnswer_MultipleChoiceQuestionAnswer_MultipleChoiceQuestionAnswerID",
                        column: x => x.MultipleChoiceQuestionAnswerID,
                        principalTable: "MultipleChoiceQuestionAnswer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExamAnswer_StudentExam_StudentExamID",
                        column: x => x.StudentExamID,
                        principalTable: "StudentExam",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Exam",
                columns: new[] { "ID", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 7, 13, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 1", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 9, new DateTime(2021, 7, 21, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 9", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 8, new DateTime(2021, 7, 20, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 8", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 7, new DateTime(2021, 7, 19, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 7", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 6, new DateTime(2021, 7, 18, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 6", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 10, new DateTime(2021, 7, 22, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 10", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 4, new DateTime(2021, 7, 16, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 4", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 3, new DateTime(2021, 7, 15, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 3", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 2, new DateTime(2021, 7, 14, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 2", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) },
                    { 5, new DateTime(2021, 7, 17, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694), "Exam 5", new DateTime(2021, 7, 12, 23, 19, 4, 694, DateTimeKind.Local).AddTicks(6694) }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "ID", "Code", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 9, "STD9", "student9@test.com", "Student 9", "P@ssw0rd" },
                    { 1, "STD1", "student1@test.com", "Student 1", "P@ssw0rd" },
                    { 2, "STD2", "student2@test.com", "Student 2", "P@ssw0rd" },
                    { 3, "STD3", "student3@test.com", "Student 3", "P@ssw0rd" },
                    { 4, "STD4", "student4@test.com", "Student 4", "P@ssw0rd" },
                    { 5, "STD5", "student5@test.com", "Student 5", "P@ssw0rd" },
                    { 6, "STD6", "student6@test.com", "Student 6", "P@ssw0rd" },
                    { 7, "STD7", "student7@test.com", "Student 7", "P@ssw0rd" },
                    { 8, "STD8", "student8@test.com", "Student 8", "P@ssw0rd" },
                    { 10, "STD10", "student10@test.com", "Student 10", "P@ssw0rd" }
                });

            migrationBuilder.InsertData(
                table: "ExamQuestion",
                columns: new[] { "ID", "Answer", "ExamID", "QuestionType", "Text" },
                values: new object[,]
                {
                    { 1, true, 1, 0, "Question 1" },
                    { 73, true, 8, 0, "Question 3" },
                    { 72, true, 8, 0, "Question 2" },
                    { 71, true, 8, 0, "Question 1" },
                    { 70, false, 7, 1, "Question 10" },
                    { 69, false, 7, 1, "Question 9" },
                    { 68, false, 7, 1, "Question 8" },
                    { 67, false, 7, 1, "Question 7" },
                    { 66, false, 7, 1, "Question 6" },
                    { 65, true, 7, 0, "Question 5" },
                    { 64, true, 7, 0, "Question 4" },
                    { 63, true, 7, 0, "Question 3" },
                    { 62, true, 7, 0, "Question 2" },
                    { 61, true, 7, 0, "Question 1" },
                    { 60, false, 6, 1, "Question 10" },
                    { 59, false, 6, 1, "Question 9" },
                    { 58, false, 6, 1, "Question 8" },
                    { 57, false, 6, 1, "Question 7" },
                    { 56, false, 6, 1, "Question 6" },
                    { 55, true, 6, 0, "Question 5" },
                    { 54, true, 6, 0, "Question 4" },
                    { 53, true, 6, 0, "Question 3" },
                    { 74, true, 8, 0, "Question 4" },
                    { 52, true, 6, 0, "Question 2" },
                    { 75, true, 8, 0, "Question 5" },
                    { 77, false, 8, 1, "Question 7" },
                    { 98, false, 10, 1, "Question 8" },
                    { 97, false, 10, 1, "Question 7" },
                    { 96, false, 10, 1, "Question 6" },
                    { 95, true, 10, 0, "Question 5" },
                    { 94, true, 10, 0, "Question 4" },
                    { 93, true, 10, 0, "Question 3" },
                    { 92, true, 10, 0, "Question 2" },
                    { 91, true, 10, 0, "Question 1" },
                    { 90, false, 9, 1, "Question 10" },
                    { 89, false, 9, 1, "Question 9" },
                    { 88, false, 9, 1, "Question 8" },
                    { 87, false, 9, 1, "Question 7" },
                    { 86, false, 9, 1, "Question 6" },
                    { 85, true, 9, 0, "Question 5" },
                    { 84, true, 9, 0, "Question 4" },
                    { 83, true, 9, 0, "Question 3" }
                });

            migrationBuilder.InsertData(
                table: "ExamQuestion",
                columns: new[] { "ID", "Answer", "ExamID", "QuestionType", "Text" },
                values: new object[,]
                {
                    { 82, true, 9, 0, "Question 2" },
                    { 81, true, 9, 0, "Question 1" },
                    { 80, false, 8, 1, "Question 10" },
                    { 79, false, 8, 1, "Question 9" },
                    { 78, false, 8, 1, "Question 8" },
                    { 76, false, 8, 1, "Question 6" },
                    { 51, true, 6, 0, "Question 1" },
                    { 50, false, 5, 1, "Question 10" },
                    { 49, false, 5, 1, "Question 9" },
                    { 22, true, 3, 0, "Question 2" },
                    { 21, true, 3, 0, "Question 1" },
                    { 20, false, 2, 1, "Question 10" },
                    { 19, false, 2, 1, "Question 9" },
                    { 18, false, 2, 1, "Question 8" },
                    { 17, false, 2, 1, "Question 7" },
                    { 16, false, 2, 1, "Question 6" },
                    { 15, true, 2, 0, "Question 5" },
                    { 14, true, 2, 0, "Question 4" },
                    { 13, true, 2, 0, "Question 3" },
                    { 12, true, 2, 0, "Question 2" },
                    { 11, true, 2, 0, "Question 1" },
                    { 10, false, 1, 1, "Question 10" },
                    { 9, false, 1, 1, "Question 9" },
                    { 8, false, 1, 1, "Question 8" },
                    { 7, false, 1, 1, "Question 7" },
                    { 6, false, 1, 1, "Question 6" },
                    { 5, true, 1, 0, "Question 5" },
                    { 4, true, 1, 0, "Question 4" },
                    { 3, true, 1, 0, "Question 3" },
                    { 2, true, 1, 0, "Question 2" },
                    { 23, true, 3, 0, "Question 3" },
                    { 24, true, 3, 0, "Question 4" },
                    { 25, true, 3, 0, "Question 5" },
                    { 26, false, 3, 1, "Question 6" },
                    { 48, false, 5, 1, "Question 8" },
                    { 47, false, 5, 1, "Question 7" },
                    { 46, false, 5, 1, "Question 6" },
                    { 45, true, 5, 0, "Question 5" },
                    { 44, true, 5, 0, "Question 4" },
                    { 43, true, 5, 0, "Question 3" },
                    { 42, true, 5, 0, "Question 2" },
                    { 41, true, 5, 0, "Question 1" }
                });

            migrationBuilder.InsertData(
                table: "ExamQuestion",
                columns: new[] { "ID", "Answer", "ExamID", "QuestionType", "Text" },
                values: new object[,]
                {
                    { 40, false, 4, 1, "Question 10" },
                    { 39, false, 4, 1, "Question 9" },
                    { 99, false, 10, 1, "Question 9" },
                    { 38, false, 4, 1, "Question 8" },
                    { 36, false, 4, 1, "Question 6" },
                    { 35, true, 4, 0, "Question 5" },
                    { 34, true, 4, 0, "Question 4" },
                    { 33, true, 4, 0, "Question 3" },
                    { 32, true, 4, 0, "Question 2" },
                    { 31, true, 4, 0, "Question 1" },
                    { 30, false, 3, 1, "Question 10" },
                    { 29, false, 3, 1, "Question 9" },
                    { 28, false, 3, 1, "Question 8" },
                    { 27, false, 3, 1, "Question 7" },
                    { 37, false, 4, 1, "Question 7" },
                    { 100, false, 10, 1, "Question 10" }
                });

            migrationBuilder.InsertData(
                table: "MultipleChoiceQuestionAnswer",
                columns: new[] { "ID", "ExamQuestionID", "IsCorrectAnswer", "Text" },
                values: new object[,]
                {
                    { 11, 6, false, "Answer 1" },
                    { 153, 77, false, "Answer 1" },
                    { 152, 76, true, "Answer 2" },
                    { 151, 76, false, "Answer 1" },
                    { 140, 70, true, "Answer 2" },
                    { 139, 70, false, "Answer 1" },
                    { 138, 69, true, "Answer 2" },
                    { 137, 69, false, "Answer 1" },
                    { 136, 68, true, "Answer 2" },
                    { 135, 68, false, "Answer 1" },
                    { 134, 67, true, "Answer 2" },
                    { 133, 67, false, "Answer 1" },
                    { 132, 66, true, "Answer 2" },
                    { 131, 66, false, "Answer 1" },
                    { 120, 60, true, "Answer 2" },
                    { 119, 60, false, "Answer 1" },
                    { 118, 59, true, "Answer 2" },
                    { 117, 59, false, "Answer 1" },
                    { 116, 58, true, "Answer 2" },
                    { 115, 58, false, "Answer 1" },
                    { 114, 57, true, "Answer 2" },
                    { 113, 57, false, "Answer 1" },
                    { 154, 77, true, "Answer 2" },
                    { 112, 56, true, "Answer 2" },
                    { 155, 78, false, "Answer 1" },
                    { 157, 79, false, "Answer 1" },
                    { 198, 99, true, "Answer 2" },
                    { 197, 99, false, "Answer 1" },
                    { 196, 98, true, "Answer 2" },
                    { 195, 98, false, "Answer 1" },
                    { 194, 97, true, "Answer 2" },
                    { 193, 97, false, "Answer 1" },
                    { 192, 96, true, "Answer 2" },
                    { 191, 96, false, "Answer 1" },
                    { 180, 90, true, "Answer 2" },
                    { 179, 90, false, "Answer 1" },
                    { 178, 89, true, "Answer 2" },
                    { 177, 89, false, "Answer 1" },
                    { 176, 88, true, "Answer 2" },
                    { 175, 88, false, "Answer 1" },
                    { 174, 87, true, "Answer 2" },
                    { 173, 87, false, "Answer 1" }
                });

            migrationBuilder.InsertData(
                table: "MultipleChoiceQuestionAnswer",
                columns: new[] { "ID", "ExamQuestionID", "IsCorrectAnswer", "Text" },
                values: new object[,]
                {
                    { 172, 86, true, "Answer 2" },
                    { 171, 86, false, "Answer 1" },
                    { 160, 80, true, "Answer 2" },
                    { 159, 80, false, "Answer 1" },
                    { 158, 79, true, "Answer 2" },
                    { 156, 78, true, "Answer 2" },
                    { 111, 56, false, "Answer 1" },
                    { 100, 50, true, "Answer 2" },
                    { 99, 50, false, "Answer 1" },
                    { 52, 26, true, "Answer 2" },
                    { 51, 26, false, "Answer 1" },
                    { 40, 20, true, "Answer 2" },
                    { 39, 20, false, "Answer 1" },
                    { 38, 19, true, "Answer 2" },
                    { 37, 19, false, "Answer 1" },
                    { 36, 18, true, "Answer 2" },
                    { 35, 18, false, "Answer 1" },
                    { 34, 17, true, "Answer 2" },
                    { 33, 17, false, "Answer 1" },
                    { 32, 16, true, "Answer 2" },
                    { 31, 16, false, "Answer 1" },
                    { 20, 10, true, "Answer 2" },
                    { 19, 10, false, "Answer 1" },
                    { 18, 9, true, "Answer 2" },
                    { 17, 9, false, "Answer 1" },
                    { 16, 8, true, "Answer 2" },
                    { 15, 8, false, "Answer 1" },
                    { 14, 7, true, "Answer 2" },
                    { 13, 7, false, "Answer 1" },
                    { 12, 6, true, "Answer 2" },
                    { 53, 27, false, "Answer 1" },
                    { 54, 27, true, "Answer 2" },
                    { 55, 28, false, "Answer 1" },
                    { 56, 28, true, "Answer 2" },
                    { 98, 49, true, "Answer 2" },
                    { 97, 49, false, "Answer 1" },
                    { 96, 48, true, "Answer 2" },
                    { 95, 48, false, "Answer 1" },
                    { 94, 47, true, "Answer 2" },
                    { 93, 47, false, "Answer 1" },
                    { 92, 46, true, "Answer 2" },
                    { 91, 46, false, "Answer 1" }
                });

            migrationBuilder.InsertData(
                table: "MultipleChoiceQuestionAnswer",
                columns: new[] { "ID", "ExamQuestionID", "IsCorrectAnswer", "Text" },
                values: new object[,]
                {
                    { 80, 40, true, "Answer 2" },
                    { 79, 40, false, "Answer 1" },
                    { 199, 100, false, "Answer 1" },
                    { 78, 39, true, "Answer 2" },
                    { 76, 38, true, "Answer 2" },
                    { 75, 38, false, "Answer 1" },
                    { 74, 37, true, "Answer 2" },
                    { 73, 37, false, "Answer 1" },
                    { 72, 36, true, "Answer 2" },
                    { 71, 36, false, "Answer 1" },
                    { 60, 30, true, "Answer 2" },
                    { 59, 30, false, "Answer 1" },
                    { 58, 29, true, "Answer 2" },
                    { 57, 29, false, "Answer 1" },
                    { 77, 39, false, "Answer 1" },
                    { 200, 100, true, "Answer 2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_ExamID",
                table: "ExamQuestion",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestionAnswer_ExamQuestionID",
                table: "MultipleChoiceQuestionAnswer",
                column: "ExamQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_ExamID",
                table: "StudentExam",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_StudentID",
                table: "StudentExam",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswer_ExamQuestionID",
                table: "StudentExamAnswer",
                column: "ExamQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswer_MultipleChoiceQuestionAnswerID",
                table: "StudentExamAnswer",
                column: "MultipleChoiceQuestionAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamAnswer_StudentExamID",
                table: "StudentExamAnswer",
                column: "StudentExamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExamAnswer");

            migrationBuilder.DropTable(
                name: "MultipleChoiceQuestionAnswer");

            migrationBuilder.DropTable(
                name: "StudentExam");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Exam");
        }
    }
}
