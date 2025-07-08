using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly CodeNestContext _context;

    public AssignmentsController(CodeNestContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assignments = await _context.Assignments.ToListAsync();
        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null) return NotFound();
        return Ok(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Assignment assignment)
    {
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = assignment.Id }, assignment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Assignment assignment)
    {
        if (id != assignment.Id) return BadRequest();
        _context.Entry(assignment).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assignments.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null) return NotFound();
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}