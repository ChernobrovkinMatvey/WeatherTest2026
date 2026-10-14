namespace Weather.Application.DTOs;

/// <summary>
/// Dto создания/изменения города субъекта Российской Федерации
/// </summary>
public class CityOfSubjectPushDto
{
    /// <summary>
    /// Субъект РФ
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// Административный центр
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Широта
    /// </summary>
    public double Lat { get; set; }
    
    /// <summary>
    /// Долгота
    /// </summary>
    public double Lon { get; set; }
}