using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models;

/// <summary>
/// DTO para crear una nueva tarea
/// </summary>
public class CreateTaskDto
{
    [Required(ErrorMessage = "El título es requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 100 caracteres")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no debe exceder 500 caracteres")]
    public string? Description { get; set; }
}
