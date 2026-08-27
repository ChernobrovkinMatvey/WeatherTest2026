namespace Weather.Application.Contracts;

/// <summary>
/// Город субъекта Российской Федерации
/// </summary>
public class CityOfSubjectContract
{
    /// <summary>
    /// Id записи
    /// </summary>
    public string Id { get; set; }
    
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