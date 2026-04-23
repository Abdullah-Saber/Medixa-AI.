using Microsoft.AspNetCore.Mvc;
using Medixa_AI.Application.DTOs;

namespace Medixa_AI.Api.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        // TODO: Inject IPatientService via constructor
        // private readonly IPatientService _patientService;

        // GET: api/patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            // var patients = await _patientService.GetAllPatientsAsync();
            // return Ok(patients);
            return Ok(new List<PatientDto>());
        }

        // GET: api/patient/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(Guid id)
        {
            // var patient = await _patientService.GetPatientByIdAsync(id);
            // if (patient == null) return NotFound();
            // return Ok(patient);
            return Ok(new PatientDto());
        }

        // POST: api/patient
        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] CreatePatientDto dto)
        {
            // var patient = await _patientService.CreatePatientAsync(dto);
            // return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            return Ok(new PatientDto());
        }

        // PUT: api/patient/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientDto dto)
        {
            // await _patientService.UpdatePatientAsync(id, dto);
            // return NoContent();
            return NoContent();
        }

        // DELETE: api/patient/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            // await _patientService.DeletePatientAsync(id);
            // return NoContent();
            return NoContent();
        }
    }
}
