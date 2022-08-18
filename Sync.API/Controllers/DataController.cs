using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sync.BL.HostedServiceQueue;
using Sync.BL.Models;
using Sync.BL.Services;
using Sync.DAL.Entities;

namespace Sync.API.Controllers;

[ApiController]
[Route("api/data/")]
public class DataController : ControllerBase
{
    private readonly IThreadSafeWriter _writer;
    private readonly IDataService _dataService; 
    
    public DataController(IDataService dataService, IThreadSafeWriter writer)
    {
        _dataService = dataService;
        _writer = writer;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IEnumerable<Data>> All()
    {
        var result = await _dataService.GetAllAsync();
        return result;
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromBody] Data model)
    {
        try
        {
            await _dataService.RemoveAsync(model);
        }
        catch (ArgumentException)
        {
            return NotFound(model);
        }

        return Ok();
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] Data model)
    {
        try
        {
            await _dataService.AddAsync(model);
        }
        catch (ArgumentNullException)
        {
            return BadRequest(model);
        }
        catch (ArgumentException)
        {
            return NotFound(model);
        }

        return Ok();
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] Data model)
    {
        try
        {
            await _dataService.UpdateAsync(model);
        }
        catch(ArgumentNullException)
        {
            return BadRequest(model);
        }
        catch (ArgumentException)
        {
            return NotFound(model);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("threadsafe/add")]
    public async Task<IActionResult> ThreadSafeAdd( [FromQuery] Value value)
    {
        await _writer.WriteAsync(value);
        
        return Ok();
    }
}