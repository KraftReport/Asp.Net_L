using PizzaApiWithRedis.Pizza.Model;
using PizzaApiWithRedis.Pizza.Repository;
using System.ComponentModel;

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

        public async Task<List<string>> getListOfPizzaNames()
        {
            var data = await findDataInTheCache<List<string>>("pizzaList");
            if (data == null)
            {
                var found = await pizzaRepository.getListOfPizzas();
                await setDataIntoCache<List<string>>("pizzaList", found);
                return found;
            }
            return data;
        }

        private async Task<bool> setDataIntoCache<T>(string key,T value)
        {
            return await cacheManager.saveIntoCacheDbWithKey(key, value);
        }

        private async Task<T> findDataInTheCache<T>(string key)
        {
            return await cacheManager.findDataInTheCache<T>(key);
        }

        public async Task<int> addPizzaToCatalog(ApiRequestDto pizzaRequest)
        {
            return await pizzaRepository.addPizzaToCatalog(await dtoToEntityMapper(pizzaRequest));
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
