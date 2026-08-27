namespace Weather.Domain.Entities;

/// <summary>
/// Город субъекта Российской Федерации
/// </summary>
public class CityOfSubject : Entity
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