using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Implementations;

/// <summary>
/// Репозиторий с городами субъектов Российской Федерации
/// </summary>
public class CityOfSubjectRepository : IReadRepository<CityOfSubject>
{
    private List<CityOfSubject> _cityOfSubjects =
    [
        new CityOfSubject { Subject = "Адыгея", City = "Майкоп", Lat = 44.606208, Lon = 40.104053 },
        new CityOfSubject { Subject = "Республика Алтай", City = "Горно-Алтайск", Lat = 51.957775, Lon = 85.963653 },
        new CityOfSubject { Subject = "Башкортостан", City = "Уфа", Lat = 54.726141, Lon = 55.947499 },
        new CityOfSubject { Subject = "Бурятия", City = "Улан-Удэ", Lat = 51.835784, Lon = 107.583911 },
        new CityOfSubject { Subject = "Дагестан", City = "Махачкала", Lat = 42.983024, Lon = 47.504872 },
        new CityOfSubject { Subject = "ДНР", City = "Донецк", Lat = 48.015875, Lon = 37.801341 },
        new CityOfSubject { Subject = "Ингушетия", City = "Магас", Lat = 43.166650, Lon = 44.804819 },
        new CityOfSubject { Subject = "Кабардино-Балкария", City = "Нальчик", Lat = 43.476960, Lon = 43.596658 },
        new CityOfSubject { Subject = "Калмыкия", City = "Элиста", Lat = 46.307329, Lon = 44.269221 },
        new CityOfSubject { Subject = "Карачаево-Черкесия", City = "Черкесск", Lat = 44.228523, Lon = 42.048257 },
        new CityOfSubject { Subject = "Карелия", City = "Петрозаводск", Lat = 61.789221, Lon = 34.368804 },
        new CityOfSubject { Subject = "Коми", City = "Сыктывкар", Lat = 61.668524, Lon = 50.835202 },
        new CityOfSubject { Subject = "Республика Крым", City = "Симферополь", Lat = 44.952146, Lon = 34.102486 },
        new CityOfSubject { Subject = "ЛНР", City = "Луганск", Lat = 48.571708, Lon = 39.297315 },
        new CityOfSubject { Subject = "Марий Эл", City = "Йошкар-Ола", Lat = 56.631556, Lon = 47.886880 },
        new CityOfSubject { Subject = "Мордовия", City = "Саранск", Lat = 54.186710, Lon = 45.183830 },
        new CityOfSubject { Subject = "Якутия", City = "Якутск", Lat = 62.027408, Lon = 129.731979 },
        new CityOfSubject { Subject = "Северная Осетия", City = "Владикавказ", Lat = 43.024593, Lon = 44.682110 },
        new CityOfSubject { Subject = "Татарстан", City = "Казань", Lat = 55.794649, Lon = 49.111502 },
        new CityOfSubject { Subject = "Тыва", City = "Кызыл", Lat = 51.719079, Lon = 94.430068 },
        new CityOfSubject { Subject = "Удмуртия", City = "Ижевск", Lat = 56.852973, Lon = 53.210307 },
        new CityOfSubject { Subject = "Хакасия", City = "Абакан", Lat = 53.720680, Lon = 91.440602 },
        new CityOfSubject { Subject = "Чечня", City = "Грозный", Lat = 43.319703, Lon = 45.693431 },
        new CityOfSubject { Subject = "Чувашия", City = "Чебоксары", Lat = 56.139960, Lon = 47.248100 },
        new CityOfSubject { Subject = "Алтайский край", City = "Барнаул", Lat = 53.347549, Lon = 83.778845 },
        new CityOfSubject { Subject = "Забайкальский край", City = "Чита", Lat = 52.033409, Lon = 113.500893 },
        new CityOfSubject { Subject = "Камчатский край", City = "Петропавловск-Камчатский", Lat = 53.019984, Lon = 158.647136 },
        new CityOfSubject { Subject = "Краснодарский край", City = "Краснодар", Lat = 45.035153, Lon = 38.977240 },
        new CityOfSubject { Subject = "Красноярский край", City = "Красноярск", Lat = 56.009117, Lon = 92.872586 },
        new CityOfSubject { Subject = "Пермский край", City = "Пермь", Lat = 58.010853, Lon = 56.231853 },
        new CityOfSubject { Subject = "Приморский край", City = "Владивосток", Lat = 43.115068, Lon = 131.885577 },
        new CityOfSubject { Subject = "Ставропольский край", City = "Ставрополь", Lat = 45.043325, Lon = 41.969093 },
        new CityOfSubject { Subject = "Хабаровский край", City = "Хабаровск", Lat = 48.481257, Lon = 135.076297 },
        new CityOfSubject { Subject = "Амурская область", City = "Благовещенск", Lat = 50.260042, Lon = 127.533738 },
        new CityOfSubject { Subject = "Архангельская область", City = "Архангельск", Lat = 64.541025, Lon = 40.528677 },
        new CityOfSubject { Subject = "Астраханская область", City = "Астрахань", Lat = 46.349831, Lon = 48.032620 },
        new CityOfSubject { Subject = "Белгородская область", City = "Белгород", Lat = 50.595560, Lon = 36.587339 },
        new CityOfSubject { Subject = "Брянская область", City = "Брянск", Lat = 53.242378, Lon = 34.366829 },
        new CityOfSubject { Subject = "Владимирская область", City = "Владимир", Lat = 56.128890, Lon = 40.407520 },
        new CityOfSubject { Subject = "Волгоградская область", City = "Волгоград", Lat = 48.708191, Lon = 44.515335 },
        new CityOfSubject { Subject = "Вологодская область", City = "Вологда", Lat = 59.218939, Lon = 39.893391 },
        new CityOfSubject { Subject = "Воронежская область", City = "Воронеж", Lat = 51.660598, Lon = 39.200586 },
        new CityOfSubject { Subject = "Запорожская область", City = "Мелитополь", Lat = 46.846727, Lon = 35.382728 },
        new CityOfSubject { Subject = "Ивановская область", City = "Иваново", Lat = 56.995686, Lon = 40.981210 },
        new CityOfSubject { Subject = "Иркутская область", City = "Иркутск", Lat = 52.289123, Lon = 104.279829 },
        new CityOfSubject { Subject = "Калининградская область", City = "Калининград", Lat = 54.710128, Lon = 20.510584 },
        new CityOfSubject { Subject = "Калужская область", City = "Калуга", Lat = 54.510109, Lon = 36.259812 },
        new CityOfSubject { Subject = "Кемеровская область", City = "Кемерово", Lat = 55.355091, Lon = 86.087121 },
        new CityOfSubject { Subject = "Кировская область", City = "Киров", Lat = 58.603566, Lon = 49.666624 },
        new CityOfSubject { Subject = "Костромская область", City = "Кострома", Lat = 57.767916, Lon = 40.926914 },
        new CityOfSubject { Subject = "Курганская область", City = "Курган", Lat = 55.440936, Lon = 65.342117 },
        new CityOfSubject { Subject = "Курская область", City = "Курск", Lat = 51.730925, Lon = 36.192312 },
        new CityOfSubject { Subject = "Ленинградская область", City = "Гатчина", Lat = 59.568041, Lon = 30.125208 },
        new CityOfSubject { Subject = "Липецкая область", City = "Липецк", Lat = 52.605149, Lon = 39.596378 },
        new CityOfSubject { Subject = "Магаданская область", City = "Магадан", Lat = 59.564566, Lon = 150.807883 },
        new CityOfSubject { Subject = "Московская область", City = "Красногорск", Lat = 55.820471, Lon = 37.319694 },
        new CityOfSubject { Subject = "Мурманская область", City = "Мурманск", Lat = 68.970665, Lon = 33.074970 },
        new CityOfSubject { Subject = "Нижегородская область", City = "Нижний Новгород", Lat = 56.326482, Lon = 44.005140 },
        new CityOfSubject { Subject = "Новгородская область", City = "Великий Новгород", Lat = 58.520986, Lon = 31.275786 },
        new CityOfSubject { Subject = "Новосибирская область", City = "Новосибирск", Lat = 54.967815, Lon = 82.951599 },
        new CityOfSubject { Subject = "Омская область", City = "Омск", Lat = 54.991375, Lon = 73.371529 },
        new CityOfSubject { Subject = "Оренбургская область", City = "Оренбург", Lat = 51.767125, Lon = 55.097852 },
        new CityOfSubject { Subject = "Орловская область", City = "Орёл", Lat = 52.968017, Lon = 36.099499 },
        new CityOfSubject { Subject = "Пензенская область", City = "Пенза", Lat = 53.193784, Lon = 45.006741 },
        new CityOfSubject { Subject = "Псковская область", City = "Псков", Lat = 57.817392, Lon = 28.334347 },
        new CityOfSubject { Subject = "Ростовская область", City = "Ростов-на-Дону", Lat = 47.222260, Lon = 39.719874 },
        new CityOfSubject { Subject = "Рязанская область", City = "Рязань", Lat = 54.629569, Lon = 39.742504 },
        new CityOfSubject { Subject = "Самарская область", City = "Самара", Lat = 53.195626, Lon = 50.101493 },
        new CityOfSubject { Subject = "Саратовская область", City = "Саратов", Lat = 51.530018, Lon = 46.034683 },
        new CityOfSubject { Subject = "Сахалинская область", City = "Южно-Сахалинск", Lat = 46.957427, Lon = 142.727438 },
        new CityOfSubject { Subject = "Свердловская область", City = "Екатеринбург", Lat = 56.838207, Lon = 60.600789 },
        new CityOfSubject { Subject = "Смоленская область", City = "Смоленск", Lat = 54.781406, Lon = 32.046126 },
        new CityOfSubject { Subject = "Тамбовская область", City = "Тамбов", Lat = 52.721616, Lon = 41.452399 },
        new CityOfSubject { Subject = "Тверская область", City = "Тверь", Lat = 56.859671, Lon = 35.895242 },
        new CityOfSubject { Subject = "Томская область", City = "Томск", Lat = 56.488753, Lon = 84.952343 },
        new CityOfSubject { Subject = "Тульская область", City = "Тула", Lat = 54.193032, Lon = 37.617540 },
        new CityOfSubject { Subject = "Тюменская область", City = "Тюмень", Lat = 57.153534, Lon = 65.542274 },
        new CityOfSubject { Subject = "Ульяновская область", City = "Ульяновск", Lat = 54.315028, Lon = 48.403373 },
        new CityOfSubject { Subject = "Херсонская область", City = "Херсон", Lat = 46.640130, Lon = 32.614392 },
        new CityOfSubject { Subject = "Челябинская область", City = "Челябинск", Lat = 55.159841, Lon = 61.402555 },
        new CityOfSubject { Subject = "Ярославская область", City = "Ярославль", Lat = 57.626388, Lon = 39.893371 },
        new CityOfSubject { Subject = "Москва", City = "Москва", Lat = 55.755800, Lon = 37.617300 },
        new CityOfSubject { Subject = "Санкт-Петербург", City = "Санкт-Петербург", Lat = 59.934300, Lon = 30.335100 },
        new CityOfSubject { Subject = "Севастополь", City = "Севастополь", Lat = 44.605443, Lon = 33.522084 },
        new CityOfSubject { Subject = "Еврейская АО", City = "Биробиджан", Lat = 48.793290, Lon = 132.926984 },
        new CityOfSubject { Subject = "Ненецкий АО", City = "Нарьян-Мар", Lat = 67.638018, Lon = 53.007104 },
        new CityOfSubject { Subject = "Ханты-Мансийский АО", City = "Ханты-Мансийск", Lat = 61.003525, Lon = 69.018968 },
        new CityOfSubject { Subject = "Чукотский АО", City = "Анадырь", Lat = 64.731688, Lon = 177.506093 },
        new CityOfSubject { Subject = "Ямало-Ненецкий АО", City = "Салехард", Lat = 66.529866, Lon = 66.614631 }
    ];

    public CityOfSubjectRepository()
    {
        foreach (var cityOfSubject in _cityOfSubjects)
        {
            cityOfSubject.Id = Guid.NewGuid();
        }
    }
    
    /// <inheritdoc/>
    public async Task<ICollection<CityOfSubject>> GetAll()
    {
        return _cityOfSubjects;
    }
}