using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class InstructorsController : ControllerBase
{
    private readonly CodeNestContext _context;

    public InstructorsController(CodeNestContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var instructors = await _context.Instructors.ToListAsync();
        return Ok(instructors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null) return NotFound();
        return Ok(instructor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = instructor.Id }, instructor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Instructor instructor)
    {
        if (id != instructor.Id) return BadRequest();
        _context.Entry(instructor).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Instructors.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null) return NotFound();
        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("count")]
    public IActionResult GetCount()
    {
        return Ok(_context.Instructors.Count());
    }
}