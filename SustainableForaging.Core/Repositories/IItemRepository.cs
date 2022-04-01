using SustainableForaging.Core.Models;
using System.Collections.Generic;

namespace SustainableForaging.Core.Repositories
{
    public interface IItemRepository
    {
        List<Item> FindAll();

        Item FindById(int id);

        Item Add(Item item);
    }
}
