using AuthService.Models;
using AuthService.Services.Captura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CapturaController(ICapturaService capturaService) : ControllerBase
{

    // GET /api/captura
    [HttpGet]
    public IActionResult GetAll() =>
        Ok(capturaService.GetAll());

    // GET /api/captura/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var record = capturaService.GetById(id);
        if (record is null) return NotFound();
        return Ok(record);
    }

    // POST /api/captura
    [HttpPost]
    public IActionResult Add([FromBody] CapturaRecord record)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = capturaService.Add(record);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/captura/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CapturaRecord record)
    {
        if (id != record.Id) return BadRequest("Id mismatch");
        var updated = capturaService.Update(record);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    // DELETE /api/captura/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = capturaService.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}