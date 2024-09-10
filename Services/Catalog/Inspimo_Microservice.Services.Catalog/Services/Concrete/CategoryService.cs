using AutoMapper;
using Inspimo_Microservice.Services.Catalog.Dtos;
using Inspimo_Microservice.Services.Catalog.Models;
using Inspimo_Microservice.Services.Catalog.Services.Abstract;
using Inspimo_Microservice.Services.Catalog.Settings.Abstract;
using Inspimo_Microservice.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inspimo_Microservice.Services.Catalog.Services.Concrete
{
    public class CategoryService : ICategoroyService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDataBaseSettings dataBaseSettings)
        {
            _mapper = mapper;
            var client= new MongoClient(dataBaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(dataBaseSettings.DataBaseName);
            _categoryCollection = dataBase.GetCollection<Category>(dataBaseSettings.CategoryCollectionName);
        }

        public async Task<Response<NoContent>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            //Category ile Category e ait Dto yu mapledi.
            var values = _mapper.Map<Category>(createCategoryDto);
            //Kayıt ekleme işlemi
            await _categoryCollection.InsertOneAsync(values);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result=await _categoryCollection.DeleteOneAsync(x=>x.CategoryID==id);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<ResultCategoryDto>>> GetAllAsync()
        {
            var values = await _categoryCollection.Find(value => true).ToListAsync();
            return Response<List<ResultCategoryDto>>.Success(_mapper.Map<List<ResultCategoryDto>>(values), 200);
        }

        public async Task<Response<ResultCategoryDto>> GetByIdAsync(string id)
        {
            var value=await _categoryCollection.Find<Category>(x=>x.CategoryID== id).FirstOrDefaultAsync();
            if (value==null)
            {
                return Response<ResultCategoryDto>.Fail("Kategori bulunamadı.", 404);
            }
            return Response<ResultCategoryDto>.Success(_mapper.Map<ResultCategoryDto>(value),200);
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = _mapper.Map<Category>(updateCategoryDto);
            var result=await _categoryCollection.FindOneAndReplaceAsync(x=>x.CategoryID==updateCategoryDto.CategoryID,value);
            return Response<NoContent>.Success(200);
        }
    }
}
