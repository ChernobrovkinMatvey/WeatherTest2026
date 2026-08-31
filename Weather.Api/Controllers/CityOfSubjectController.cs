using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Contracts;
using Weather.Application.DTOs;
using Weather.Application.Queries;

namespace Weather.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityOfSubjectController(IMediator mediator, IMapper mapper) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
        
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<ICollection<CityOfSubjectContract>> Get()
    {
        var cityOfSubjects = await _mediator.Send(new GetAllCityOfSubjectQuery());
        
        return _mapper.Map<ICollection<CityOfSubjectContract>>(cityOfSubjects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CityOfSubjectContract>> Get(string id)
    {
        var cityOfSubject = await _mediator.Send(new GetCityOfSubjectQuery() { Id = Guid.Parse(id) });
        if (cityOfSubject == null)
            return NotFound();
        
        var contract = _mapper.Map<CityOfSubjectContract>(cityOfSubject);
        return contract;
    }
    
    /// <summary>
    /// Создание административного центра
    /// </summary>
    [HttpPost]
    public async Task<CityOfSubjectContract> Create([FromBody]CityOfSubjectCreateContract cityOfSubject)
    {
        var createDto = new CreateCityOfSubjectCommand()
        {
            CreateDto = _mapper.Map<CityOfSubjectCreateDto>(cityOfSubject)
        };
        var newCityOfSubject = await _mediator.Send(createDto);
        
        var contract = _mapper.Map<CityOfSubjectContract>(newCityOfSubject);
        return contract;
    }

    // [HttpPut] // HttpPost - когда обновление носит характер побочного, неидемпотентного действия 
    // public async Task<IActionResult> Update([FromBody]CityOfSubjectContract cityOfSubject)
    // {
    //     //
    // }
    
    /* [HttpPatch]
    public async Task<CityOfSubjectContract> Update([FromBody]CityOfSubjectContract cityOfSubject)
    {
        // PATCH — частичное обновление ресурса. Передаёте только те поля, которые нужно изменить
    } */

    // [HttpDelete]
    // public async Task Delete([fromBody]string id)
    // {
    //     //
    // }
}