using MedicineApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicineApi.Services;

public class MedicationService 
{
    private readonly IMongoCollection<Medication> _medicationCollection;

    public MedicationService (
        IOptions<Settings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

       _medicationCollection = mongoDatabase.GetCollection<Medication>(
            databaseSettings.Value.MedicationsCollection);
    }

    public async Task<Medication?> GetAsync(string id) =>
        await _medicationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Medication newMedication) =>
        await _medicationCollection.InsertOneAsync(newMedication);
}
