using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Inspimo_Microservice.Services.Catalog.Models;

namespace Inspimo_Microservice.Services.Catalog.Dtos
{
    public class ResultProductDto
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string PictureURL { get; set; }
        public string CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
