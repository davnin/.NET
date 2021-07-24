using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Controllers
{
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController (StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        /// <summary>
        /// Get collection of Cars.
        /// </summary>
        /// <returns>A colection of Cars</returns>
        /// <response code="200">Returns a collection of Cars</response>
        /// <response code="500">Internal error</response>
        [HttpGet("/Students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        // GET: Students/d2cab0c9-7e94-409e-2b9a-08d88428ae4a
        /// <summary>
        /// Get a Student.
        /// </summary>
        /// <param id="id"></param>
        /// <returns>A Car</returns>
        /// <response code="201">Returns a collection of Students</response>
        /// <response code="400">If the id is malformed</response>      
        /// <response code="404">If the Student is null</response>      
        /// <response code="500">Internal error</response>
        [HttpGet("/Students/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetById(Guid id)
        {
            Student student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);

        }


        // POST: Student
        /// <summary>
        /// Creates a Student.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Student
        ///     {
        ///        "FirstName": "firstName",
        ///        "LastName": "lastName",
        ///        "Program": "program"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created Car</returns>
        /// <response code="201">Returns the newly created Student</response>
        /// <response code="400">If the Student is malformed</response>      
        /// <response code="500">Internal error</response>
        [HttpPost("/Students")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> CreateAsync([Bind("FirstName,LastName,Program")] StudentBase studentBase)
        {
            Student student = new Student
            {
                FirstName = studentBase.FirstName,
                LastName = studentBase.LastName,
                Program = studentBase.Program
            };

            _context.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }


        // DELETE: Student/5
        /// <summary>
        /// Deletes a Student.
        /// </summary>
        /// <param id="id"></param>
        /// <response code="202">Car is deleted</response>
        /// <response code="400">If the id is malformed</response>      
        /// <response code="500">Internal error</response>
        [HttpDelete("/Students/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Accepted();
        }

        // PUT: Student/5
        /// <summary>
        /// Update a Student.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Students
        ///     {
        ///        "FirstName": "firstName",
        ///        "LastName": "lastName",
        ///        "Program": "program"
        ///     }
        ///
        /// </remarks>
        /// <param id="id"></param>
        /// <returns>An updated Student</returns>
        /// <response code="200">Returns the updated Student</response>
        /// <response code="201">Returns the newly created Student</response>
        /// <response code="400">If the Student or id is malformed</response>      
        /// <response code="500">Internal error</response>
        [HttpPut("/Students/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> Upsert(Guid id, [Bind("FirstName,LastName,Program")] StudentBase studentBase)
        {
            Student student = new Student
            {
                FirstName = studentBase.FirstName,
                LastName = studentBase.LastName,
                Program = studentBase.Program
            };

            if (!StudentExists(id))
            {
                student.Id = id;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
            }

            Student dbStudent = await _context.Students.FindAsync(id);
            dbStudent.FirstName = student.FirstName;
            dbStudent.LastName = student.LastName;
            dbStudent.Program = student.Program;

            _context.Update(dbStudent);
            await _context.SaveChangesAsync();

            return Ok(dbStudent);
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

    }
}
