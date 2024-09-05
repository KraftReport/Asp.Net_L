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

        public async Task<PizzaDetail> getPizzaById(int id)
        {
            return await pizzaContext.FindAsync(id);
        }
        
        public async Task<PizzaDetail> updatePizzaById(int id, PizzaDetail pizza)
        {
            pizza.id = id;
            var updated = pizzaContext.Update(pizza).Entity;
            await context.SaveChangesAsync();
            return updated;
        }

        public async Task<bool> deletePizza(int id)
        {
            var found = pizzaContext.Remove(await getPizzaById(id));
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<PizzaDetail>> findAllPizza()
        {
            return await pizzaContext.ToListAsync();
        }

    }
}
