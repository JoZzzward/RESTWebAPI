using RESTWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTWebAPI.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(Item item);
    }
}