using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementWithJWT.Models;
using StudentManagementWithJWT.Services;

namespace StudentManagementWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add-student")]
        [Authorize]
        public IActionResult AddStudent(StudentsVM studentsVM)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    _studentService.AddStudent(studentsVM);
                    return Ok();

                }

                return BadRequest();

            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            
            }
            

        }

        [HttpPost("update-student")]
        [Authorize]

        public IActionResult UpdateStudent(int? id,StudentsVM student)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _studentService.UpdateStudent(id,student);
                    return Ok();

                }

                return BadRequest();

            }

            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpGet("get-all-students")]
        [Authorize]

        public IActionResult GetAllStudents()
        {
            var allStudents= _studentService.GetAllStudents();
            return Ok(allStudents);
        }

        [HttpGet("get-student-by-id")]
        [Authorize]

        public IActionResult GetStudentById(int? id)
        {

            try 
            {
                if (id == 0 || id == null)
                {
                    return NotFound();
                }
                var student = _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);

            }

        }

        [HttpDelete("delete-student-by-id")]
        [Authorize]
        public IActionResult DeleteStudent(int? id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    return NotFound();
                }
                _studentService.DeleteStudent(id);
                return Ok();
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);

            }

        }


    }
}
