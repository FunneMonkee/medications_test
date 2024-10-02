using MedicineApi.Models;
using MedicineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationController: ControllerBase
{
    private readonly MedicationService _medicationService;

    public MedicationController(MedicationService medicationService) =>
        _medicationService = medicationService;

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
}
