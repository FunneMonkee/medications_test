using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MedicineApi.Models
{
    public class Medication
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [BsonElement("quantity")]
        [JsonPropertyName("quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be higher than 0")] 
        public int Quantity { get; set; }

        [BsonElement("creationDate")]
        [JsonIgnore]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationDate { get; set; }
   }
}
