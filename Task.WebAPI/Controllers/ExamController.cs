using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Task.BLL.DTOs;
using Task.BLL.Logic;

namespace Task.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamController : ControllerBase
    {
        private readonly StudentExamLogic _studentExamLogic;
        private readonly ILogger _logger;
        public ExamController(StudentExamLogic studentExamLogic, ILogger<ExamController> logger)
        {
            _studentExamLogic = studentExamLogic;
            _logger = logger;
        }
        [HttpPost]
        public ActionResult SaveAnswers(ExamStudentAnswersDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _studentExamLogic.SaveAnswers(model, GetStudentID());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new
                {
                    error = ex is ArgumentException || ex is InvalidOperationException ? ex.Message : "Something went wrong"
                });
            }
        }
        [HttpGet("{examID}")]
        [ProducesResponseType(typeof(List<StudentAnswersDTO>), StatusCodes.Status200OK)]
        public ActionResult GetAnswers(int examID)
        {
            try
            {
                var data = _studentExamLogic.GetAnswers(examID, GetStudentID());
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new
                {
                    error = ex is InvalidOperationException ? ex.Message : "Something went wrong"
                });
            }
        }
        private int GetStudentID()
        {
            return int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value);
        }
    }
}
