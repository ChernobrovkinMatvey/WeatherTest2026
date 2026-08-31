using System.ComponentModel.DataAnnotations;

namespace Weather.Domain;

/// <summary>
/// Сущность
/// </summary>
public class Entity
{
    /// <summary>
    /// Ключ
    /// </summary>
    [Key]
    [Required]
    public Guid Id { get; set; }
}