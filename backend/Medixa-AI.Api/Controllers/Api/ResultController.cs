using Microsoft.AspNetCore.Mvc;
using Medixa_AI.Application.DTOs;

namespace Medixa_AI.Api.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        // TODO: Inject IResultService via constructor
        // private readonly IResultService _resultService;

        // GET: api/result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetResults()
        {
            // var results = await _resultService.GetAllResultsAsync();
            // return Ok(results);
            return Ok(new List<ResultDto>());
        }

        // GET: api/result/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultDto>> GetResult(int id)
        {
            // var result = await _resultService.GetResultByIdAsync(id);
            // if (result == null) return NotFound();
            // return Ok(result);
            return Ok(new ResultDto());
        }

        // GET: api/result/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<ResultDto>>> GetOrderResults(int orderId)
        {
            // var results = await _resultService.GetOrderResultsAsync(orderId);
            // return Ok(results);
            return Ok(new List<ResultDto>());
        }

        // POST: api/result
        [HttpPost]
        public async Task<ActionResult<ResultDto>> CreateResult([FromBody] CreateResultDto dto)
        {
            // var result = await _resultService.CreateResultAsync(dto);
            // return CreatedAtAction(nameof(GetResult), new { id = result.Id }, result);
            return Ok(new ResultDto());
        }

        // PUT: api/result/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, [FromBody] UpdateResultDto dto)
        {
            // await _resultService.UpdateResultAsync(id, dto);
            // return NoContent();
            return NoContent();
        }
    }
}
