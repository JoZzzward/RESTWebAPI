using RESTWebAPI.Dtos;
using RESTWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTWebAPI.Repositories
{
    public class InMemItemRepository : IItemRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Poison", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return items;
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            Item item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(item => item.Id == id);
            items.RemoveAt(index);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }
    }
}
