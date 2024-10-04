using Mongo2Go;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MedicineApi.Services;
using MedicineApi.Models;
using MedicineApi.Controllers;

public class MedicationControllerTest 
{
    private readonly MongoDbRunner _mongoDbRunner;
    private readonly MedicationService _medicationService;

    private readonly MedicationController _medicationController;

    public MedicationControllerTest()
    {
        _mongoDbRunner = MongoDbRunner.Start();
        
        var mongoClient = new MongoClient(_mongoDbRunner.ConnectionString);

        var settings = new Settings
        {
            ConnectionString = _mongoDbRunner.ConnectionString,
            DatabaseName = "TestDatabase",
            MedicationsCollection = "Medications"
        };

        var options = Options.Create(settings);

        _medicationService = new MedicationService(options);

        _medicationController = new MedicationController(_medicationService);
    }

    [Fact]
    public async Task WhenPOSTIsReceived_andMedicationIsValid_thenReturnOkWithLocation()
    {
        Medication newMedication = new Medication();
        newMedication.Name = "name";
        newMedication.Quantity = 1;

        var result = await _medicationController.Post(newMedication) as CreatedAtActionResult; 

        //Status and Value
        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.IsType<Medication>(result.Value);
        
        //Location ID
        Assert.NotNull(result.RouteValues);
        Assert.NotEmpty(result.RouteValues); 

        var first = result.RouteValues.First();
        Assert.NotNull(first.Value);

        string? id = first.Value.ToString()!;
        Assert.NotNull(id);
        Assert.Equal(24, id.Length);
    }
}
