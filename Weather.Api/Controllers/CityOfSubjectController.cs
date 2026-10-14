using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Contracts;
using Weather.Application.DTOs;
using Weather.Application.Queries;
using Weather.Domain.Entities;

namespace Weather.Api.Controllers;

/// <summary>
/// Контроллер административных центров субъектов РФ.
/// Реализует REST API: GET (список/по id), POST (создание), PUT (обновление), DELETE (удаление).
/// Все операции делегируются в домен через MediatR, а данные маппятся в контракты с помощью AutoMapper.
/// Необработанные исключения обрабатываются глобально через <see cref="Exceptions.ExceptionHandler"/>.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CityOfSubjectController(IMediator mediator, IMapper mapper) : ControllerBase
{
    // MediatR — отправляет запросы/команды обработчикам в Application-слое
    private readonly IMediator _mediator = mediator;

    // AutoMapper — преобразует сущности домена в контракты (и обратно)
    private readonly IMapper _mapper = mapper;
    
    /// <summary>
    /// Получает все административные центры РФ
    /// </summary>
    [HttpGet]
    [ProducesResponseType<ICollection<CityOfSubjectContract>>(StatusCodes.Status200OK)]
    public async Task<ICollection<CityOfSubjectContract>> Get()
    {
        var cityOfSubjectRecords = await _mediator.Send(new GetAllCityOfSubjectQuery());
        
        return _mapper.Map<ICollection<CityOfSubjectContract>>(cityOfSubjectRecords);
    }

    /// <summary>
    /// Получает административный центр РФ по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<CityOfSubjectContract>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CityOfSubjectContract>> Get(string id)
    {
        var cityOfSubjectRecord = await FindCityOfSubjectAsync(Guid.Parse(id));
        if (cityOfSubjectRecord == null)
            return NotFound();
        
        var contract = _mapper.Map<CityOfSubjectContract>(cityOfSubjectRecord);
        return contract;
    }
    
    /// <summary>
    /// Создание административного центра РФ
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CityOfSubjectContract>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<CityOfSubjectContract> Create([FromBody]CityOfSubjectCreateContract cityOfSubject)
    {
        var createCommand = new CreateCityOfSubjectCommand()
        {
            CreateDto = _mapper.Map<CityOfSubjectPushDto>(cityOfSubject)
        };
        var newCityOfSubject = await _mediator.Send(createCommand);
        
        var contract = _mapper.Map<CityOfSubjectContract>(newCityOfSubject);
        return contract;
    }

    /// <summary>
    /// Обновление административного центра РФ
    /// </summary>
    [HttpPut] // HttpPost - когда обновление носит характер побочного, неидемпотентного действия 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody]CityOfSubjectContract cityOfSubject)
    {
        if (await FindCityOfSubjectAsync(Guid.Parse(cityOfSubject.Id)) == null)
            return NotFound();
        
        var updateCommand = new UpdateCityOfSubjectCommand()
        {
            Id = Guid.Parse(cityOfSubject.Id),
            UpdateDto = _mapper.Map<CityOfSubjectPushDto>(cityOfSubject)
        };
        await _mediator.Send(updateCommand);
        
        return Ok();
    }
    
    /* [HttpPatch]
    public async Task<CityOfSubjectContract> Update([FromBody]CityOfSubjectContract cityOfSubject)
    {
        // PATCH — частичное обновление ресурса. Передаёте только те поля, которые нужно изменить
    } */

    /// <summary>
    /// Удаление административного центра РФ по ID
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromBody]string id)
    {
        if (await FindCityOfSubjectAsync(Guid.Parse(id)) == null)
            return NotFound(); 
        
        var deleteCommand = new DeleteCityOfSubjectCommand { Id = Guid.Parse(id) };
        await _mediator.Send(deleteCommand);

        return Ok();
    }

    private async Task<CityOfSubject?> FindCityOfSubjectAsync(Guid id)
    {
        return await _mediator.Send(new GetCityOfSubjectQuery() { Id = id });
    }
}