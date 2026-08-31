using System.ComponentModel.DataAnnotations;

namespace Weather.Application.Contracts;

/// <summary>
/// Контракт создания города субъекта Российской Федерации
/// </summary>
public class CityOfSubjectCreateContract
{
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