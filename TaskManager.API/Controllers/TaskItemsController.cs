using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskItemsController(AppDbContext context)
    {
        _context = context;
    }

   
    /// Obtiene todas las tareas
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
    {
        var tasks = await _context.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
        return Ok(tasks);
    }

    /// Obtiene una tarea por ID
   
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        return Ok(task);
    }

    
    /// Crea una nueva tarea
    
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = false
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

   
    /// Actualiza una tarea existente
   
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        task.Title = dto.Title ?? task.Title;
        task.Description = dto.Description ?? task.Description;
        task.IsCompleted = dto.IsCompleted ?? task.IsCompleted;

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Tarea actualizada correctamente", task });
    }

  
    /// Elimina una tarea
  
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound(new { message = $"Tarea con ID {id} no encontrada" });
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Tarea eliminada correctamente" });
    }
}
