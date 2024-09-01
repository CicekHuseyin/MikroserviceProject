using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inspimo_Microservice.Services.Catalog.Models
{
    public class Category
    {
        [BsonId]//Hangi sütunun id olduğunu belirtiyor.
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryID { get; set; }
        //MongoDb nin ID değeri string
        public string CategoryName { get; set; }
    }
}
