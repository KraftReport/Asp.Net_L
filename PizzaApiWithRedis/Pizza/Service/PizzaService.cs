using PizzaApiWithRedis.Pizza.Model;
using PizzaApiWithRedis.Pizza.Repository; 

namespace PizzaApiWithRedis.Pizza.Service
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaRepository pizzaRepository;
        private readonly ICacheManagerService cacheManager;
        public PizzaService(PizzaRepository pizzaRepository, ICacheManagerService cacheManager)
        {
            this.pizzaRepository = pizzaRepository;
            this.cacheManager = cacheManager;
        }

        public async Task<ApiResponseDto> getPizzaById(int id)
        {
            var data = await findDataInTheCache<CacheDto>(id.ToString());
            if (data.Item1)
            {
                return await cacheDtoToResponseDto(data.Item2);
            }
            var found = await findPizzaById(id);
            await setDataIntoCache<CacheDto>(found.id.ToString(), await entityToCacheDto(found));
            return await entityToResponseDtoMapper(found);
        }

        public async Task<int> addPizzaToCatalog(ApiRequestDto pizzaRequest)
        {
            return await pizzaRepository.addPizzaToCatalog(await dtoToEntityMapper(pizzaRequest));
        }

        public async Task<List<string>> getListOfPizzaNames()
        {
            var data = await findDataInTheCache<List<string>>("pizzaList");
            if (data.Item1 == false)
            {
                var found = await pizzaRepository.getListOfPizzas();
                await setDataIntoCache<List<string>>("pizzaList", found);
                return found;
            }
            return data.Item2;
        }

        public async Task<byte[]> getPizzaPhoto(int id)
        {
            var data = await getPizzaById(id);
            return await File.ReadAllBytesAsync(data.pizzaObject.photo);
        }

        public async Task<ApiResponseDto> updatePizzaById(int id,ApiRequestDto updateRequest)
        {
            var pizza = await dtoToEntityMapper(updateRequest); 
            var data = await findDataInTheCache<CacheDto>(id.ToString());
            var updated = await entityToResponseDtoMapper(await pizzaRepository.updatePizzaById(id, pizza));
            if (data.Item1)
            {
                pizza.id = id;
                await cacheManager.saveIntoCacheDbWithKey(id.ToString(), pizza);
            }
            return updated;
        }

        public async Task<bool> deletePizza(int id)
        {
            var found = await findDataInTheCache<CacheDto>(id.ToString());
            if (found.Item1)
            {
                await cacheManager.deleteFromCache(id.ToString());
            }
            return await pizzaRepository.deletePizza(id);
        }













        private async Task<ApiResponseDto> entityToResponseDtoMapper(PizzaDetail pizza)
        {
            return new ApiResponseDto
            {
                pizzaObject = pizza,
                Base64String = await getBase64String(pizza.photo)
            };
        }

        private async Task<CacheDto> entityToCacheDto(PizzaDetail pizza)
        {
            return new CacheDto
            {
                id = pizza.id,
                description = pizza.description,
                name = pizza.name,
                photo = pizza.photo,
                price = pizza.price,
                base64String = await getBase64String(pizza.photo)
            };
        }

        private async Task<ApiResponseDto> cacheDtoToResponseDto(CacheDto cacheDto)
        { 
            return new ApiResponseDto
            {
                pizzaObject = new PizzaDetail
                {
                    id = cacheDto.id,
                    name = cacheDto.name,
                    description = cacheDto.description,
                    price = cacheDto.price,
                    photo = cacheDto.photo,
                },
                Base64String = await getBase64String(cacheDto.photo)
            };
        }

        private async Task<string> getBase64String(string url)
        { 
            return Convert.ToBase64String(await File.ReadAllBytesAsync(url));
        }

        private async Task<PizzaDetail> findPizzaById(int id)
        {
            return await pizzaRepository.getPizzaById(id);
        }

        private async Task<bool> setDataIntoCache<T>(string key,T value)
        {
            return await cacheManager.saveIntoCacheDbWithKey(key, value);
        }

        private async Task<(bool,T?)> findDataInTheCache<T>(string key)
        {
            var found = await cacheManager.findDataInTheCache<T>(key);
            if (found == null)
            {
                return (false,default(T));
            }
            return (true,found);
        }


        private async Task<PizzaDetail> dtoToEntityMapper(ApiRequestDto pizzaRequest)
        {
            return  new PizzaDetail
            {
                name = pizzaRequest.name,
                description = pizzaRequest.description,
                price = pizzaRequest.price,
                photo = await savePhoto(pizzaRequest.photo)
            };
        }

        private async Task<string> savePhoto(IFormFile photoFile)
        {
            var directory = await createDirectory();
            var fileName = await createFileName(photoFile);
            var filePath = Path.Combine(directory, fileName);
            await savePhotoToDirectory(photoFile, filePath);
            return Path.Combine(directory, fileName).Replace("\\","/");
        }

        private async Task<string>  createDirectory()
        {
            var location = Path.Combine(Directory.GetCurrentDirectory(),"Resources", "Images");
            if(!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }
            return location;
        }

        private async Task<string> createFileName(IFormFile file)
        {
            return $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        }

        private async Task savePhotoToDirectory(IFormFile photoFile,string filePath)
        {
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream);
            }
        }
    }
}
