using Microsoft.EntityFrameworkCore;
using PizzaApiWithRedis.Database;
using PizzaApiWithRedis.Pizza.Model;

namespace PizzaApiWithRedis.Pizza.Repository
{
    public class PizzaRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<PizzaDetail> pizzaContext;
        public PizzaRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.pizzaContext = context.pizza;
        }

        public async Task<int> addPizzaToCatalog(PizzaDetail pizza)
        {
            await pizzaContext.AddAsync(pizza);
            return await context.SaveChangesAsync();
        }

        public async Task<List<string>> getListOfPizzas()
        {
            return await pizzaContext.Select(p=>p.name).ToListAsync();
        }

        
    }
}
