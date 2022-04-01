using SustainableForaging.Core.Exceptions;
using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SustainableForaging.DAL
{
    public class ItemFileRepository : IItemRepository
    {
        private const string HEADER = "id,name,category,dollars/kilogram";
        private readonly string filePath;

        public ItemFileRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public Item Add(Item item)
        {
            if(item == null)
            {
                return null;
            }

            List<Item> all = FindAll();

            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;

            item.Id = nextId;

            all.Add(item);
            Write(all);

            return item;
        }

        public List<Item> FindAll()
        {
            var items = new List<Item>();
            if(!File.Exists(filePath))
            {
                return items;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not read items", ex);
            }

            for(int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Item item = Deserialize(fields);
                if(item != null)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public Item FindById(int id)
        {
            return FindAll().FirstOrDefault(i => i.Id == id);
        }

        private string Serialize(Item item)
        {
            return string.Format("{0},{1},{2},{3:0.00}",
                    item.Id,
                    item.Name,
                    item.Category,
                    item.DollarsPerKilogram);
        }

        private Item Deserialize(string[] fields)
        {
            if(fields.Length != 4)
            {
                return null;
            }

            Item result = new Item();
            result.Id = int.Parse(fields[0]);
            result.Name = fields[1];
            result.Category = Enum.Parse<Category>(fields[2], true);
            result.DollarsPerKilogram = decimal.Parse(fields[3]);
            return result;
        }

        private void Write(List<Item> items)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine(HEADER);

                if(items == null)
                {
                    return;
                }

                foreach(var item in items)
                {
                    writer.WriteLine(Serialize(item));
                }
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not write items", ex);
            }
        }
    }
}
