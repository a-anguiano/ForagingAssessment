using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL.Tests.TestDoubles
{
    public class ItemRepositoryDouble : IItemRepository
    {
        public static readonly Item ITEM = new Item(1, "Chanterelle", Category.Edible, 9.99M);
        private List<Item> items = new List<Item>();

        public ItemRepositoryDouble()
        {
            items.Add(ITEM);
        }
        public Item Add(Item item)
        {
            List<Item> all = FindAll();
            item.Id = all.Max(i => i.Id) + 1;
            all.Add(item);
            return item;
        }

        public List<Item> FindAll()
        {
            return new List<Item>(items);
        }

        public Item FindById(int id)
        {
            return FindAll().FirstOrDefault(i => i.Id == id);
        }
    }
}
