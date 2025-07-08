using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CodeNestContext _context;

    public CoursesController(CodeNestContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Course course)
    {
        if (id != course.Id) return BadRequest();
        _context.Entry(course).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Courses.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return NotFound();
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("count")]
    public IActionResult GetCount()
    {
        return Ok(_context.Courses.Count());
    }
}