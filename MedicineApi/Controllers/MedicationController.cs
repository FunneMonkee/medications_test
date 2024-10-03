using MedicineApi.Models;
using MedicineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineApi.Controllers;

[ApiController]
[Route("api/medications")]
public class MedicationController: ControllerBase
{
    private readonly MedicationService _medicationService;

    public MedicationController(MedicationService medicationService) =>
        _medicationService = medicationService;

    [HttpGet]
    public async Task<List<Medication>> Get() =>
        await _medicationService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Medication>> Get(string id)
    {
        var medication = await _medicationService.GetAsync(id);

        if (medication is null)
        {
            return NotFound();
        }

        return medication;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Medication newMedication)
    {
        await _medicationService.CreateAsync(newMedication);

        return CreatedAtAction(nameof(Get), new { id = newMedication.Id }, newMedication);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var medication = await _medicationService.GetAsync(id);

        if (medication is null)
        {
            return NotFound();
        }

        await _medicationService.RemoveAsync(id);

        return NoContent();
    }
}
