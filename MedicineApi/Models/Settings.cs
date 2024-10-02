namespace MedicineApi.Models
{
    public class Settings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MedicationsCollection { get; set; } = null!;
    }
}
