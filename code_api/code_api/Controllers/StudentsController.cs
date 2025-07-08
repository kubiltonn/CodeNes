using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly CodeNestContext _context;

    public StudentsController(CodeNestContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Student student)
    {
        if (id != student.Id) return BadRequest();
        _context.Entry(student).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Students.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/Courses")]
    public IActionResult GetStudentCourses(int id)
    {
        var enrollments = _context.Enrollments.Where(e => e.StudentId == id).ToList();
        var courseIds = enrollments.Select(e => e.CourseId).ToList();
        var courses = _context.Courses.Where(c => courseIds.Contains(c.Id)).ToList();
        return Ok(courses);
    }

    [HttpGet("{id}/Assignments")]
    public IActionResult GetStudentAssignments(int id)
    {
        var enrollments = _context.Enrollments.Where(e => e.StudentId == id).ToList();
        var courseIds = enrollments.Select(e => e.CourseId).ToList();
        var assignments = _context.Assignments.Where(a => courseIds.Contains(a.CourseId)).ToList();
        return Ok(assignments);
    }

    [HttpGet("count")]
    public IActionResult GetCount()
    {
        return Ok(_context.Students.Count());
    }
}