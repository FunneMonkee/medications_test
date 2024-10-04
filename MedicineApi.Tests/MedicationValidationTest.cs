using MedicineApi.Models;
using System.ComponentModel.DataAnnotations;

public class MedicationValidationTest 
{
    [Theory]
    [InlineData(0)]
    [InlineData(int.MinValue)]
    public void WhenMedicationHasLessThan1Quantity_returnInvalid(int quantity)
    {
        Medication newMedication = new Medication();
        newMedication.Name = "name";
        newMedication.Quantity = quantity;

        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(newMedication, null, null);
        Validator.TryValidateObject(newMedication, ctx, validationResults, true);

        Assert.Contains(validationResults, v => v.MemberNames.Contains("Quantity") &&
            v.ErrorMessage!.Contains("Quantity must be higher than 0"));
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void WhenMedicationHasMoreThan1Quantity_returnValid(int quantity)
    {
        Medication newMedication = new Medication();
        newMedication.Name = "name";
        newMedication.Quantity = quantity;

        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(newMedication, null, null);
        Validator.TryValidateObject(newMedication, ctx, validationResults, true);

        Assert.Empty(validationResults);
    }

    [Fact]
    public void WhenMedicationIsCreated_thenCreationDateShouldBeFilled()
    {
        Medication newMedication = new Medication();
        newMedication.Name = "name";
        newMedication.Quantity = 1;

        TimeSpan expected = TimeSpan.FromMilliseconds(10); 

        Assert.True(Math.Abs((newMedication.CreationDate - DateTime.Now).TotalMilliseconds) <= expected.TotalMilliseconds);
    }
}
