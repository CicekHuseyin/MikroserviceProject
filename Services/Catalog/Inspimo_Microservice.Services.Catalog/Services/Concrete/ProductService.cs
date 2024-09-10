using AutoMapper;
using Inspimo_Microservice.Services.Catalog.Dtos;
using Inspimo_Microservice.Services.Catalog.Models;
using Inspimo_Microservice.Services.Catalog.Services.Abstract;
using Inspimo_Microservice.Services.Catalog.Settings.Abstract;
using Inspimo_Microservice.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspimo_Microservice.Services.Catalog.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper,IDataBaseSettings dataBaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(dataBaseSettings.DataBaseName);
            _productCollection = dataBase.GetCollection<Product>(dataBaseSettings.ProductCollectionName);
            _categoryCollection = dataBase.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
        }

        public async Task<Response<NoContent>> CreateAsync(CreateProductDto createProductDto)
        {
            var values=_mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(values);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(id);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<ResultProductDto>>> GetAllAsync()
        {
            var values = await _productCollection.Find(value => true).ToListAsync();
            //İçinde herhangi bir değer var mı.
            if (values.Any())
            {
                //her bir ürünün kategorisini almak için
                foreach (var item in values)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).FirstAsync();
                }
            }
            else
            {
                values = new List<Product>();
            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<ResultProductDto>> GetByIdAsync(string id)
        {
            var value = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            value.Category= await _categoryCollection.Find<Category>(x=>x.CategoryID==value.CategoryID).FirstOrDefaultAsync();
            return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(value), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, value);
            return Response<NoContent>.Success(200);
        }
    }
}
