using System.ComponentModel.DataAnnotations;

namespace Weather.Application.Contracts;

/// <summary>
/// Город субъекта Российской Федерации
/// </summary>
public class CityOfSubjectContract
{
    /// <summary>
    /// Id записи
    /// </summary>
    [Required]
    public string Id { get; set; } = "";

    /// <summary>
    /// Субъект РФ
    /// </summary>
    [Required]
    public string Subject { get; set; } = "";

    /// <summary>
    /// Административный центр
    /// </summary>
    [Required]
    public string City { get; set; } = "";
    
    /// <summary>
    /// Широта
    /// </summary>
    [Required]
    public double Lat { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    [Required]
    public double Lon { get; set; }
}