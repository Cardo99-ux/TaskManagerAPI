using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models;

/// <summary>
/// DTO para actualizar una tarea existente
/// </summary>
public class UpdateTaskDto
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 100 caracteres")]
    public string? Title { get; set; }

    [StringLength(500, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }
}
