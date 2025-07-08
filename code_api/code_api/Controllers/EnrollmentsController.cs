using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly CodeNestContext _context;

    public EnrollmentsController(CodeNestContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _context.Enrollments.ToListAsync();
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();
        return Ok(enrollment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, enrollment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Enrollment enrollment)
    {
        if (id != enrollment.Id) return BadRequest();
        _context.Entry(enrollment).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Enrollments.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();
        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}